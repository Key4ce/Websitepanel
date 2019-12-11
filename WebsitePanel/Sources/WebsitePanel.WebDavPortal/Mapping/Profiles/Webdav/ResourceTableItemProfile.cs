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
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using WebsitePanel.WebDav.Core.Client;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Extensions;
using WebsitePanel.WebDavPortal.Constants;
using WebsitePanel.WebDavPortal.FileOperations;
using WebsitePanel.WebDavPortal.Models.FileSystem;

namespace WebsitePanel.WebDavPortal.Mapping.Profiles.Webdav
{
    public class ResourceTableItemProfile : Profile
    {
        /// <summary>
        ///     Gets the name of the profile.
        /// </summary>
        /// <value>
        ///     The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        ///     Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        ///     Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            var openerManager = new FileOpenerManager();

            Mapper.CreateMap<WebDavResource, ResourceTableItemModel>()
                .ForMember(ti => ti.DisplayName, x => x.MapFrom(hi => hi.DisplayName.Trim('/')))
                .ForMember(ti => ti.Href, x => x.MapFrom(hi => hi.Href))
                .ForMember(ti => ti.Type, x => x.MapFrom(hi => hi.ItemType.GetDescription().ToLowerInvariant()))
                .ForMember(ti => ti.IconHref, x => x.MapFrom(hi => hi.ItemType == ItemType.Folder ? WebDavAppConfigManager.Instance.FileIcons.FolderPath.Trim('~') : WebDavAppConfigManager.Instance.FileIcons[Path.GetExtension(hi.DisplayName.Trim('/'))].Trim('~')))
                .ForMember(ti => ti.IsTargetBlank, x => x.MapFrom(hi => openerManager.GetIsTargetBlank(hi)))
                .ForMember(ti => ti.LastModified, x => x.MapFrom(hi => hi.LastModified))
                .ForMember(ti => ti.LastModifiedFormated, x => x.MapFrom(hi => hi.LastModified == DateTime.MinValue ? "--" : (new WebDavResource(null, hi)).LastModified.ToString(Formats.DateFormatWithTime)))

                .ForMember(ti => ti.Summary, x => x.MapFrom(hi => hi.Summary))
                .ForMember(ti => ti.IsRoot, x => x.MapFrom(hi => hi.IsRootItem))
                .ForMember(ti => ti.Size, x => x.MapFrom(hi => hi.ContentLength))
                .ForMember(ti => ti.Quota, x => x.MapFrom(hi => hi.AllocatedSpace))
                .ForMember(ti => ti.Url, x => x.Ignore())
                .ForMember(ti => ti.FolderUrlAbsoluteString, x => x.Ignore())
                .ForMember(ti => ti.FolderUrlLocalString, x => x.Ignore())
                .ForMember(ti => ti.FolderName, x => x.Ignore())
                .ForMember(ti => ti.IsFolder, x => x.MapFrom(hi => hi.ItemType == ItemType.Folder));
        }
    }
}
