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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using WebsitePanel.WebDavPortal.CustomAttributes;
using WebsitePanel.WebDavPortal.Models.Common;
using WebsitePanel.WebDavPortal.UI.Routes;

namespace WebsitePanel.WebDavPortal.Models.Account
{
    public class UserProfile 
    {
        [Display(ResourceType = typeof(Resources.UI), Name = "PrimaryEmail")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "EmailInvalid", ErrorMessage = null)]
        public string PrimaryEmailAddress { get; set; }

        [Display(ResourceType = typeof(Resources.UI), Name = "DisplayName")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string DisplayName { get; set; }
        public string AccountName { get; set; }

        [Display(ResourceType = typeof(Resources.UI), Name = "FirstName")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string FirstName { get; set; }
        public string Initials { get; set; }

        [Display(ResourceType = typeof(Resources.UI), Name = "LastName")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string Office { get; set; }

        [PhoneNumber(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PhoneNumberInvalid")]
        [UniqueAdPhoneNumber(AccountRouteNames.PhoneNumberIsAvailible, ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "AlreadyInUse")]
        [Display(ResourceType = typeof(Resources.UI), Name = "BusinessPhone")]
        public string BusinessPhone { get; set; }

        [PhoneNumber(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PhoneNumberInvalid")]
        [UniqueAdPhoneNumber(AccountRouteNames.PhoneNumberIsAvailible, ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "AlreadyInUse")]
        [Display(ResourceType = typeof(Resources.UI), Name = "Fax")]
        public string Fax { get; set; }

        [Display(ResourceType = typeof(Resources.UI), Name = "HomePhone")]
        [PhoneNumber(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PhoneNumberInvalid")]
        [UniqueAdPhoneNumber(AccountRouteNames.PhoneNumberIsAvailible, ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "AlreadyInUse")]
        public string HomePhone { get; set; }

        [Display(ResourceType = typeof(Resources.UI), Name = "MobilePhone")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [PhoneNumber(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PhoneNumberInvalid")]
        [UniqueAdPhoneNumber(AccountRouteNames.PhoneNumberIsAvailible, ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "AlreadyInUse")]
        public string MobilePhone { get; set; }

        [PhoneNumber(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PhoneNumberInvalid")]
        [Display(ResourceType = typeof(Resources.UI), Name = "Pager")]
        [UniqueAdPhoneNumber(AccountRouteNames.PhoneNumberIsAvailible,ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "AlreadyInUse")]
        public string Pager { get; set; }

        [Url(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "UrlInvalid", ErrorMessage = null)]
        public string WebPage { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "EmailInvalid", ErrorMessage = null)]
        public string ExternalEmail { get; set; }

        [UIHint("CountrySelector")]
        public string Country { get; set; }

        public string Notes { get; set; }
        public DateTime PasswordExpirationDateTime { get; set; }
    }
}
