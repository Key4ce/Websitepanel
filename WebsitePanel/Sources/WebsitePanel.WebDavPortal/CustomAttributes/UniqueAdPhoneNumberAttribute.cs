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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using WebsitePanel.WebDav.Core;

namespace WebsitePanel.WebDavPortal.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UniqueAdPhoneNumberAttribute : RemoteAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueString = value as string;

            if (!string.IsNullOrEmpty(valueString) && WspContext.User != null)
            {
                var attributes =
                    validationContext.ObjectType.GetProperty(validationContext.MemberName)
                        .GetCustomAttributes(typeof(DisplayNameAttribute), true);

                string displayName = attributes != null && attributes.Any()
                    ? (attributes[0] as DisplayNameAttribute).DisplayName
                    : validationContext.DisplayName;


                var result = !WspContext.Services.Organizations.CheckPhoneNumberIsInUse(WspContext.User.ItemId, valueString, WspContext.User.Login);

                return result ? ValidationResult.Success :
                       new ValidationResult(string.Format(Resources.Messages.AlreadyInUse, displayName));
            }

            return ValidationResult.Success;
        }

        public UniqueAdPhoneNumberAttribute(string routeName) : base(routeName) { }
        public UniqueAdPhoneNumberAttribute(string action, string controller) : base(action, controller) { }
        public UniqueAdPhoneNumberAttribute(string action, string controller, 
               string area) : base(action, controller, area) { }
    }
}
