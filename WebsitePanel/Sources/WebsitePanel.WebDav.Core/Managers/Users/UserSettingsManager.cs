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

using WebsitePanel.WebDav.Core.Entities.Account;
using WebsitePanel.WebDav.Core.Entities.Account.Enums;
using WebsitePanel.WebDav.Core.Helper;
using WebsitePanel.WebDav.Core.Interfaces.Managers.Users;
using WebsitePanel.WebDav.Core.Wsp.Framework;

namespace WebsitePanel.WebDav.Core.Managers.Users
{
    public class UserSettingsManager : IUserSettingsManager
    {
        public UserPortalSettings GetUserSettings(int accountId)
        {
            string xml = WSP.Services.EnterpriseStorage.GetWebDavPortalUserSettingsByAccountId(accountId);

            if (string.IsNullOrEmpty(xml))
            {
                return new UserPortalSettings();
            }

            return SerializeHelper.Deserialize<UserPortalSettings>(xml);
        }

        public void UpdateSettings(int accountId, UserPortalSettings settings)
        {
            var xml = SerializeHelper.Serialize(settings);

            WSP.Services.EnterpriseStorage.UpdateWebDavPortalUserSettings(accountId, xml);
        }

        public void ChangeWebDavViewType(int accountId, FolderViewTypes type)
        {
            var settings = GetUserSettings(accountId);

            settings.WebDavViewType = type;

            UpdateSettings(accountId, settings);
        }
    }
}
