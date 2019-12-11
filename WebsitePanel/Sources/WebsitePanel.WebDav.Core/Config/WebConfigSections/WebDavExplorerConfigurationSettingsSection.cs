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
using WebsitePanel.WebDav.Core.Config.WebConfigSections;

namespace WebsitePanel.WebDavPortal.WebConfigSections
{
    public class WebDavExplorerConfigurationSettingsSection : ConfigurationSection
    {
        private const string UserDomainKey = "userDomain";
        private const string WebdavRootKey = "webdavRoot";
        private const string AuthTimeoutCookieNameKey = "authTimeoutCookieName";
        private const string AppName = "applicationName";
        private const string EnterpriseServerUrlNameKey = "enterpriseServer";
        private const string WebsitePanelConstantUserKey = "websitePanelConstantUser";
        private const string ElementsRenderingKey = "elementsRendering";
        private const string Rfc2898CryptographyKey = "rfc2898Cryptography";
        private const string ConnectionStringsKey = "appConnectionStrings";
        private const string SessionKeysKey = "sessionKeys";
        private const string FileIconsKey = "fileIcons";
        private const string OwaSupportedBrowsersKey = "owaSupportedBrowsers";
        private const string OfficeOnlineKey = "officeOnline";
        private const string FilesToIgnoreKey = "filesToIgnore";
        private const string TypeOpenerKey = "typeOpener";
        private const string TwilioKey = "twilio";

        public const string SectionName = "webDavExplorerConfigurationSettings";

        [ConfigurationProperty(AuthTimeoutCookieNameKey, IsRequired = true)]
        public AuthTimeoutCookieNameElement AuthTimeoutCookieName
        {
            get { return (AuthTimeoutCookieNameElement)this[AuthTimeoutCookieNameKey]; }
            set { this[AuthTimeoutCookieNameKey] = value; }
        }

        [ConfigurationProperty(EnterpriseServerUrlNameKey, IsRequired = true)]
        public EnterpriseServerElement EnterpriseServerUrl
        {
            get { return (EnterpriseServerElement)this[EnterpriseServerUrlNameKey]; }
            set { this[EnterpriseServerUrlNameKey] = value; }
        }

        [ConfigurationProperty(WebdavRootKey, IsRequired = true)]
        public WebdavRootElement WebdavRoot
        {
            get { return (WebdavRootElement)this[WebdavRootKey]; }
            set { this[WebdavRootKey] = value; }
        }

        [ConfigurationProperty(UserDomainKey, IsRequired = true)]
        public UserDomainElement UserDomain
        {
            get { return (UserDomainElement) this[UserDomainKey]; }
            set { this[UserDomainKey] = value; }
        }

        [ConfigurationProperty(AppName, IsRequired = true)]
        public ApplicationNameElement ApplicationName
        {
            get { return (ApplicationNameElement)this[AppName]; }
            set { this[AppName] = value; }
        }

        [ConfigurationProperty(WebsitePanelConstantUserKey, IsRequired = true)]
        public WebsitePanelConstantUserElement WebsitePanelConstantUser
        {
            get { return (WebsitePanelConstantUserElement)this[WebsitePanelConstantUserKey]; }
            set { this[WebsitePanelConstantUserKey] = value; }
        }

        [ConfigurationProperty(TwilioKey, IsRequired = true)]
        public TwilioElement Twilio
        {
            get { return (TwilioElement)this[TwilioKey]; }
            set { this[TwilioKey] = value; }
        }

        [ConfigurationProperty(ElementsRenderingKey, IsRequired = true)]
        public ElementsRenderingElement ElementsRendering
        {
            get { return (ElementsRenderingElement)this[ElementsRenderingKey]; }
            set { this[ElementsRenderingKey] = value; }
        }

        [ConfigurationProperty(SessionKeysKey, IsDefaultCollection = false)]
        public SessionKeysElementCollection SessionKeys
        {
            get { return (SessionKeysElementCollection) this[SessionKeysKey]; }
            set { this[SessionKeysKey] = value; }
        }

        [ConfigurationProperty(FileIconsKey, IsDefaultCollection = false)]
        public FileIconsElementCollection FileIcons
        {
            get { return (FileIconsElementCollection) this[FileIconsKey]; }
            set { this[FileIconsKey] = value; }
        }

        [ConfigurationProperty(OwaSupportedBrowsersKey, IsDefaultCollection = false)]
        public OwaSupportedBrowsersElementCollection OwaSupportedBrowsers
        {
            get { return (OwaSupportedBrowsersElementCollection)this[OwaSupportedBrowsersKey]; }
            set { this[OwaSupportedBrowsersKey] = value; }
        }

        [ConfigurationProperty(OfficeOnlineKey, IsDefaultCollection = false)]
        public OfficeOnlineElementCollection OfficeOnline
        {
            get { return (OfficeOnlineElementCollection)this[OfficeOnlineKey]; }
            set { this[OfficeOnlineKey] = value; }
        }

        [ConfigurationProperty(TypeOpenerKey, IsDefaultCollection = false)]
        public OpenerElementCollection TypeOpener
        {
            get { return (OpenerElementCollection)this[TypeOpenerKey]; }
            set { this[TypeOpenerKey] = value; }
        }

        [ConfigurationProperty(FilesToIgnoreKey, IsDefaultCollection = false)]
        public FilesToIgnoreElementCollection FilesToIgnore
        {
            get { return (FilesToIgnoreElementCollection)this[FilesToIgnoreKey]; }
            set { this[FilesToIgnoreKey] = value; }
        }
    }
}
