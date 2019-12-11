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
using System.Net.Mail;
using WebsitePanel.Providers.HostedSolution;

namespace WebsitePanel.EnterpriseServer
{
    public class UserPasswordExpirationNotificationTask : SchedulerTask
    {
        // Input parameters:
        private static readonly string DaysBeforeNotify = "DAYS_BEFORE_EXPIRATION";

        public override void DoWork()
        {
            BackgroundTask topTask = TaskManager.TopTask;

            int daysBeforeNotify;

            // check input parameters
            if (!int.TryParse((string)topTask.GetParamValue(DaysBeforeNotify), out daysBeforeNotify))
            {
                TaskManager.WriteWarning("Specify 'Notify before (days)' task parameter");
                return;
            }

            OrganizationController.DeleteAllExpiredTokens();

            var owner = UserController.GetUser(topTask.EffectiveUserId);

            var packages = PackageController.GetMyPackages(topTask.EffectiveUserId);

            foreach (var package in packages)
            {
                var organizations = ExchangeServerController.GetExchangeOrganizations(package.PackageId, true);
                
                foreach (var organization in organizations)
                {
                    var usersWithExpiredPasswords = OrganizationController.GetOrganizationUsersWithExpiredPassword(organization.Id, daysBeforeNotify);

                    var generalSettings = OrganizationController.GetOrganizationGeneralSettings(organization.Id);

                    var logoUrl = generalSettings != null ? generalSettings.OrganizationLogoUrl : string.Empty;

                    foreach (var user in usersWithExpiredPasswords)
                    {
                        user.ItemId = organization.Id;

                        if (string.IsNullOrEmpty(user.PrimaryEmailAddress))
                        {
                            TaskManager.WriteWarning(string.Format("Unable to send email to {0} user (organization: {1}), user primary email address is not set.", user.DisplayName, organization.OrganizationId));
                            continue;
                        }

                        OrganizationController.SendUserExpirationPasswordEmail(owner, user, "Scheduler Password Expiration Notification", user.PrimaryEmailAddress, logoUrl);
                    }
                }
            }
        }
    }
}
