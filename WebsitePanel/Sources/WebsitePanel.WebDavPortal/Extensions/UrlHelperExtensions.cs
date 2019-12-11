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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using WebsitePanel.EnterpriseServer.Base.HostedSolution;
using WebsitePanel.WebDav.Core;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDavPortal.UI.Routes;

namespace WebsitePanel.WebDavPortal.Extensions
{
    public static class UrlHelperExtensions
    {
        public static String GenerateWopiUrl(this System.Web.Mvc.UrlHelper urlHelper, WebDavAccessToken token, string path)
        {
            var urlPart = urlHelper.HttpRouteUrl(OwaRouteNames.CheckFileInfo, new { accessTokenId = token.Id });

            return GenerateWopiUrl(token, urlPart, path);
        }

        public static String GenerateWopiUrl(this UrlHelper urlHelper, WebDavAccessToken token, string path)
        {
            var urlPart = urlHelper.Route(OwaRouteNames.CheckFileInfo, new { accessTokenId = token.Id });

            return GenerateWopiUrl(token, urlPart, path);
        }

        private static string GenerateWopiUrl(WebDavAccessToken token, string  urlPart, string path)
        {
            var url = new Uri(HttpContext.Current.Request.Url, urlPart).ToString();

            string wopiSrc = HttpUtility.UrlDecode(url);

            return string.Format("{0}&access_token={1}", wopiSrc, token.AccessToken.ToString("N"));
        }
    }
}
