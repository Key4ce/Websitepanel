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
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebsitePanel.EnterpriseServer;
using WebsitePanel.Providers.HostedSolution;

namespace WebsitePanel.Portal.ExchangeServer
{
    public partial class OrganizationSettingsPasswordSettings : WebsitePanelModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Organization org = ES.Services.Organizations.GetOrganization(PanelRequest.ItemID);
                litOrganizationName.Text = org.OrganizationId;

                BindSettings();
            }

        }

        private void BindSettings()
        {
            var settings = ES.Services.Organizations.GetOrganizationPasswordSettings(PanelRequest.ItemID);

            if (settings == null)
            {
                var defaultSettings = ES.Services.Users.GetUserSettings(PanelSecurity.LoggedUserId, UserSettings.EXCHANGE_POLICY);

                BindDefaultSettings(defaultSettings[UserSettings.HOSTED_ORGANIZATION_PASSWORD_POLICY]);
            }
            else
            {
                BindSettings(settings);
            }

            ToggleLockoutControls(chkLockOutSettigns.Checked);
            ToggleComplexityControls(chkPasswordComplexity.Checked);
        }

        private void BindDefaultSettings(string defaultSettings)
        {
            // parse settings
            string[] parts = defaultSettings.Split(';');
            txtMinimumLength.Text = parts[1];
            txtMaximumLength.Text = parts[2];
            txtMinimumUppercase.Text = parts[3];
            txtMinimumNumbers.Text = parts[4];
            txtMinimumSymbols.Text = parts[5];
            chkNotEqualUsername.Checked = Utils.ParseBool(parts[6], false);
            txtLockedOut.Text = parts[7];

            txtEnforcePasswordHistory.Text = PasswordPolicyEditor.GetValueSafe(parts, 8, "0");
            txtAccountLockoutDuration.Text = PasswordPolicyEditor.GetValueSafe(parts, 9, "0");
            txtResetAccountLockout.Text = PasswordPolicyEditor.GetValueSafe(parts, 10, "0");
            chkLockOutSettigns.Checked = PasswordPolicyEditor.GetValueSafe(parts, 11, false);
            chkPasswordComplexity.Checked = PasswordPolicyEditor.GetValueSafe(parts, 12, true);

            txtMaxPasswordAge.Text = PasswordPolicyEditor.GetValueSafe(parts, 13, "42");
        }

        private void BindSettings(OrganizationPasswordSettings settings)
        {
            txtMinimumLength.Text = settings.MinimumLength.ToString();
            txtMaximumLength.Text = settings.MaximumLength.ToString();
            txtMinimumUppercase.Text = settings.UppercaseLettersCount.ToString();
            txtMinimumNumbers.Text = settings.NumbersCount.ToString();
            txtMinimumSymbols.Text = settings.SymbolsCount.ToString();
            txtLockedOut.Text = settings.AccountLockoutThreshold.ToString();

            txtEnforcePasswordHistory.Text = settings.EnforcePasswordHistory.ToString();
            txtAccountLockoutDuration.Text = settings.AccountLockoutDuration.ToString();
            txtResetAccountLockout.Text = settings.ResetAccountLockoutCounterAfter.ToString();
            chkLockOutSettigns.Checked = settings.LockoutSettingsEnabled;
            chkPasswordComplexity.Checked = settings.PasswordComplexityEnabled;

            txtMaxPasswordAge.Text = settings.MaxPasswordAge.ToString();
        }

        private OrganizationPasswordSettings GetSettings()
        {
            var settings = new OrganizationPasswordSettings();

            settings.MinimumLength = Utils.ParseInt(txtMinimumLength.Text, 3);
            settings.MaximumLength = Utils.ParseInt(txtMaximumLength.Text, 7);
            settings.UppercaseLettersCount = Utils.ParseInt(txtMinimumUppercase.Text, 3);
            settings.NumbersCount = Utils.ParseInt(txtMinimumNumbers.Text, 3);
            settings.SymbolsCount = Utils.ParseInt(txtMinimumSymbols.Text, 3);
            settings.AccountLockoutThreshold = Utils.ParseInt(txtLockedOut.Text, 3);
            settings.EnforcePasswordHistory = Utils.ParseInt(txtEnforcePasswordHistory.Text, 3);
            settings.AccountLockoutDuration = Utils.ParseInt(txtAccountLockoutDuration.Text, 3);
            settings.ResetAccountLockoutCounterAfter = Utils.ParseInt(txtResetAccountLockout.Text, 3);

            settings.LockoutSettingsEnabled = chkLockOutSettigns.Checked;
            settings.PasswordComplexityEnabled =chkPasswordComplexity.Checked;

            settings.MaxPasswordAge = Utils.ParseInt(txtMaxPasswordAge.Text, 42);

            return settings;
        }


        private bool SavePasswordSettings()
        {
            try
            {
                ES.Services.Organizations.UpdateOrganizationPasswordSettings(PanelRequest.ItemID, GetSettings());
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ORANIZATIONSETTINGS_NOT_UPDATED", ex);
                return false;
            }

            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            SavePasswordSettings();
        }

        private void ToggleLockoutControls(bool visible)
        {
            RowAccountLockoutDuration.Visible = visible;
            RowLockedOut.Visible = visible;
            RowResetAccountLockout.Visible = visible;
        }

        protected void chkLockOutSettigns_CheckedChanged(object sender, EventArgs e)
        {
            ToggleLockoutControls(chkLockOutSettigns.Checked);
        }

        private void ToggleComplexityControls(bool visible)
        {
            RowMinimumUppercase.Visible = visible;
            RowMinimumNumbers.Visible = visible;
            RowMinimumSymbols.Visible = visible;
        }

        protected void chkPasswordComplexity_CheckedChanged(object sender, EventArgs e)
        {
            ToggleComplexityControls(chkPasswordComplexity.Checked);
        }

        protected void btnSaveExit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (SavePasswordSettings())
            {
                Response.Redirect(EditUrl("ItemID", PanelRequest.ItemID.ToString(), "organization_home", "SpaceID=" + PanelSecurity.PackageId));
            }
        }
    }
}
