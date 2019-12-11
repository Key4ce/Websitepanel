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
using WebsitePanel.Providers.Common;
using WebsitePanel.Providers.OS;
using WebsitePanel.Providers.StorageSpaces;

namespace WebsitePanel.EnterpriseServer
{
    public class OrganizationFoldersManager
    {
        public List<StorageSpaceFolder> GetFolders(int itemId, string type)
        {
            var folders = new List<StorageSpaceFolder>();

            ObjectUtils.FillCollectionFromDataReader(folders, DataProvider.GetOrganizationStoragSpacesFolderByType(itemId, type));

            return folders;
        }

        public StorageSpaceFolder GetFolder(int itemId, string type)
        {
            var folders = new List<StorageSpaceFolder>();

            ObjectUtils.FillCollectionFromDataReader(folders, DataProvider.GetOrganizationStoragSpacesFolderByType(itemId, type));

            return folders.FirstOrDefault();
        }

        public StorageSpaceFolder CreateFolder(string organizationId, int itemId, string type, long quotaInBytes, QuotaType qoutaType)
        {
            var storageId = StorageSpacesController.FindBestStorageSpaceService(new DefaultStorageSpaceSelector(), ResourceGroups.HostedOrganizations, quotaInBytes);

            if (!storageId.IsSuccess)
            {
                throw new Exception(storageId.ErrorCodes.First());
            }

            var folder = StorageSpacesController.CreateStorageSpaceFolder(storageId.Value, ResourceGroups.HostedOrganizations, organizationId, type, quotaInBytes, qoutaType);

            if (!folder.IsSuccess)
            {
                throw new Exception(string.Join("---------------------------------------", folder.ErrorCodes));
            }

            DataProvider.AddOrganizationStoragSpacesFolder(itemId, type, folder.Value);

            return StorageSpacesController.GetStorageSpaceFolderById(folder.Value);
        }

        public ResultObject DeleteFolder(int itemId, int folderId)
        {
            var result = TaskManager.StartResultTask<ResultObject>("ORGANIZATION_FOLDERS", "DELETE_FOLDER");

            try
            {
                var folder = StorageSpacesController.GetStorageSpaceFolderById(folderId);

                if (folder == null)
                {
                    throw new Exception("Folder not found");
                }

                DataProvider.DeleteOrganizationStoragSpacesFolder(folderId);

                var deletionResult = StorageSpacesController.DeleteStorageSpaceFolder(folder.StorageSpaceId, folder.Id);

                if (deletionResult.IsSuccess == false)
                {
                    throw new Exception(string.Join(";",deletionResult.ErrorCodes));
                }
            }
            catch (Exception exception)
            {
                TaskManager.WriteError(exception);
                result.AddError("Error deleting organization folder", exception);
            }
            finally
            {
                if (!result.IsSuccess)
                {
                    TaskManager.CompleteResultTask(result);
                }
                else
                {
                    TaskManager.CompleteResultTask();
                }
            }

            return result;
        }

        public ResultObject DeleteFolders(int itemId)
        {
            var result = TaskManager.StartResultTask<ResultObject>("ORGANIZATION_FOLDERS", "DELETE_ALL_FOLDERS");

            try
            {
                foreach (var storageSpaceFolderType in Enum.GetValues(typeof(StorageSpaceFolderTypes)))
                {
                    DeleteFolders(itemId, storageSpaceFolderType.ToString());
                }
            }
            catch (Exception exception)
            {
                TaskManager.WriteError(exception);
                result.AddError("Error deleting organization folders", exception);
            }
            finally
            {
                if (!result.IsSuccess)
                {
                    TaskManager.CompleteResultTask(result);
                }
                else
                {
                    TaskManager.CompleteResultTask();
                }
            }

            return result;
        }

        public ResultObject DeleteFolders(int itemId, string type)
        {
            var result = TaskManager.StartResultTask<ResultObject>("ORGANIZATION_FOLDERS", "DELETE_FOLDERS_BY_TYPE");

            try
            {
                var folders = GetFolders(itemId, type);

                foreach (var folder in folders)
                {
                    DeleteFolder(itemId, folder.Id);
                }
            }
            catch (Exception exception)
            {
                TaskManager.WriteError(exception);
                result.AddError("Error deleting organization folders", exception);
            }
            finally
            {
                if (!result.IsSuccess)
                {
                    TaskManager.CompleteResultTask(result);
                }
                else
                {
                    TaskManager.CompleteResultTask();
                }
            }

            return result;
        }
    }
}
