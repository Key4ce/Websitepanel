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
    public static class DvdDriveHelper
    {
        public static DvdDriveInfo Get(PowerShellManager powerShell, string vmName)
        {
            DvdDriveInfo info = null;

            PSObject result = GetPS(powerShell, vmName);

            if (result != null)
            {
                info = new DvdDriveInfo();
                info.Id = result.GetString("Id");
                info.Name = result.GetString("Name");
                info.ControllerType = result.GetEnum<ControllerType>("ControllerType");
                info.ControllerNumber = result.GetInt("ControllerNumber");
                info.ControllerLocation = result.GetInt("ControllerLocation");
                info.Path = result.GetString("Path");
            }
            return info;
        }

        public static PSObject GetPS(PowerShellManager powerShell, string vmName)
        {
            Command cmd = new Command("Get-VMDvdDrive");

            cmd.Parameters.Add("VMName", vmName);

            Collection<PSObject> result = powerShell.Execute(cmd, true);

            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            
            return null;
        }

        public static void Set(PowerShellManager powerShell, string vmName, string path)
        {
            var dvd = Get(powerShell, vmName);
 
            Command cmd = new Command("Set-VMDvdDrive");

            cmd.Parameters.Add("VMName", vmName);
            cmd.Parameters.Add("Path", path);
            cmd.Parameters.Add("ControllerNumber", dvd.ControllerNumber);
            cmd.Parameters.Add("ControllerLocation", dvd.ControllerLocation);

            powerShell.Execute(cmd, true);
        }

        public static void Update(PowerShellManager powerShell, VirtualMachine vm, bool dvdDriveShouldBeInstalled)
        {
            if (!vm.DvdDriveInstalled && dvdDriveShouldBeInstalled)
                Add(powerShell, vm.Name);
            else if (vm.DvdDriveInstalled && !dvdDriveShouldBeInstalled)
                Remove(powerShell, vm.Name);
        }

        public static void Add(PowerShellManager powerShell, string vmName)
        {
            Command cmd = new Command("Add-VMDvdDrive");

            cmd.Parameters.Add("VMName", vmName);

            powerShell.Execute(cmd, true);
        }

        public static void Remove(PowerShellManager powerShell, string vmName)
        {
            var dvd = Get(powerShell, vmName);

            Command cmd = new Command("Remove-VMDvdDrive");

            cmd.Parameters.Add("VMName", vmName);
            cmd.Parameters.Add("ControllerNumber", dvd.ControllerNumber);
            cmd.Parameters.Add("ControllerLocation", dvd.ControllerLocation);

            powerShell.Execute(cmd, true);
        }
    }
}
