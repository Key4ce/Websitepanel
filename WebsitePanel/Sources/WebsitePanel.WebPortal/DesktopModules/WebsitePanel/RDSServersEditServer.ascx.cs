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

namespace WebsitePanel.Portal
{
    public partial class RDSServersEditServer : WebsitePanelModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rdsServer = ES.Services.RDS.GetRdsServer(PanelRequest.ServerId);
                lblServerName.Text = rdsServer.FqdName;
                txtServerComments.Text = rdsServer.Description;

                var serverInfo = ES.Services.RDS.GetRdsServerInfo(null, rdsServer.FqdName);
                litProcessor.Text = string.Format("{0}x{1} MHz", serverInfo.NumberOfCores, serverInfo.MaxClockSpeed);
                litLoadPercentage.Text = string.Format("{0}%", serverInfo.LoadPercentage);
                litMemoryAllocated.Text = string.Format("{0} MB", serverInfo.MemoryAllocatedMb);
                litFreeMemory.Text = string.Format("{0} MB", serverInfo.FreeMemoryMb);
                litStatus.Text = serverInfo.Status;
                rpServerDrives.DataSource = serverInfo.Drives;
                rpServerDrives.DataBind(); 
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            try
            {
                var rdsServer = ES.Services.RDS.GetRdsServer(PanelRequest.ServerId);                
                rdsServer.Description = txtServerComments.Text;

                ResultObject result = ES.Services.RDS.UpdateRdsServer(rdsServer);

                if (!result.IsSuccess && result.ErrorCodes.Count > 0)
                {
                    messageBox.ShowMessage(result, "RDSSERVER_NOT_UPDATED", "");
                    return;
                }

                RedirectToBrowsePage();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("RDSSERVER_NOT_UPDATED", ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RedirectToBrowsePage();
        }
    }
}
