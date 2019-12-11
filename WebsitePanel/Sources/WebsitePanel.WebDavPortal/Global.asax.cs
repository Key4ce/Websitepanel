// Copyright (c) 2019, WebsitePanel-Support.net.
// Distributed by websitepanel-support.net
// Build and fixed by Key4ce - IT Professionals
// https://www.key4ce.com
// 
// Original source:
// Copyright (c) 2015, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using AutoMapper;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Interfaces.Security;
using WebsitePanel.WebDav.Core.Security.Authentication.Principals;
using WebsitePanel.WebDav.Core.Security.Cryptography;
using WebsitePanel.WebDavPortal.App_Start;
using WebsitePanel.WebDavPortal.Controllers;
using WebsitePanel.WebDavPortal.CustomAttributes;
using WebsitePanel.WebDavPortal.DependencyInjection;
using WebsitePanel.WebDavPortal.HttpHandlers;
using WebsitePanel.WebDavPortal.Mapping;
using WebsitePanel.Server.Utils;

namespace WebsitePanel.WebDavPortal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Log.WriteStart("Application_Start");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AccessTokenHandler());

            DependencyResolver.SetResolver(new NinjectDependecyResolver());

            AutoMapperPortalConfiguration.Configure();
            
            Mapper.AssertConfigurationIsValid();

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
               typeof(PhoneNumberAttribute),
               typeof(RegularExpressionAttributeAdapter));

            Log.WriteEnd("Application_Start");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            Server.ClearError();

            int statusCode;

            if (lastError.GetType() == typeof (HttpException))
                statusCode = ((HttpException) lastError).GetHttpCode();
            else
                statusCode = 500;

            var contextWrapper = new HttpContextWrapper(Context);

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);
            routeData.Values.Add("isAjaxRequet", contextWrapper.Request.IsAjaxRequest());

            IController controller = new ErrorController();
            var requestContext = new RequestContext(contextWrapper, routeData);
            controller.Execute(requestContext);
            Response.End();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var s = HttpContext.Current.Request;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            Log.WriteStart("Application_PostAuthenticateRequest");

            if (!IsOwaRequest())
            {
                Log.WriteInfo("Try get HttpContext ...");
                var contextWrapper = new HttpContextWrapper(Context);

                Log.WriteInfo("Try get Auth-Cookie ...");
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                Log.WriteInfo("Try get Auth-Servive ...");
                var authService = DependencyResolver.Current.GetService<IAuthenticationService>();

                Log.WriteInfo("Try get Crypto-Service ...");
                var cryptography = DependencyResolver.Current.GetService<ICryptography>();

                if (authCookie != null)
                {
                    Log.WriteInfo("Found Auth-Cookie!");
                    Log.WriteInfo("Try to Decrpyt ...");
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    Log.WriteInfo("Try to get UserData from Auth-Cookie");
                    var serializer = new JavaScriptSerializer();
                    var principalSerialized = serializer.Deserialize<WspPrincipal>(authTicket.UserData);

                    Log.WriteInfo("Try to Login ...");
                    authService.LogIn(principalSerialized.Login, cryptography.Decrypt(principalSerialized.EncryptedPassword));

                    if (!contextWrapper.Request.IsAjaxRequest())
                    {
                        SetAuthenticationExpirationTicket();
                    }
                }
                else
                {
                    Log.WriteWarning("Auth-Cookie is null");
                }
            }
            else
            {
                Log.WriteInfo("Is OWA Request!");
            }

            Log.WriteEnd("Application_PostAuthenticateRequest");
        }



        private bool IsOwaRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/owa");
        }

        public static void SetAuthenticationExpirationTicket()
        {
            Log.WriteStart("SetAuthenticationExpirationTicket");

            var expirationDateTimeInUtc = DateTime.UtcNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes).AddSeconds(1);
            var authenticationExpirationTicketCookie = new HttpCookie(WebDavAppConfigManager.Instance.AuthTimeoutCookieName);
            
            authenticationExpirationTicketCookie.Value = expirationDateTimeInUtc.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("F0");
            authenticationExpirationTicketCookie.HttpOnly = false; 
            authenticationExpirationTicketCookie.Secure = FormsAuthentication.RequireSSL;

            HttpContext.Current.Response.Cookies.Add(authenticationExpirationTicketCookie);

            Log.WriteEnd("SetAuthenticationExpirationTicket");
        }
    }
}
