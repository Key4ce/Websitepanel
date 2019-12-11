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
    public static class HardDriveHelper
    {
        public static VirtualHardDiskInfo[] Get(PowerShellManager powerShell, string vmname)
        {
            List<VirtualHardDiskInfo> disks = new List<VirtualHardDiskInfo>();

            Collection<PSObject> result = GetPS(powerShell, vmname);

            if (result != null && result.Count > 0)
            {
                foreach (PSObject d in result)
                {
                    VirtualHardDiskInfo disk = new VirtualHardDiskInfo();

                    disk.SupportPersistentReservations = Convert.ToBoolean(d.GetProperty("SupportPersistentReservations"));
                    disk.MaximumIOPS = Convert.ToUInt64(d.GetProperty("MaximumIOPS"));
                    disk.MinimumIOPS = Convert.ToUInt64(d.GetProperty("MinimumIOPS"));
                    disk.VHDControllerType = d.GetEnum<ControllerType>("ControllerType");
                    disk.ControllerNumber = Convert.ToInt32(d.GetProperty("ControllerNumber"));
                    disk.ControllerLocation = Convert.ToInt32(d.GetProperty("ControllerLocation"));
                    disk.Path = d.GetProperty("Path").ToString();
                    disk.Name = d.GetProperty("Name").ToString();

                    GetVirtualHardDiskDetail(powerShell, disk.Path, ref disk);

                    disks.Add(disk);
                }
            }
            return disks.ToArray();
        }

        //public static VirtualHardDiskInfo GetByPath(PowerShellManager powerShell, string vhdPath)
        //{
        //    VirtualHardDiskInfo info = null;
        //    var vmNames = new List<string>();

        //    Command cmd = new Command("Get-VM");

        //    Collection<PSObject> result = powerShell.Execute(cmd, true);

        //    if (result == null || result.Count == 0)
        //        return null;

        //    vmNames = result.Select(r => r.GetString("Name")).ToList();
        //    var drives = vmNames.SelectMany(n => Get(powerShell, n));

        //    return drives.FirstOrDefault(d=>d.Path == vhdPath);
        //}

        public static Collection<PSObject> GetPS(PowerShellManager powerShell, string vmname)
        {
            Command cmd = new Command("Get-VMHardDiskDrive");
            cmd.Parameters.Add("VMName", vmname);

            return powerShell.Execute(cmd, true);
        }

        public static void GetVirtualHardDiskDetail(PowerShellManager powerShell, string path, ref VirtualHardDiskInfo disk)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Command cmd = new Command("Get-VHD");
                cmd.Parameters.Add("Path", path);
                Collection<PSObject> result = powerShell.Execute(cmd, true);
                if (result != null && result.Count > 0)
                {
                    disk.DiskFormat = result[0].GetEnum<VirtualHardDiskFormat>("VhdFormat");
                    disk.DiskType = result[0].GetEnum<VirtualHardDiskType>("VhdType");
                    disk.ParentPath = result[0].GetProperty<string>("ParentPath");
                    disk.MaxInternalSize = Convert.ToInt64(result[0].GetProperty("Size"));
                    disk.FileSize = Convert.ToInt64(result[0].GetProperty("FileSize"));
                    disk.Attached = disk.InUse = Convert.ToBoolean(result[0].GetProperty("Attached"));
                }
            }
        }

    }
}
