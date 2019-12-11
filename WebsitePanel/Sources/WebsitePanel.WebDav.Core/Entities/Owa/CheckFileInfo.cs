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

using System.Runtime.Serialization;

namespace WebsitePanel.WebDav.Core.Entities.Owa
{
    [DataContract]
    public class CheckFileInfo
    {
        [DataMember]
        public string BaseFileName { get; set; }
        [DataMember]
        public string OwnerId { get; set; }
        [DataMember]
        public long Size { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public bool SupportsCoauth { get; set; }
        [DataMember]
        public bool SupportsCobalt { get; set; }
        [DataMember]
        public bool SupportsFolders { get; set; }
        [DataMember]
        public bool SupportsLocks { get; set; }
        [DataMember]
        public bool SupportsScenarioLinks { get; set; }
        [DataMember]
        public bool SupportsSecureStore { get; set; }
        [DataMember]
        public bool SupportsUpdate { get; set; }
        [DataMember]
        public bool UserCanWrite { get; set; }
        [DataMember]
        public string DownloadUrl { get; set; }
        [DataMember]
        public bool ReadOnly { get; set; }
        [DataMember]
        public bool RestrictedWebViewOnly { get; set; }
        [DataMember]
        public string ClientUrl { get; set; }
        [DataMember]
        public bool CloseButtonClosesWindow { get; set; }
        //[DataMember]
        //public string CloseUrl { get; set; }
        //[DataMember]
        //public bool UserCanNotWriteRelative { get; set; }
        

        //[DataMember]
        //public string SHA256 { get; set; }
        //[DataMember]
        //public bool AllowExternalMarketplace { get; set; }
        //[DataMember]
        //public string BreadcrumbBrandName { get; set; }
        //[DataMember]
        //public string BreadcrumbBrandUrl { get; set; }
        //[DataMember]
        //public string BreadcrumbDocName { get; set; }
        //[DataMember]
        //public string BreadcrumbDocUrl { get; set; }
        //[DataMember]
        //public string BreadcrumbFolderName { get; set; }
        //[DataMember]
        //public string BreadcrumbFolderUrl { get; set; }
        //[DataMember]
        //public string ClientUrl { get; set; }
    
        //[DataMember]
        //public string CloseUrl { get; set; }
        //[DataMember]
        //public bool DisableBrowserCachingOfUserContent { get; set; }
        //[DataMember]
        //public bool DisablePrint { get; set; }
        //[DataMember]
        //public bool DisableTranslation { get; set; }
        //[DataMember]
        //public string FileSharingUrl { get; set; }
        //[DataMember]
        //public string FileUrl { get; set; }
        //[DataMember]
        //public string HostAuthenticationId { get; set; }
        //[DataMember]
        //public string HostEditUrl { get; set; }
        //[DataMember]
        //public string HostEmbeddedEditUrl { get; set; }
        //[DataMember]
        //public string HostEmbeddedViewUrl { get; set; }
        //[DataMember]
        //public string HostName { get; set; }
        //[DataMember]
        //public string HostNotes { get; set; }
        //[DataMember]
        //public string HostRestUrl { get; set; }
        //[DataMember]
        //public string HostViewUrl { get; set; }
        //[DataMember]
        //public string IrmPolicyDescription { get; set; }
        //[DataMember]
        //public string IrmPolicyTitle { get; set; }

        //[DataMember]
        //public string PresenceProvider { get; set; }
        //[DataMember]
        //public string PresenceUserId { get; set; }
        //[DataMember]
        //public string PrivacyUrl { get; set; }
        //[DataMember]
        //public bool ProtectInClient { get; set; }
        //[DataMember]
        //public bool ReadOnly { get; set; }


        //[DataMember]
        //public string SignoutUrl { get; set; }


        //[DataMember]
        //public string TenantId { get; set; }
        //[DataMember]
        //public string TermsOfUseUrl { get; set; }
        //[DataMember]
        //public string TimeZone { get; set; }
        //[DataMember]
        //public bool UserCanAttend { get; set; }

        //[DataMember]
        //public bool UserCanPresent { get; set; }
        //[DataMember]
        //public bool UserCanWrite { get; set; }
        //[DataMember]
        //public string UserFriendlyName { get; set; }
        //[DataMember]
        //public string UserId { get; set; }

        //[DataMember]
        //public bool WebEditingDisabled { get; set; }
    }
}
