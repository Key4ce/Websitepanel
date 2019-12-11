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
using System.Web;
using Cobalt;
using WebsitePanel.EnterpriseServer.Base.HostedSolution;
using WebsitePanel.Providers.HostedSolution;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Extensions;
using WebsitePanel.WebDav.Core.Interfaces.Security;
using WebsitePanel.WebDav.Core.Security.Authentication.Principals;
using WebsitePanel.WebDav.Core.Security.Authorization.Enums;
using WebsitePanel.WebDav.Core.Wsp.Framework;

namespace WebsitePanel.WebDav.Core.Security.Authorization
{
    public class WebDavAuthorizationService : IWebDavAuthorizationService
    {
        public bool HasAccess(WspPrincipal principal, string path)
        {
            path = path.RemoveLeadingFromPath(principal.OrganizationId);

            var permissions = GetPermissions(principal, path);

            return permissions.HasFlag(WebDavPermissions.Read) || permissions.HasFlag(WebDavPermissions.Write);
        }

        public WebDavPermissions GetPermissions(WspPrincipal principal, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return WebDavPermissions.Read;
            }

            var resultPermissions = WebDavPermissions.Empty;

            var rootFolder = GetRootFolder(path);

            var userGroups = GetUserSecurityGroups(principal);

            var permissions = GetFolderEsPermissions(principal, rootFolder);

            foreach (var permission in permissions)
            {
                if ((!permission.IsGroup
                        && (permission.DisplayName == principal.UserName || permission.DisplayName == principal.DisplayName))
                    || (permission.IsGroup && userGroups.Any(x => x.DisplayName == permission.DisplayName)))
                {
                    if (permission.Access.ToLowerInvariant().Contains("read"))
                    {
                        resultPermissions |= WebDavPermissions.Read;
                    }

                    if (permission.Access.ToLowerInvariant().Contains("write"))
                    {
                        resultPermissions |= WebDavPermissions.Write;
                    }
                }
            }

            var owaEditFolders = GetOwaFoldersWithEditPermission(principal);

            if (owaEditFolders.Contains(rootFolder))
            {
                resultPermissions |= WebDavPermissions.OwaEdit;
            }
            else
            {
                resultPermissions |= WebDavPermissions.OwaRead;
            }

            return resultPermissions;
        }

        private string GetRootFolder(string path)
        {
            return path.Split(new[]{'/'}, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private IEnumerable<ESPermission> GetFolderEsPermissions(WspPrincipal principal, string rootFolderName)
        {
            var dictionary = HttpContext.Current.Session != null ?HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.WebDavRootFoldersPermissions] as
                Dictionary<string, IEnumerable<ESPermission>> : null;

            if (dictionary == null)
            {
                dictionary = new Dictionary<string, IEnumerable<ESPermission>>();

                var rootFolders = WSP.Services.EnterpriseStorage.GetEnterpriseFoldersPaged(principal.ItemId, false,false, false,"","",0, int.MaxValue).PageItems;

                foreach (var rootFolder in rootFolders)
                {
                    var permissions = WSP.Services.EnterpriseStorage.GetEnterpriseFolderPermissions(principal.ItemId, rootFolder.Name);

                    dictionary.Add(rootFolder.Name, permissions);
                }

                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.WebDavRootFoldersPermissions] = dictionary;
                }
            }

            return dictionary.ContainsKey(rootFolderName) ? dictionary[rootFolderName] : new ESPermission[0];
        }

        public IEnumerable<ExchangeAccount> GetUserSecurityGroups(WspPrincipal principal)
        {
            var groups = HttpContext.Current.Session != null ? HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.UserGroupsKey] as IEnumerable<ExchangeAccount> : null;

            if (groups == null)
            {
                 groups = WSP.Services.Organizations.GetSecurityGroupsByMember(principal.ItemId, principal.AccountId);

                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.UserGroupsKey] = groups;
                }
            }

            return groups ?? new ExchangeAccount[0];
        }

        private IEnumerable<string> GetOwaFoldersWithEditPermission(WspPrincipal principal)
        {
            var folders = HttpContext.Current.Session != null ? HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.OwaEditFoldersSessionKey] as IEnumerable<string> : null;

            if (folders != null)
            {
                return folders;
            }

            var accountsIds = new List<int>();

            accountsIds.Add(principal.AccountId);

            var groups = GetUserSecurityGroups(principal);

            accountsIds.AddRange(groups.Select(x=>x.AccountId));

            try
            {
                folders = WspContext.Services.EnterpriseStorage.GetUserEnterpriseFolderWithOwaEditPermission(principal.ItemId, accountsIds.ToArray());
            }
            catch (Exception)
            {
                //TODO remove try catch when es &portal will be updated
                return new List<string>();
            }


            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.OwaEditFoldersSessionKey] = folders;
            }

            return folders;
        }
    }
}
