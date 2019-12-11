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

using System.Management.Automation.Runspaces;

namespace WebsitePanel.Providers.Virtualization
{
    public static class ReplicaHelper
    {
        public static void SetReplicaServer(PowerShellManager powerShell, bool enabled, string remoteServer, string thumbprint, string storagePath)
        {
            Command cmd = new Command("Set-VMReplicationServer");
            cmd.Parameters.Add("ReplicationEnabled", enabled);

            if (!string.IsNullOrEmpty(remoteServer))
            {
                cmd.Parameters.Add("ComputerName", remoteServer);
            }

            if (!string.IsNullOrEmpty(thumbprint))
            {
                cmd.Parameters.Add("AllowedAuthenticationType", "Certificate");
                cmd.Parameters.Add("CertificateThumbprint", thumbprint);
            }

            if (!string.IsNullOrEmpty(storagePath))
            {
                cmd.Parameters.Add("ReplicationAllowedFromAnyServer", true);
                cmd.Parameters.Add("DefaultStorageLocation", storagePath);
            }

            powerShell.Execute(cmd, false);
        }

        public static void SetFirewallRule(PowerShellManager powerShell, bool enabled)
        {
            Command cmd = new Command("Enable-Netfirewallrule");
            cmd.Parameters.Add("DisplayName", "Hyper-V Replica HTTPS Listener (TCP-In)");

            powerShell.Execute(cmd, false);
        }

        public static void RemoveVmReplication(PowerShellManager powerShell, string vmName, string server)
        {
            Command cmd = new Command("Remove-VMReplication");
            cmd.Parameters.Add("VmName", vmName);
            if (!string.IsNullOrEmpty(server)) cmd.Parameters.Add("ComputerName", server);

            powerShell.Execute(cmd, false);
        }
    }
}
