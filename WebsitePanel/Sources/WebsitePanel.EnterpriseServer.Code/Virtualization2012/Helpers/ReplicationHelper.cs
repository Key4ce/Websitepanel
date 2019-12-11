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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using WebsitePanel.Providers.Common;
using WebsitePanel.Providers.Virtualization;
using WebsitePanel.Providers.Virtualization2012;

namespace WebsitePanel.EnterpriseServer.Code.Virtualization2012
{
    public static class ReplicationHelper
    {

        public static void CleanUpReplicaServer(VirtualMachine originalVm)
        {
            try
            {
                ResultObject result = new ResultObject();

                // Get replica server
                var replicaServer = GetReplicaForService(originalVm.ServiceId, ref result);

                // Clean up replica server
                var replicaVm = replicaServer.GetVirtualMachines().FirstOrDefault(m => m.Name == originalVm.Name);
                if (replicaVm != null)
                {
                    replicaServer.DisableVmReplication(replicaVm.VirtualMachineId);
                    replicaServer.ShutDownVirtualMachine(replicaVm.VirtualMachineId, true, "ReplicaDelete");
                    replicaServer.DeleteVirtualMachine(replicaVm.VirtualMachineId);
                }
            }
            catch { /* skip */ }
        }

        public static ReplicationServerInfo GetReplicaInfoForService(int serviceId, ref ResultObject result)
        {
            // Get service id of replica server
            StringDictionary vsSesstings = ServerController.GetServiceSettings(serviceId);
            string replicaServiceId = vsSesstings["ReplicaServerId"];

            if (string.IsNullOrEmpty(replicaServiceId))
            {
                result.ErrorCodes.Add(VirtualizationErrorCodes.NO_REPLICA_SERVER_ERROR);
                return null;
            }

            // get replica server info for replica service id
            VirtualizationServer2012 vsReplica = VirtualizationHelper.GetVirtualizationProxy(Convert.ToInt32(replicaServiceId));
            StringDictionary vsReplicaSesstings = ServerController.GetServiceSettings(Convert.ToInt32(replicaServiceId));
            string computerName = vsReplicaSesstings["ServerName"];
            var replicaServerInfo = vsReplica.GetReplicaServer(computerName);

            if (!replicaServerInfo.Enabled)
            {
                result.ErrorCodes.Add(VirtualizationErrorCodes.NO_REPLICA_SERVER_ERROR);
                return null;
            }

            return replicaServerInfo;
        }

        public static VirtualizationServer2012 GetReplicaForService(int serviceId, ref ResultObject result)
        {
            // Get service id of replica server
            StringDictionary vsSesstings = ServerController.GetServiceSettings(serviceId);
            string replicaServiceId = vsSesstings["ReplicaServerId"];

            if (string.IsNullOrEmpty(replicaServiceId))
            {
                result.ErrorCodes.Add(VirtualizationErrorCodes.NO_REPLICA_SERVER_ERROR);
                return null;
            }

            // get replica server for replica service id
            return VirtualizationHelper.GetVirtualizationProxy(Convert.ToInt32(replicaServiceId));
        }

        public static void CheckReplicationQuota(int packageId, ref ResultObject result)
        {
            List<string> quotaResults = new List<string>();
            PackageContext cntx = PackageController.GetPackageContext(packageId);

            QuotaHelper.CheckBooleanQuota(cntx, quotaResults, Quotas.VPS2012_REPLICATION_ENABLED, true, VirtualizationErrorCodes.QUOTA_REPLICATION_ENABLED);

            if (quotaResults.Count > 0)
                result.ErrorCodes.AddRange(quotaResults);
        }
    }
}
