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
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using WebsitePanel.WebDav.Core;
using WebsitePanel.WebDav.Core.Client;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDavPortal.Extensions;
using WebsitePanel.WebDavPortal.UI.Routes;

namespace WebsitePanel.WebDavPortal.FileOperations
{
    public class FileOpenerManager
    {
        private readonly IDictionary<string, FileOpenerType> _operationTypes = new Dictionary<string, FileOpenerType>();

        private readonly Lazy<IDictionary<string, FileOpenerType>> _officeOperationTypes = new Lazy<IDictionary<string, FileOpenerType>>(
            () =>
            {
                if (WebDavAppConfigManager.Instance.OfficeOnline.IsEnabled)
                {
                    return 
                        WebDavAppConfigManager.Instance.OfficeOnline.ToDictionary(x => x.Extension,
                            y => FileOpenerType.OfficeOnline);
                }

                return new Dictionary<string, FileOpenerType>();
            });

        public FileOpenerManager()
        {
            _operationTypes.AddRange(
                    WebDavAppConfigManager.Instance.FileOpener.ToDictionary(x => x.Extension,
                        y => FileOpenerType.Open));
        }

        public string GetUrl(IHierarchyItem item, UrlHelper urlHelper)
        {
            var opener = this[Path.GetExtension(item.DisplayName)];
            string href = "/";

            switch (opener)
            {
                case FileOpenerType.OfficeOnline:
                {
                    var pathPart = item.Href.AbsolutePath.Replace("/" + WspContext.User.OrganizationId, "").TrimStart('/');
                    href = string.Concat(urlHelper.RouteUrl(FileSystemRouteNames.EditOfficeOnline, new { org = WspContext.User.OrganizationId, pathPart = "" }), pathPart);
                    break;
                }
                default:
                {
                    href = item.Href.LocalPath;
                    break;
                }
            }

            return href;
        }

        public bool GetIsTargetBlank(IHierarchyItem item)
        {
            var opener = this[Path.GetExtension(item.DisplayName)];
            var result = false;

            switch (opener)
            {
                case FileOpenerType.OfficeOnline:
                {
                    result = true;
                    break;
                }
                    case FileOpenerType.Open:
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public string GetMimeType(string extension)
        {
            var opener = WebDavAppConfigManager.Instance.FileOpener.FirstOrDefault(x => x.Extension.ToLowerInvariant() == extension.ToLowerInvariant());

            if (opener == null)
            {
                return MediaTypeNames.Application.Octet;
            }

            return opener.MimeType;
        }

        public FileOpenerType this[string fileExtension]
        {
            get
            {
                FileOpenerType result;
                if (_officeOperationTypes.Value.TryGetValue(fileExtension, out result) && CheckBrowserSupport())
                {
                    return result;
                }

                if (_operationTypes.TryGetValue(fileExtension, out result))
                {
                    return result;
                }

                return FileOpenerType.Download;
            }
        }

        private bool CheckBrowserSupport()
        {
            var request = HttpContext.Current.Request;
            int supportedVersion;

            string key = string.Empty;

            foreach (var supportedKey in WebDavAppConfigManager.Instance.OwaSupportedBrowsers.Keys)
            {
                if (supportedKey.Split(';').Contains(request.Browser.Browser))
                {
                    key = supportedKey;
                    break;
                }
            }

            if (WebDavAppConfigManager.Instance.OwaSupportedBrowsers.TryGetValue(key, out supportedVersion) == false)
            {
                return false;
            }

            return supportedVersion <= request.Browser.MajorVersion;
        }
    }
}
