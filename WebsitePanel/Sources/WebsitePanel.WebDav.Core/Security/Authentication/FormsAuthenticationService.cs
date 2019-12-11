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
using System.DirectoryServices.AccountManagement;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebsitePanel.EnterpriseServer.Base.HostedSolution;
using WebsitePanel.Server.Utils;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Interfaces.Security;
using WebsitePanel.WebDav.Core.Security.Authentication.Principals;
using WebsitePanel.WebDav.Core.Security.Cryptography;
using WebsitePanel.WebDav.Core.Wsp.Framework;

namespace WebsitePanel.WebDav.Core.Security.Authentication
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly ICryptography _cryptography;
        private readonly PrincipalContext _principalContext;

        public FormsAuthenticationService(ICryptography cryptography)
        {
            Log.WriteStart("FormsAuthenticationService");

            _cryptography = cryptography;

            try
            {
                _principalContext = new PrincipalContext(ContextType.Domain, WebDavAppConfigManager.Instance.UserDomain);
            }
            catch (Exception ex)
            {

                Log.WriteError(ex);
            }
            

            Log.WriteEnd("FormsAuthenticationService");
        }

        public WspPrincipal LogIn(string login, string password)
        {
            Log.WriteStart("Login");

            if (ValidateAuthenticationData(login, password) == false)
            {
                return null;
            }

            var principal = new WspPrincipal(login);
            
            var exchangeAccount = WSP.Services.ExchangeServer.GetAccountByAccountNameWithoutItemId(login);
            var organization = WSP.Services.Organizations.GetOrganization(exchangeAccount.ItemId);

            principal.AccountId = exchangeAccount.AccountId;
            principal.ItemId = exchangeAccount.ItemId;
            principal.OrganizationId = organization.OrganizationId;
            principal.DisplayName = exchangeAccount.DisplayName;
            principal.AccountName = exchangeAccount.AccountName;
            principal.EncryptedPassword = _cryptography.Encrypt(password);

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            Thread.CurrentPrincipal = principal;

            Log.WriteEnd("Login");

            return principal;
        }

        public void CreateAuthenticationTicket(WspPrincipal principal)
        {
            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(principal);

            var authTicket = new FormsAuthenticationTicket(1, principal.Identity.Name, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout),
                FormsAuthentication.SlidingExpiration, userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            if (FormsAuthentication.SlidingExpiration)
            {
                cookie.Expires = authTicket.Expiration;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool ValidateAuthenticationData(string login, string password)
        {
            Log.WriteStart("ValidateAuthenticationData");

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = UserPrincipal.FindByIdentity(_principalContext, IdentityType.UserPrincipalName, login);

            if (user == null || _principalContext.ValidateCredentials(login, password) == false)
            {
                return false;
            }

            Log.WriteEnd("ValidateAuthenticationData");

            return true;
        }
    }
}
