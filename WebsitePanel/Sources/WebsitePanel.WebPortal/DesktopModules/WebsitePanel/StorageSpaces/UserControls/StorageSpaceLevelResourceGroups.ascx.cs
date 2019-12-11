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
using System.Web.UI;
using System.Web.UI.WebControls;
using WebsitePanel.EnterpriseServer;

namespace WebsitePanel.Portal.StorageSpaces.UserControls
{
    public partial class StorageSpaceLevelResourceGroups : WebsitePanelControlBase
    {
        public const string DirectionString = "DirectionString";

        protected enum SelectedState
        {
            All,
            Selected,
            Unselected
        }

        public ResourceGroupInfo[] GetResourceGroups()
        {
            return GetGridViewResourceGroups(SelectedState.All).ToArray();
        }

        public void SetResourceGroups(ResourceGroupInfo[] groups)
        {
            BindResourceGroups(groups, false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // register javascript
            if (!Page.ClientScript.IsClientScriptBlockRegistered("SelectAllCheckboxes"))
            {
                string script = @"    function SelectAllCheckboxes(box)
                {
		            var state = box.checked;
                    var elm = box.parentElement.parentElement.parentElement.parentElement.getElementsByTagName(""INPUT"");
                    for(i = 0; i < elm.length; i++)
                        if(elm[i].type == ""checkbox"" && elm[i].id != box.id && elm[i].checked != state && !elm[i].disabled)
		                    elm[i].checked = state;
                }";
                Page.ClientScript.RegisterClientScriptBlock(typeof(StorageSpaceLevelResourceGroups), "SelectAllCheckboxes",
                    script, true);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // bind all accounts
            BindPopupResourceGroups();

            // show modal
            AddAccountsModal.Show();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<ResourceGroupInfo> selectedGroups = GetGridViewResourceGroups(SelectedState.Unselected);

            BindResourceGroups(selectedGroups.ToArray(), false);
        }

        protected void btnAddSelected_Click(object sender, EventArgs e)
        {
            List<ResourceGroupInfo> selectedGroups = GetGridViewResourceGroups();

            BindResourceGroups(selectedGroups.ToArray(), true);
        }


        protected void BindPopupResourceGroups()
        {
            ResourceGroupInfo[] groups = ES.Services.Servers.GetResourceGroups().Where(x => string.IsNullOrEmpty(txtSearchValue.Text) || x.GroupName.ToLowerInvariant().Contains(txtSearchValue.Text.ToLowerInvariant())).ToArray();

            groups = groups.Where(x => !GetResourceGroups().Select(p => p.GroupId).Contains(x.GroupId)).ToArray();

            Array.Sort(groups, CompareResourceGroups);

            if (Direction == SortDirection.Ascending)
            {
                Array.Reverse(groups);
                Direction = SortDirection.Descending;
            }
            else
                Direction = SortDirection.Ascending;

            gvPopupResourceGroups.DataSource = groups;
            gvPopupResourceGroups.DataBind();
        }

        protected void BindResourceGroups(ResourceGroupInfo[] newGroups, bool preserveExisting)
        {
            // get binded addresses
            List<ResourceGroupInfo> groups = new List<ResourceGroupInfo>();
            if (preserveExisting)
                groups.AddRange(GetGridViewResourceGroups(SelectedState.All));

            // add new accounts
            if (newGroups != null)
            {
                foreach (ResourceGroupInfo newGroup in newGroups)
                {
                    // check if exists
                    bool exists = false;
                    foreach (ResourceGroupInfo group in groups)
                    {
                        if (group.GroupId == newGroup.GroupId)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                        continue;

                    groups.Add(newGroup);
                }
            }

            gvResourceGroups.DataSource = groups;
            gvResourceGroups.DataBind();
        }

        protected List<ResourceGroupInfo> GetGridViewResourceGroups(SelectedState state)
        {
            List<ResourceGroupInfo> groups = new List<ResourceGroupInfo>();

            for (int i = 0; i < gvResourceGroups.Rows.Count; i++)
            {
                GridViewRow row = gvResourceGroups.Rows[i];
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect == null)
                    continue;

                ResourceGroupInfo user = new ResourceGroupInfo();

                user.GroupName = ((Literal)row.FindControl("litGroupName")).Text;
                user.GroupId = Convert.ToInt32(((HiddenField)row.FindControl("hdnGroupId")).Value);

                if (state == SelectedState.All ||
                    (state == SelectedState.Selected && chkSelect.Checked) ||
                    (state == SelectedState.Unselected && !chkSelect.Checked))
                    groups.Add(user);
            }

            return groups;
        }

        protected List<ResourceGroupInfo> GetGridViewResourceGroups()
        {
            var groups = new List<ResourceGroupInfo>();

            for (int i = 0; i < gvPopupResourceGroups.Rows.Count; i++)
            {
                GridViewRow row = gvPopupResourceGroups.Rows[i];
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect == null)
                    continue;

                if (chkSelect.Checked)
                {
                    groups.Add(new ResourceGroupInfo
                    {
                        GroupName = ((Literal)row.FindControl("litGroupName")).Text,
                        GroupId = Convert.ToInt32(((HiddenField)row.FindControl("hdnGroupId")).Value)
                    });
                }
            }

            return groups;

        }

        protected void cmdSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindPopupResourceGroups();
        }

        protected SortDirection Direction
        {
            get { return ViewState[DirectionString] == null ? SortDirection.Descending : (SortDirection)ViewState[DirectionString]; }
            set { ViewState[DirectionString] = value; }
        }

        protected static int CompareResourceGroups(ResourceGroupInfo group1, ResourceGroupInfo group2)
        {
            return string.Compare(group1.GroupName, group2.GroupName);
        }

        protected string LocalizeGroup(string groupName)
        {
            return GetLocalizedString(groupName) ?? groupName;
        }
    }
}
