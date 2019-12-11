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
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cobalt;
using WebsitePanel.EnterpriseServer.Base.HostedSolution;
using WebsitePanel.WebDav.Core.Client;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Entities.Owa;
using WebsitePanel.WebDav.Core.Interfaces.Managers;
using WebsitePanel.WebDav.Core.Interfaces.Owa;
using WebsitePanel.WebDav.Core.Interfaces.Security;
using WebsitePanel.WebDav.Core.Security.Authentication.Principals;
using WebsitePanel.WebDav.Core.Security.Authorization.Enums;

namespace WebsitePanel.WebDav.Core.Owa
{
    public class WopiServer : IWopiServer
    {
        private readonly IWebDavManager _webDavManager;
        private readonly IAccessTokenManager _tokenManager;
        private readonly IWebDavAuthorizationService _webDavAuthorizationService;
        private readonly IWopiFileManager _fileManager;


        public WopiServer(IWebDavManager webDavManager, IAccessTokenManager tokenManager, IWebDavAuthorizationService webDavAuthorizationService, IWopiFileManager fileManager)
        {
            _webDavManager = webDavManager;
            _tokenManager = tokenManager;
            _webDavAuthorizationService = webDavAuthorizationService;
            _fileManager = fileManager;
        }

        public CheckFileInfo GetCheckFileInfo(WebDavAccessToken token)
        {
            var resource = _webDavManager.GetResource(token.FilePath);

            var permissions = _webDavAuthorizationService.GetPermissions(WspContext.User, token.FilePath);

            var readOnly = permissions.HasFlag(WebDavPermissions.Write) == false || permissions.HasFlag(WebDavPermissions.OwaEdit) == false;

            var cFileInfo = new CheckFileInfo
            {
                BaseFileName = resource == null ? token.FilePath.Split('/').Last() : resource.DisplayName.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
                OwnerId = WspContext.User.Login,
                Size = resource == null ? 0 : resource.ContentLength,
                Version = DateTime.Now.ToString("s"),
                SupportsCoauth = true,
                SupportsCobalt = true,
                SupportsFolders = true,
                SupportsLocks = true,
                SupportsScenarioLinks = false,
                SupportsSecureStore = false,
                SupportsUpdate = true,
                UserCanWrite = !readOnly,
                ReadOnly = readOnly,
                RestrictedWebViewOnly = false,
                CloseButtonClosesWindow = true
            };

            if (resource != null)
            {
                cFileInfo.ClientUrl = _webDavManager.GetFileUrl(token.FilePath);
            }

            return cFileInfo;
        }

        public byte[] GetFileBytes(int accessTokenId)
        {
            var token = _tokenManager.GetToken(accessTokenId);

            if (_webDavManager.FileExist(token.FilePath))
            {
                return _webDavManager.GetFileBytes(token.FilePath);
            }

            var cobaltFile = _fileManager.Get(token.FilePath) ?? _fileManager.Create(accessTokenId);

            var stream = new MemoryStream();

            new GenericFda(cobaltFile.CobaltEndpoint, null).GetContentStream().CopyTo(stream);

            return stream.ToArray();
        }
    }
}
