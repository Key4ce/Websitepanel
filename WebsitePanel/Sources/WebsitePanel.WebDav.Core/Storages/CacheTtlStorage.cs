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
using System.Runtime.Caching;
using Microsoft.Web.Services3.Design;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Interfaces.Storages;

namespace WebsitePanel.WebDav.Core.Storages
{
    public class CacheTtlStorage : ITtlStorage
    {
       private static readonly ObjectCache Cache;

       static CacheTtlStorage()
        {
            Cache = MemoryCache.Default;
        }

        public TV Get<TV>(string id)
        {
            var value = (TV)Cache[id];

            if (!EqualityComparer<TV>.Default.Equals(value, default(TV)))
            {
                SetTtl(id, value);
            }

            return value;
        }

        public bool Add<TV>(string id, TV value)
        {
            return Cache.Add(id, value, DateTime.Now.AddMinutes(WebDavAppConfigManager.Instance.OfficeOnline.CobaltFileTtl));
        }

        public bool Delete(string id)
        {
            if (Cache.Any(x => x.Key == id))
            {
                Cache.Remove(id);

                return true;
            }

            return false;
        }

        public void SetTtl<TV>(string id, TV value)
        {
            Cache.Set(id, value, DateTime.Now.AddMinutes(WebDavAppConfigManager.Instance.OfficeOnline.CobaltFileTtl));
        }
    }
}
