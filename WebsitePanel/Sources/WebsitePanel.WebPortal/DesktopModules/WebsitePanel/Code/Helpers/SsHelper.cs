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

using WebsitePanel.Providers.StorageSpaces;

namespace WebsitePanel.Portal
{
    public class SsHelper
    {
        #region Storage Space Levels

        StorageSpaceLevelPaged ssLevels;

        public int GetStorageSpaceLevelsPagedCount(string filterValue)
        {
            return ssLevels.RecordsCount;
        }

        public StorageSpaceLevel[] GetStorageSpaceLevelsPaged(int maximumRows, int startRowIndex, string sortColumn, string filterValue)
        {
            ssLevels = ES.Services.StorageSpaces.GetStorageSpaceLevelsPaged("Name", filterValue, sortColumn, startRowIndex, maximumRows);

            return ssLevels.Levels;
        }

        #endregion 

        #region Storage Spaces

        StorageSpacesPaged sSpaces;

        public int GetStorageSpacePagedCount(string filterValue)
        {
            return sSpaces.RecordsCount;
        }

        public StorageSpace[] GetStorageSpacePaged(int maximumRows, int startRowIndex, string sortColumn, string filterValue)
        {
            sSpaces = ES.Services.StorageSpaces.GetStorageSpacesPaged("Name", filterValue, sortColumn, startRowIndex, maximumRows);

            return sSpaces.Spaces;
        }

        #endregion 
    }
}
