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

using Ninject;
using System.Web.SessionState;
using WebsitePanel.WebDav.Core.Interfaces.Managers;
using WebsitePanel.WebDav.Core.Interfaces.Managers.Users;
using WebsitePanel.WebDav.Core.Interfaces.Owa;
using WebsitePanel.WebDav.Core.Interfaces.Security;
using WebsitePanel.WebDav.Core.Interfaces.Services;
using WebsitePanel.WebDav.Core.Interfaces.Storages;
using WebsitePanel.WebDav.Core.Managers;
using WebsitePanel.WebDav.Core.Managers.Users;
using WebsitePanel.WebDav.Core.Owa;
using WebsitePanel.WebDav.Core.Security.Authentication;
using WebsitePanel.WebDav.Core.Security.Authorization;
using WebsitePanel.WebDav.Core.Security.Cryptography;
using WebsitePanel.WebDav.Core.Services;
using WebsitePanel.WebDav.Core.Storages;
using WebsitePanel.WebDavPortal.DependencyInjection.Providers;

namespace WebsitePanel.WebDavPortal.DependencyInjection
{
    public class PortalDependencies
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<HttpSessionState>().ToProvider<HttpSessionStateProvider>();
            kernel.Bind<ICryptography>().To<CryptoUtils>();
            kernel.Bind<IAuthenticationService>().To<FormsAuthenticationService>();
            kernel.Bind<IWebDavManager>().To<WebDavManager>();
            kernel.Bind<IAccessTokenManager>().To<AccessTokenManager>();
            kernel.Bind<IWopiServer>().To<WopiServer>();
            kernel.Bind<IWopiFileManager>().To<CobaltSessionManager>();
            kernel.Bind<IWebDavAuthorizationService>().To<WebDavAuthorizationService>();
            kernel.Bind<ICobaltManager>().To<CobaltManager>();
            kernel.Bind<ITtlStorage>().To<CacheTtlStorage>();
            kernel.Bind<IUserSettingsManager>().To<UserSettingsManager>();
            kernel.Bind<ISmsDistributionService>().To<TwillioSmsDistributionService>();
            kernel.Bind<ISmsAuthenticationService>().To<SmsAuthenticationService>();
        }
    }
}
