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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace WebsitePanel.Providers.Virtualization
{
    public static class VirtualMachineHelper
    {
        public static OperationalStatus GetVMHeartBeatStatus(PowerShellManager powerShell, string name)
        {
            OperationalStatus status = OperationalStatus.None;

            Command cmd = new Command("Get-VMIntegrationService");

            cmd.Parameters.Add("VMName", name);
            cmd.Parameters.Add("Name", "HeartBeat");

            Collection<PSObject> result = powerShell.Execute(cmd, true);
            if (result != null && result.Count > 0)
            {
                var statusString = result[0].GetProperty("PrimaryOperationalStatus");

                if (statusString != null)
                    status = (OperationalStatus)Enum.Parse(typeof(OperationalStatus), statusString.ToString());
            }
            return status;
        }

        public static int GetVMProcessors(PowerShellManager powerShell, string name)
        {
            int procs = 0;

            Command cmd = new Command("Get-VMProcessor");

            cmd.Parameters.Add("VMName", name);

            Collection<PSObject> result = powerShell.Execute(cmd, true);
            if (result != null && result.Count > 0)
            {
                procs = Convert.ToInt32(result[0].GetProperty("Count"));

            }
            return procs;
        }

        public static void UpdateProcessors(PowerShellManager powerShell, VirtualMachine vm, int cpuCores, int cpuLimitSettings, int cpuReserveSettings, int cpuWeightSettings)
        {
            Command cmd = new Command("Set-VMProcessor");

            cmd.Parameters.Add("VMName", vm.Name);
            cmd.Parameters.Add("Count", cpuCores);
            cmd.Parameters.Add("Maximum", cpuLimitSettings);
            cmd.Parameters.Add("Reserve", cpuReserveSettings);
            cmd.Parameters.Add("RelativeWeight", cpuWeightSettings);

            powerShell.Execute(cmd, true);
        }

        public static void Delete(PowerShellManager powerShell, string vmName, string server)
        {
            Command cmd = new Command("Remove-VM");
            cmd.Parameters.Add("Name", vmName);
            if (!string.IsNullOrEmpty(server)) cmd.Parameters.Add("ComputerName", server);
            cmd.Parameters.Add("Force");
            powerShell.Execute(cmd, false, true);
        }
        public static void Stop(PowerShellManager powerShell, string vmName, bool force, string server)
        {
            Command cmd = new Command("Stop-VM");

            cmd.Parameters.Add("Name", vmName);
            if (force) cmd.Parameters.Add("Force");
            if (!string.IsNullOrEmpty(server)) cmd.Parameters.Add("ComputerName", server);
            //if (!string.IsNullOrEmpty(reason)) cmd.Parameters.Add("Reason", reason);

            powerShell.Execute(cmd, false);
        }
    }
}
