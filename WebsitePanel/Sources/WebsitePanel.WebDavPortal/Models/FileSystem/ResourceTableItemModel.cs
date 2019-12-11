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
using WebsitePanel.WebDav.Core.Client;
using WebsitePanel.WebDavPortal.Models.Common.DataTable;

namespace WebsitePanel.WebDavPortal.Models.FileSystem
{
    public class ResourceTableItemModel : JqueryDataTableBaseEntity
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public Uri Href { get; set; }
        public bool IsTargetBlank { get; set; }
        public bool IsFolder { get; set; }
        public long Size { get; set; }
        public bool IsRoot { get; set; }
        public long Quota { get; set; }
        public string Type { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedFormated { get; set; }
        public string IconHref { get; set; }
        public string FolderUrlAbsoluteString { get; set; }
        public string FolderUrlLocalString { get; set; }
        public string FolderName { get; set; }
        public string Summary { get; set; }

        public override dynamic this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1 :
                    {
                        return Size;
                    }
                    case 2:
                    {
                        return LastModified;
                    }
                    case 3:
                    {
                        return Type;
                    }
                    default:
                    {
                        return DisplayName;
                    }
                }
            }
        }
    }
}
