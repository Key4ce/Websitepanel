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
using System.IO;
using AutoMapper;
using WebsitePanel.Providers.HostedSolution;
using WebsitePanel.WebDav.Core.Client;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Extensions;
using WebsitePanel.WebDavPortal.Constants;
using WebsitePanel.WebDavPortal.FileOperations;
using WebsitePanel.WebDavPortal.Models.Account;
using WebsitePanel.WebDavPortal.Models.FileSystem;

namespace WebsitePanel.WebDavPortal.Mapping.Profiles.Account
{
    public class UserProfileProfile : Profile
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
            Mapper.CreateMap<OrganizationUser, UserProfile>()
                .ForMember(ti => ti.PrimaryEmailAddress, x => x.MapFrom(hi => hi.PrimaryEmailAddress))
                .ForMember(ti => ti.DisplayName, x => x.MapFrom(hi => hi.DisplayName))
                .ForMember(ti => ti.DisplayName, x => x.MapFrom(hi => hi.DisplayName))
                .ForMember(ti => ti.AccountName, x => x.MapFrom(hi => hi.AccountName))
                .ForMember(ti => ti.FirstName, x => x.MapFrom(hi => hi.FirstName))
                .ForMember(ti => ti.Initials, x => x.MapFrom(hi => hi.Initials))
                .ForMember(ti => ti.LastName, x => x.MapFrom(hi => hi.LastName))
                .ForMember(ti => ti.JobTitle, x => x.MapFrom(hi => hi.JobTitle))
                .ForMember(ti => ti.Company, x => x.MapFrom(hi => hi.Company))
                .ForMember(ti => ti.Department, x => x.MapFrom(hi => hi.Department))
                .ForMember(ti => ti.Office, x => x.MapFrom(hi => hi.Office))
                .ForMember(ti => ti.BusinessPhone, x => x.MapFrom(hi => hi.BusinessPhone))
                .ForMember(ti => ti.Fax, x => x.MapFrom(hi => hi.Fax))
                .ForMember(ti => ti.HomePhone, x => x.MapFrom(hi => hi.HomePhone))
                .ForMember(ti => ti.MobilePhone, x => x.MapFrom(hi => hi.MobilePhone))
                .ForMember(ti => ti.Pager, x => x.MapFrom(hi => hi.Pager))
                .ForMember(ti => ti.WebPage, x => x.MapFrom(hi => hi.WebPage))
                .ForMember(ti => ti.Address, x => x.MapFrom(hi => hi.Address))
                .ForMember(ti => ti.City, x => x.MapFrom(hi => hi.City))
                .ForMember(ti => ti.State, x => x.MapFrom(hi => hi.State))
                .ForMember(ti => ti.Zip, x => x.MapFrom(hi => hi.Zip))
                .ForMember(ti => ti.Country, x => x.MapFrom(hi => hi.Country))
                .ForMember(ti => ti.Notes, x => x.MapFrom(hi => hi.Notes))
                .ForMember(ti => ti.PasswordExpirationDateTime, x => x.MapFrom(hi => hi.PasswordExpirationDateTime))
                .ForMember(ti => ti.ExternalEmail, x => x.MapFrom(hi => hi.ExternalEmail));
        }
    }
}

