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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebsitePanel.Providers
{
    public class LogExtensionHelper
    {
        public const string LOG_STRING_TEMPLATE = "{0}: {1}";
        public const string LOG_ARRAY_SEPARATOR = ", ";

        public static string CombineString(string name, string value)
        {
            if (name == null)
                name = "";

            if (value == null)
                value = "";

            return String.Format(LOG_STRING_TEMPLATE, name, value);
        }

        public static string DecorateName(string name)
        {
            if (name == null)
                return "";

            name = Regex.Replace(name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0"); // "DriveIsSCSICompatible" becomes "Drive Is SCSI Compatible"
            name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name); // Capitalize
            name = Regex.Replace(name, @"\bId\b", "ID", RegexOptions.IgnoreCase); // "Id" becomes "ID"

            return name;
        }

        public static string GetString(object value)
        {
            if (value == null)
                return "";

            // if array
            if (value.GetType().IsArray)
            {
                var elementType = value.GetType().GetElementType();

                if (elementType != null && !elementType.IsValueType)
                {
                    string[] strs = ((IEnumerable) value).Cast<object>().Select(x => x.ToString()).ToArray();
                    return string.Join(LOG_ARRAY_SEPARATOR, strs);
                }
            }

            return value.ToString();
        }
    }
}
