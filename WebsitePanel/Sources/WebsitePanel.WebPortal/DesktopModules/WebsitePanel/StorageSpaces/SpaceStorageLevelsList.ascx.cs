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
using WebsitePanel.Providers.Common;
using WebsitePanel.Providers.StorageSpaces;

namespace WebsitePanel.Portal.StorageSpaces
{
    public partial class SpaceStorageLevelsList : WebsitePanelModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvSsLevels.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
                gvSsLevels.Sort("Name", System.Web.UI.WebControls.SortDirection.Ascending);
            }
        }

        protected void odsRDSServersPaged_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                ProcessException(e.Exception.InnerException);
                this.DisableControls = true;
                e.ExceptionHandled = true;
            }
        }

        

        protected void gvSsLevels_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int id;
                bool hasValue = int.TryParse(e.CommandArgument.ToString(), out id);

                ResultObject result = new ResultObject();
                result.IsSuccess = false;

                if (hasValue)
                {
                    result = ES.Services.StorageSpaces.RemoveStorageSpaceLevel(id);
                }

                messageBox.ShowMessage(result, "STORAGE_SPACES_LEVEL_REMOVE", null);

                if (!result.IsSuccess)
                {
                    return;
                }

                gvSsLevels.DataBind();
            }
            else if (e.CommandName == "EditSsLevel")
            {
                EditSsLevel(e.CommandArgument.ToString());
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSsLevels.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);

            gvSsLevels.DataBind();
        }

        protected void btnAddSsLevel_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("SsLevelId", (-1).ToString(), "add_storage_space_level"));
        }

        private void EditSsLevel(string levelId)
        {
            Response.Redirect(EditUrl("SsLevelId", levelId, "edit_storage_space_level"));
        }

        protected bool CheckLevelIsInUse(int levelId)
        {
            return ES.Services.StorageSpaces.GetStorageSpacesByLevelId(levelId).Any();
        }
    }
}
