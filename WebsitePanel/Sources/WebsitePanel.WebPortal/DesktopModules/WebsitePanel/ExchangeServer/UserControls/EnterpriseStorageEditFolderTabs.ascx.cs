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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebsitePanel.Portal.Code.UserControls;

namespace WebsitePanel.Portal.ExchangeServer.UserControls
{
    public partial class EnterpriseStorageEditFolderTabs : WebsitePanelControlBase
    {
        public string SelectedTab { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindTabs();
        }

        private void BindTabs()
        {
            List<Tab> tabsList = new List<Tab>();
            tabsList.Add(CreateTab("enterprisestorage_folder_settings", "Tab.GeneralSettings"));
            tabsList.Add(CreateTab("enterprisestorage_folder_settings_folder_permissions", "Tab.FolderPermissions"));
            tabsList.Add(CreateTab("enterprisestorage_folder_settings_owa_editing", "Tab.OwaEditPermissions"));

            int idx = 0;

            foreach (Tab tab in tabsList)
            {
                if (String.Compare(tab.Id, SelectedTab, true) == 0)
                {
                    break;
                }

                idx++;
            }

            esTabs.SelectedIndex = idx;
            esTabs.DataSource = tabsList;
            esTabs.DataBind();
        }

        private Tab CreateTab(string id, string text)
        {
            return new Tab(id,GetLocalizedString(text),
                HostModule.EditUrl("AccountID", PanelRequest.AccountID.ToString(), id,
                "SpaceID=" + PanelSecurity.PackageId.ToString(),
                "ItemID=" + PanelRequest.ItemID.ToString(), "FolderID=" + PanelRequest.FolderID));
        }
    }
}
