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
using WebsitePanel.EnterpriseServer;
using WebsitePanel.Providers.HostedSolution;

namespace WebsitePanel.Portal.RDS
{
    public partial class RDSSeupLetter : WebsitePanelModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInstructions();
            }
        }

        private void BindInstructions()
        {            
            int accountId = PanelRequest.AccountID > 0 ? PanelRequest.AccountID : 0;   
            litContent.Text = ES.Services.RDS.GetRdsSetupLetter(PanelRequest.ItemID, accountId);
            PackageInfo package = ES.Services.Packages.GetPackage(PanelSecurity.PackageId);

            if (package == null)
            {
                RedirectSpaceHomePage();
            }

            OrganizationUser account = ES.Services.Organizations.GetUserGeneralSettings(PanelRequest.ItemID, accountId);

            if (account != null)
            {
                txtTo.Text = account.ExternalEmail;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int accountId = PanelRequest.AccountID > 0 ? PanelRequest.AccountID : 0;   
                int result = ES.Services.RDS.SendRdsSetupLetter(PanelRequest.ItemID, accountId, txtTo.Text.Trim(), txtCC.Text.Trim());

                if (result < 0)
                {
                    messageBox.ShowResultMessage(result);
                    return;
                }

                messageBox.ShowSuccessMessage("RDS_SETUP_LETTER_SEND");
            }
            catch (Exception ex)
            {
                messageBox.ShowErrorMessage("RDS_SETUP_LETTER_SEND", ex);
                return;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("SpaceID", PanelSecurity.PackageId.ToString(), "rds_collection_edit_users", "CollectionId=" + PanelRequest.CollectionID, "ItemID=" + PanelRequest.ItemID));
        }
    }
}
