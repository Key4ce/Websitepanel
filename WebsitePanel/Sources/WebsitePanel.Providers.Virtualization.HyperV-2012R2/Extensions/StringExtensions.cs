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
using System.Text.RegularExpressions;

namespace WebsitePanel.Providers.Virtualization.Extensions
{
    public static class StringExtensions
    {
        public static string[] ParseExact(
            this string data,
            string format)
        {
            return ParseExact(data, format, false);
        }

        public static string[] ParseExact(
            this string data,
            string format,
            bool ignoreCase)
        {
            string[] values;

            if (TryParseExact(data, format, out values, ignoreCase))
                return values;
            else
                throw new ArgumentException("Format not compatible with value.");
        }

        public static bool TryExtract(
            this string data,
            string format,
            out string[] values)
        {
            return TryParseExact(data, format, out values, false);
        }

        public static bool TryParseExact(
            this string data,
            string format,
            out string[] values,
            bool ignoreCase)
        {
            int tokenCount = 0;
            format = Regex.Escape(format).Replace("\\{", "{");

            for (tokenCount = 0; ; tokenCount++)
            {
                string token = string.Format("{{{0}}}", tokenCount);
                if (!format.Contains(token)) break;
                format = format.Replace(token,
                    string.Format("(?'group{0}'.*)", tokenCount));
            }

            RegexOptions options =
                ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

            Match match = new Regex(format, options).Match(data);

            if (tokenCount != (match.Groups.Count - 1))
            {
                values = new string[] { };
                return false;
            }
            else
            {
                values = new string[tokenCount];
                for (int index = 0; index < tokenCount; index++)
                    values[index] =
                        match.Groups[string.Format("group{0}", index)].Value;
                return true;
            }
        }
    }
}
