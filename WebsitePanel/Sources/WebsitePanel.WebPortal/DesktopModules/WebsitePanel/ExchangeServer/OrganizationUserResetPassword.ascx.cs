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
using WebsitePanel.Providers.HostedSolution;
using WSP = WebsitePanel.EnterpriseServer;

namespace WebsitePanel.Portal.ExchangeServer
{
    public partial class OrganizationUserResetPassword : WebsitePanelModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSettings();
            }
        }

        private void BindSettings()
        {
            OrganizationUser user = ES.Services.Organizations.GetUserGeneralSettings(PanelRequest.ItemID,
                PanelRequest.AccountID);

            litDisplayName.Text = PortalAntiXSS.Encode(user.DisplayName);

            txtEmailAddress.Text = user.PrimaryEmailAddress;

            txtMobile.Text = user.MobilePhone;

            var settings = ES.Services.System.GetSystemSettingsActive(WSP.SystemSettings.TWILIO_SETTINGS, false);

            bool twilioEnabled = settings != null 
                && !string.IsNullOrEmpty(settings.GetValueOrDefault(WSP.SystemSettings.TWILIO_ACCOUNTSID_KEY, string.Empty))
                && !string.IsNullOrEmpty(settings.GetValueOrDefault(WSP.SystemSettings.TWILIO_AUTHTOKEN_KEY, string.Empty))
                && !string.IsNullOrEmpty(settings.GetValueOrDefault(WSP.SystemSettings.TWILIO_PHONEFROM_KEY, string.Empty));

            rbtnMobile.Visible = twilioEnabled;
        }

        protected void btnResetPassoword_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (rbtnEmail.Checked)
            {
                ES.Services.Organizations.SendResetUserPasswordEmail(PanelRequest.ItemID, PanelRequest.AccountID,
                    txtReason.Text, txtEmailAddress.Text, true);
            }
            else
            {
                var result = ES.Services.Organizations.SendResetUserPasswordLinkSms(PanelRequest.ItemID,
                    PanelRequest.AccountID, txtReason.Text, txtMobile.Text);

                if (!result.IsSuccess)
                {
                    ShowErrorMessage("SEND_USER_PASSWORD_RESET_SMS");

                    return;
                }

                if (chkSaveAsMobile.Checked)
                {
                    OrganizationUser user = ES.Services.Organizations.GetUserGeneralSettings(PanelRequest.ItemID,
                        PanelRequest.AccountID);

                    ES.Services.Organizations.SetUserGeneralSettings(
                        PanelRequest.ItemID, PanelRequest.AccountID,
                        user.DisplayName,
                        string.Empty,
                        false,
                        user.Disabled,
                        user.Locked,

                        user.FirstName,
                        user.Initials,
                        user.LastName,

                        user.Address,
                        user.City,
                        user.State,
                        user.Zip,
                        user.Country,

                        user.JobTitle,
                        user.Company,
                        user.Department,
                        user.Office,
                        user.Manager == null ? null : user.Manager.AccountName,

                        user.BusinessPhone,
                        user.Fax,
                        user.HomePhone,
                        txtMobile.Text,
                        user.Pager,
                        user.WebPage,
                        user.Notes,
                        user.ExternalEmail,
                        user.SubscriberNumber,
                        user.LevelId,
                        user.IsVIP,
                        user.UserMustChangePassword);
                }
            }

            Response.Redirect(PortalUtils.EditUrl("ItemID", PanelRequest.ItemID.ToString(),
                (PanelRequest.Context == "Mailbox") ? "mailboxes" : "users",
                "SpaceID=" + PanelSecurity.PackageId));
        }

        protected void SendToGroupCheckedChanged(object sender, EventArgs e)
        {
            EmailRow.Visible = rbtnEmail.Checked;
            MobileRow.Visible = !rbtnEmail.Checked;
        }
    }
}
