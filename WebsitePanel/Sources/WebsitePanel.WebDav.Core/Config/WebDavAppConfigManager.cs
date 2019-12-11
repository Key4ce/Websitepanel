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

using System.Configuration;
using WebsitePanel.WebDav.Core.Config.Entities;
using WebsitePanel.WebDavPortal.WebConfigSections;

namespace WebsitePanel.WebDav.Core.Config
{
    public class WebDavAppConfigManager : IWebDavAppConfig
    {
        private static WebDavAppConfigManager _instance;
        private readonly WebDavExplorerConfigurationSettingsSection _configSection;

        private WebDavAppConfigManager()
        {
            _configSection = ((WebDavExplorerConfigurationSettingsSection) ConfigurationManager.GetSection(WebDavExplorerConfigurationSettingsSection.SectionName));
            WebsitePanelConstantUserParameters = new WebsitePanelConstantUserParameters();
            ElementsRendering = new ElementsRendering();
            SessionKeys = new SessionKeysCollection();
            FileIcons = new FileIconsDictionary();
            HttpErrors = new HttpErrorsCollection();
            OfficeOnline = new OfficeOnlineCollection();
            OwaSupportedBrowsers = new OwaSupportedBrowsersCollection();
            FilesToIgnore = new FilesToIgnoreCollection();
            FileOpener = new OpenerCollection();
            TwilioParameters = new TwilioParameters();
        }

        public static WebDavAppConfigManager Instance
        {
            get { return _instance ?? (_instance = new WebDavAppConfigManager()); }
        }

        public string UserDomain
        {
            get { return _configSection.UserDomain.Value; }
        }

        public string WebdavRoot
        {
            get { return _configSection.WebdavRoot.Value; }
        }

        public string ApplicationName
        {
            get { return _configSection.ApplicationName.Value; }
        }

        public string AuthTimeoutCookieName
        {
            get { return _configSection.AuthTimeoutCookieName.Value; }
        }

        public string EnterpriseServerUrl
        {
            get { return _configSection.EnterpriseServerUrl.Value; }
        }

        public ElementsRendering ElementsRendering { get; private set; }
        public WebsitePanelConstantUserParameters WebsitePanelConstantUserParameters { get; private set; }
        public TwilioParameters TwilioParameters { get; private set; }
        public SessionKeysCollection SessionKeys { get; private set; }
        public FileIconsDictionary FileIcons { get; private set; }
        public HttpErrorsCollection HttpErrors { get; private set; }
        public OfficeOnlineCollection OfficeOnline { get; private set; }
        public OwaSupportedBrowsersCollection OwaSupportedBrowsers { get; private set; }
        public FilesToIgnoreCollection FilesToIgnore { get; private set; }
        public OpenerCollection FileOpener { get; private set; }
    }
}
