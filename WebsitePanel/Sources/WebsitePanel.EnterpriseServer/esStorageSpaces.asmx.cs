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
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.Web.Services3;
using WebsitePanel.Providers.Common;
using WebsitePanel.Providers.OS;
using WebsitePanel.Providers.ResultObjects;
using WebsitePanel.Providers.StorageSpaces;

namespace WebsitePanel.EnterpriseServer
{
    /// <summary>
    /// Summary description for esSystem
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [Policy("ServerPolicy")]
    [ToolboxItem(false)]
    public class esStorageSpaces : System.Web.Services.WebService
    {
        [WebMethod]
        public StorageSpaceLevelPaged GetStorageSpaceLevelsPaged(string filterColumn, string filterValue, string sortColumn, int startRow, int maximumRows)
        {
            return StorageSpacesController.GetStorageSpaceLevelsPaged(filterColumn, filterValue, sortColumn, startRow, maximumRows);
        }

        [WebMethod]
        public StorageSpaceLevel GetStorageSpaceLevelById(int id)
        {
            return StorageSpacesController.GetStorageSpaceLevelById(id);
        }

        [WebMethod]
        public bool CheckIsStorageSpacePathInUse(int serverId, string path, int currentServiceId)
        {
            return StorageSpacesController.CheckIsStorageSpacePathInUse(serverId, path, currentServiceId);
        }

        [WebMethod]
        public IntResult SaveStorageSpaceLevel(StorageSpaceLevel level, List<ResourceGroupInfo> groups )
        {
            return StorageSpacesController.SaveStorageSpaceLevel(level, groups);
        }

        [WebMethod]
        public List<StorageSpaceFolder> GetStorageSpaceFoldersByStorageSpaceId(int id)
        {
            return StorageSpacesController.GetStorageSpaceFoldersByStorageSpaceId(id);
        }

        [WebMethod]
        public List<ResourceGroupInfo> GetLevelResourceGroups(int id)
        {
            return StorageSpacesController.GetLevelResourceGroups(id);
        }

        [WebMethod]
        public ResultObject SaveLevelResourceGroups(int levelId, List<ResourceGroupInfo> newGroups)
        {
            return StorageSpacesController.SaveLevelResourceGroups(levelId, newGroups);
        }

        [WebMethod]
        public ResultObject RemoveStorageSpaceLevel(int id)
        {
            return StorageSpacesController.RemoveStorageSpaceLevel(id);
        }

        [WebMethod]
        public StorageSpacesPaged GetStorageSpacesPaged(string filterColumn, string filterValue, string sortColumn, int startRow, int maximumRows)
        {
            return StorageSpacesController.GetStorageSpacesPaged(filterColumn, filterValue, sortColumn, startRow, maximumRows);
        }

        [WebMethod]
        public List<StorageSpace> GetStorageSpacesByLevelId(int levelId)
        {
            return StorageSpacesController.GetStorageSpacesByLevelId(levelId);
        }

        [WebMethod]
        public StorageSpace GetStorageSpaceById(int id)
        {
            return StorageSpacesController.GetStorageSpaceById(id);
        }

        [WebMethod]
        public IntResult SaveStorageSpace(StorageSpace space)
        {
            return StorageSpacesController.SaveStorageSpace(space);
        }

        [WebMethod]
        public ResultObject RemoveStorageSpace(int id)
        {
            return StorageSpacesController.RemoveStorageSpace(id);
        }

        [WebMethod]
        public SystemFile[] GetDriveLetters(int serviceId)
        {
            return StorageSpacesController.GetDriveLetters(serviceId);
        }

        [WebMethod]
        public SystemFile[] GetSystemSubFolders(int serviceId, string path)
        {
            return StorageSpacesController.GetSystemSubFolders(serviceId, path);
        }

        [WebMethod]
        public void SetStorageSpaceFolderAbeStatus(int storageSpaceFolderId, bool enabled)
        {
            StorageSpacesController.SetStorageSpaceFolderAbeStatus(storageSpaceFolderId, enabled);
        }

        [WebMethod]
        public bool GetStorageSpaceFolderAbeStatus(int storageSpaceFolderId)
        {
           return StorageSpacesController.GetStorageSpaceFolderAbeStatus(storageSpaceFolderId);
        }

        [WebMethod]
        public void SetStorageSpaceFolderEncryptDataAccessStatus(int storageSpaceFolderId, bool enabled)
        {
            StorageSpacesController.SetStorageSpaceFolderEncryptDataAccessStatus(storageSpaceFolderId, enabled);
        }

        [WebMethod]
        public bool GetStorageSpaceFolderEncryptDataAccessStatus(int storageSpaceFolderId)
        {
            return StorageSpacesController.GetStorageSpaceFolderEncryptDataAccessStatus(storageSpaceFolderId);
        }
    }
}
