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
using System.Linq;
using WebsitePanel.Providers.StorageSpaces;

namespace WebsitePanel.EnterpriseServer
{
    public class EnterpriseStorageSpaceSelector : IStorageSpaceSelector
    {
        private readonly int _esId;

        public EnterpriseStorageSpaceSelector(int esId)
        {
            _esId = esId;
        }


        public StorageSpace FindBest(string groupName, long quotaSizeInBytes)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentNullException("groupName");
            }


            var storages = ObjectUtils.CreateListFromDataReader<StorageSpace>(DataProvider.GetStorageSpacesByResourceGroupName(groupName)).Where(x => !x.IsDisabled).ToList();

            if (!storages.Any())
            {
                throw new Exception(string.Format("Storage spaces not found for '{0}' resource group", groupName));
            }

            var service = ServerController.GetServiceInfo(_esId);

            storages = storages.Any(x => x.ServerId == service.ServerId) ? storages.Where(x => x.ServerId == service.ServerId).ToList() : storages;

            var orderedStorages = storages.OrderByDescending(x => x.FsrmQuotaSizeBytes - x.UsedSizeBytes);

            var bestStorage = orderedStorages.First();

            if (bestStorage.FsrmQuotaSizeBytes - bestStorage.UsedSizeBytes < quotaSizeInBytes)
            {
                throw new Exception("Space storages was found, but available space not enough");
            }
            
            if (bestStorage.FsrmQuotaSizeBytes - bestStorage.UsedSizeBytes < quotaSizeInBytes)
            {
                throw new Exception("Space storages was found, but available space not enough");
            }

            return bestStorage;
        }
    }
}
