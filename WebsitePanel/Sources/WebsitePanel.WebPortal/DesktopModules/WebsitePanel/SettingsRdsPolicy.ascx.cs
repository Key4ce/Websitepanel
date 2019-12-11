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
using WebsitePanel.EnterpriseServer.Base.RDS;

namespace WebsitePanel.Portal
{
    public partial class SettingsRdsPolicy : WebsitePanelControlBase, IUserSettingsEditorControl
    {        
        public void BindSettings(UserSettings settings)
        {
            var timeouts = RdsServerSettings.ScreenSaverTimeOuts;
            ddTimeout.DataSource = timeouts;
            ddTimeout.DataTextField = "Value";
            ddTimeout.DataValueField = "Key";
            ddTimeout.DataBind();

            ddTimeout.SelectedValue = settings[RdsServerSettings.LOCK_SCREEN_TIMEOUT_VALUE];
            cbTimeoutAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.LOCK_SCREEN_TIMEOUT_ADMINISTRATORS]);
            cbTimeoutUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.LOCK_SCREEN_TIMEOUT_USERS]);

            cbRunCommandAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.REMOVE_RUN_COMMAND_ADMINISTRATORS]);
            cbRunCommandUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.REMOVE_RUN_COMMAND_USERS]);

            cbPowershellAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.REMOVE_POWERSHELL_COMMAND_ADMINISTRATORS]);
            cbPowershellUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.REMOVE_POWERSHELL_COMMAND_USERS]);

            cbHideCDriveAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.HIDE_C_DRIVE_ADMINISTRATORS]);
            cbHideCDriveUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.HIDE_C_DRIVE_USERS]);

            cbShutdownAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.REMOVE_SHUTDOWN_RESTART_ADMINISTRATORS]);
            cbShutdownUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.REMOVE_SHUTDOWN_RESTART_USERS]);

            cbTaskManagerAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.DISABLE_TASK_MANAGER_ADMINISTRATORS]);
            cbTaskManagerUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.DISABLE_TASK_MANAGER_USERS]);

            cbDesktopAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.CHANGE_DESKTOP_DISABLED_ADMINISTRATORS]);
            cbDesktopUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.CHANGE_DESKTOP_DISABLED_USERS]);

            cbScreenSaverAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.SCREEN_SAVER_DISABLED_ADMINISTRATORS]);
            cbScreenSaverUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.SCREEN_SAVER_DISABLED_USERS]);

            cbViewSessionAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.RDS_VIEW_WITHOUT_PERMISSION_ADMINISTRATORS]);
            cbViewSessionUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.RDS_VIEW_WITHOUT_PERMISSION_Users]);
            cbControlSessionAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.RDS_CONTROL_WITHOUT_PERMISSION_ADMINISTRATORS]);
            cbControlSessionUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.RDS_CONTROL_WITHOUT_PERMISSION_Users]);

            cbDisableCmdAdministrators.Checked = Convert.ToBoolean(settings[RdsServerSettings.DISABLE_CMD_ADMINISTRATORS]);
            cbDisableCmdUsers.Checked = Convert.ToBoolean(settings[RdsServerSettings.DISABLE_CMD_USERS]);

            ddTreshold.SelectedValue = settings[RdsServerSettings.DRIVE_SPACE_THRESHOLD_VALUE];            
        }

        public void SaveSettings(UserSettings settings)
        {
            settings[RdsServerSettings.LOCK_SCREEN_TIMEOUT_VALUE] = ddTimeout.SelectedValue;
            settings[RdsServerSettings.LOCK_SCREEN_TIMEOUT_ADMINISTRATORS] = cbTimeoutAdministrators.Checked.ToString();
            settings[RdsServerSettings.LOCK_SCREEN_TIMEOUT_USERS] = cbTimeoutUsers.Checked.ToString();
            settings[RdsServerSettings.REMOVE_RUN_COMMAND_ADMINISTRATORS] = cbRunCommandAdministrators.Checked.ToString();
            settings[RdsServerSettings.REMOVE_RUN_COMMAND_USERS] = cbRunCommandUsers.Checked.ToString();
            settings[RdsServerSettings.REMOVE_POWERSHELL_COMMAND_ADMINISTRATORS] = cbPowershellAdministrators.Checked.ToString();
            settings[RdsServerSettings.REMOVE_POWERSHELL_COMMAND_USERS] = cbPowershellUsers.Checked.ToString();
            settings[RdsServerSettings.HIDE_C_DRIVE_ADMINISTRATORS] = cbHideCDriveAdministrators.Checked.ToString();
            settings[RdsServerSettings.HIDE_C_DRIVE_USERS] = cbHideCDriveUsers.Checked.ToString();
            settings[RdsServerSettings.REMOVE_SHUTDOWN_RESTART_ADMINISTRATORS] = cbShutdownAdministrators.Checked.ToString();
            settings[RdsServerSettings.REMOVE_SHUTDOWN_RESTART_USERS] = cbShutdownUsers.Checked.ToString();
            settings[RdsServerSettings.DISABLE_TASK_MANAGER_ADMINISTRATORS] = cbTaskManagerAdministrators.Checked.ToString();
            settings[RdsServerSettings.DISABLE_TASK_MANAGER_USERS] = cbTaskManagerUsers.Checked.ToString();
            settings[RdsServerSettings.CHANGE_DESKTOP_DISABLED_ADMINISTRATORS] = cbDesktopAdministrators.Checked.ToString();
            settings[RdsServerSettings.CHANGE_DESKTOP_DISABLED_USERS] = cbDesktopUsers.Checked.ToString();
            settings[RdsServerSettings.SCREEN_SAVER_DISABLED_ADMINISTRATORS] = cbScreenSaverAdministrators.Checked.ToString();
            settings[RdsServerSettings.SCREEN_SAVER_DISABLED_USERS] = cbScreenSaverUsers.Checked.ToString();
            settings[RdsServerSettings.DRIVE_SPACE_THRESHOLD_VALUE] = ddTreshold.SelectedValue;
            settings[RdsServerSettings.RDS_VIEW_WITHOUT_PERMISSION_ADMINISTRATORS] = cbViewSessionAdministrators.Checked.ToString();
            settings[RdsServerSettings.RDS_VIEW_WITHOUT_PERMISSION_Users] = cbViewSessionUsers.Checked.ToString();
            settings[RdsServerSettings.RDS_CONTROL_WITHOUT_PERMISSION_ADMINISTRATORS] = cbControlSessionAdministrators.Checked.ToString();
            settings[RdsServerSettings.RDS_CONTROL_WITHOUT_PERMISSION_Users] = cbControlSessionUsers.Checked.ToString();
            settings[RdsServerSettings.DISABLE_CMD_ADMINISTRATORS] = cbDisableCmdAdministrators.Checked.ToString();
            settings[RdsServerSettings.DISABLE_CMD_USERS] = cbDisableCmdUsers.Checked.ToString();            
        }
    }
}
