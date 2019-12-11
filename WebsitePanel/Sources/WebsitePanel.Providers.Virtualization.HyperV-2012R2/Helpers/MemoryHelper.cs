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
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace WebsitePanel.Providers.Virtualization
{
    public static class MemoryHelper
    {
        public static DynamicMemory GetDynamicMemory(PowerShellManager powerShell, string vmName)
        {
            DynamicMemory info = null;

            Command cmd = new Command("Get-VMMemory");
            cmd.Parameters.Add("VMName", vmName);
            Collection<PSObject> result = powerShell.Execute(cmd);

            if (result != null && result.Count > 0)
            {
                info = new DynamicMemory();
                info.Enabled = result[0].GetBool("DynamicMemoryEnabled");
                info.Minimum = Convert.ToInt32(result[0].GetLong("Minimum") / Constants.Size1M);
                info.Maximum = Convert.ToInt32(result[0].GetLong("Maximum") / Constants.Size1M);
                info.Buffer = Convert.ToInt32(result[0].GetInt("Buffer"));
                info.Priority = Convert.ToInt32(result[0].GetInt("Priority"));
            }

            return info;
        }

        public static void Update(PowerShellManager powerShell, VirtualMachine vm, int ramMb, DynamicMemory dynamicMemory)
        {
            Command cmd = new Command("Set-VMMemory");

            cmd.Parameters.Add("VMName", vm.Name);
            cmd.Parameters.Add("StartupBytes", ramMb * Constants.Size1M);

            if (dynamicMemory != null && dynamicMemory.Enabled)
            {
                cmd.Parameters.Add("DynamicMemoryEnabled", true);
                cmd.Parameters.Add("MinimumBytes", dynamicMemory.Minimum * Constants.Size1M);
                cmd.Parameters.Add("MaximumBytes", dynamicMemory.Maximum * Constants.Size1M);
                cmd.Parameters.Add("Buffer", dynamicMemory.Buffer);
                cmd.Parameters.Add("Priority", dynamicMemory.Priority);
            }
            else
            {
                cmd.Parameters.Add("DynamicMemoryEnabled", false);
            }

            powerShell.Execute(cmd, true, true);
        }
    }
}
