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
using WebsitePanel.Providers.StorageSpaces;

namespace WebsitePanel.Portal.StorageSpaces
{
    public partial class EditStorageSpaceLevel : WebsitePanelModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // servers.Module = Module;

            if (!Page.IsPostBack)
            {
                var level = ES.Services.StorageSpaces.GetStorageSpaceLevelById(PanelRequest.SsLevelId);

                if (level != null)
                {
                    txtName.Text = level.Name;
                    txtDescription.Text = level.Description;
                }

                var groups = ES.Services.StorageSpaces.GetLevelResourceGroups(PanelRequest.SsLevelId);

                resourceGroups.SetResourceGroups(groups);
            }
        }

        private bool SaveSsLevel(out int levelId,bool exit = false)
        {
            StorageSpaceLevel level = ES.Services.StorageSpaces.GetStorageSpaceLevelById(PanelRequest.SsLevelId) 
                                      ?? new StorageSpaceLevel();
            var groups = resourceGroups.GetResourceGroups();

            level.Id = PanelRequest.SsLevelId;
            level.Name = txtName.Text;
            level.Description = txtDescription.Text;

            var result = ES.Services.StorageSpaces.SaveStorageSpaceLevel(level, groups);

            levelId = result.Value;

            messageBox.ShowMessage(result, "STORAGE_SPACE_LEVEL_SAVE", null);

            if (!exit)
            {
                resourceGroups.SetResourceGroups(groups);
            }


            return result.IsSuccess;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            int levelId;
            if (SaveSsLevel(out levelId) && PanelRequest.SsLevelId <= 0)
            {
                EditSsLevel(levelId);
            }
        }

        protected void btnSaveExit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            int levelId;
            if (SaveSsLevel(out levelId))
            {
                Response.Redirect(EditUrl(null));
            }
        }

        private void EditSsLevel(int levelId)
        {
            Response.Redirect(EditUrl("SsLevelId", levelId.ToString(), "edit_storage_space_level"));
        }
    }
}
