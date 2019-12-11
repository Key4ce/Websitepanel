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

using System.Collections.Generic;
using System.Linq;
using WebsitePanel.EnterpriseServer.Base.HostedSolution;
using WebsitePanel.WebDav.Core.Config.WebConfigSections;
using WebsitePanel.WebDavPortal.WebConfigSections;

namespace WebsitePanel.WebDav.Core.Config.Entities
{
    public class SessionKeysCollection : AbstractConfigCollection
    {
        private readonly IEnumerable<SessionKeysElement> _sessionKeys;

        public SessionKeysCollection()
        {
            _sessionKeys = ConfigSection.SessionKeys.Cast<SessionKeysElement>();
        }

        public string AuthTicket
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.AuthTicketKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string WebDavManager
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.WebDavManagerKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string UserGroupsKey
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.UserGroupsKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string OwaEditFoldersSessionKey
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.OwaEditFoldersSessionKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string WebDavRootFoldersPermissions
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.WebDavRootFolderPermissionsKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string PasswordResetSmsKey
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.PasswordResetSmsKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string AccountIdKey
        {
            get
            {
                SessionKeysElement sessionKey =
                    _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.AccountIdKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string ResourseRenderCount
        {
            get
            {
                SessionKeysElement sessionKey = _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.ResourseRenderCountKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }

        public string ItemId
        {
            get
            {
                SessionKeysElement sessionKey = _sessionKeys.FirstOrDefault(x => x.Key == SessionKeysElement.ItemIdSessionKey);
                return sessionKey != null ? sessionKey.Value : null;
            }
        }
    }
}
