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
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace WebsitePanel.Providers.Virtualization
{
    static class PSObjectExtension
    {
        #region Properties

        public static object GetProperty(this PSObject obj, string name)
        {
            return obj.Members[name].Value;
        }
        public static T GetProperty<T>(this PSObject obj, string name)
        {
            return (T)obj.Members[name].Value;
        }
        public static T GetEnum<T>(this PSObject obj, string name, T? defaultValue = null) where T : struct
        {
            try
            {
                return (T) Enum.Parse(typeof (T), GetProperty(obj, name).ToString());
            }
            catch
            {
                if (defaultValue.HasValue) return defaultValue.Value;
                throw;
            }
        }
        public static int GetInt(this PSObject obj, string name)
        {
            return Convert.ToInt32(obj.Members[name].Value);
        }
        public static long GetLong(this PSObject obj, string name)
        {
            return Convert.ToInt64(obj.Members[name].Value);
        }
        public static string GetString(this PSObject obj, string name)
        {
            return obj.Members[name].Value == null ? "" : obj.Members[name].Value.ToString();
        }
        public static bool GetBool(this PSObject obj, string name)
        {
            return Convert.ToBoolean(obj.Members[name].Value);
        }

        public static string GetMb(this PSObject obj, string name)
        {
            var bytes = GetLong(obj, name);

            if (bytes == 0)
                return "0";

            if (bytes > Constants.Size1G)
                return string.Format("{0:0.0} GB", bytes / Constants.Size1G);

            if (bytes > Constants.Size1M)
                return string.Format("{0:0.0} MB", bytes / Constants.Size1M);

            if (bytes > Constants.Size1K)
                return string.Format("{0:0.0} KB", bytes / Constants.Size1K);

            return string.Format("{0} b", bytes);
        }
        
        #endregion


        #region Methods

        public static ManagementObject Invoke(this PSObject obj, string name, object argument)
        {
            return obj.Invoke(name, new[] {argument});
        }
        public static ManagementObject Invoke(this PSObject obj, string name, params object[] arguments)
        {
            var results = (ManagementObjectCollection)obj.Methods[name].Invoke(arguments);

            foreach (var result in results)
            {
                return (ManagementObject) result;
            }
            return null;
        }

        #endregion
    }
}
