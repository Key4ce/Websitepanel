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
using System.Text;

namespace WebsitePanel.EnterpriseServer.Code.Virtualization2012
{
    public static class QuotaHelper
    {
        public static void CheckNumericQuota(PackageContext cntx, List<string> errors, string quotaName, long currentVal, long val, string messageKey)
        {
            CheckQuotaValue(cntx, errors, quotaName, currentVal, val, messageKey);
        }
        public static void CheckNumericQuota(PackageContext cntx, List<string> errors, string quotaName, int currentVal, int val, string messageKey)
        {
            CheckQuotaValue(cntx, errors, quotaName, Convert.ToInt64(currentVal), Convert.ToInt64(val), messageKey);
        }

        public static void CheckNumericQuota(PackageContext cntx, List<string> errors, string quotaName, int val, string messageKey)
        {
            CheckQuotaValue(cntx, errors, quotaName, 0, val, messageKey);
        }

        public static void CheckBooleanQuota(PackageContext cntx, List<string> errors, string quotaName, bool val, string messageKey)
        {
            CheckQuotaValue(cntx, errors, quotaName, 0, val ? 1 : 0, messageKey);
        }

        public static void CheckListsQuota(PackageContext cntx, List<string> errors, string quotaName, string messageKey)
        {
            CheckQuotaValue(cntx, errors, quotaName, 0, -1, messageKey);
        }

        public static void CheckQuotaValue(PackageContext cntx, List<string> errors, string quotaName, long currentVal, long val, string messageKey)
        {
            if (!cntx.Quotas.ContainsKey(quotaName))
                return;

            QuotaValueInfo quota = cntx.Quotas[quotaName];

            if (val == -1 && quota.QuotaExhausted) // check if quota already reached
            {
                errors.Add(messageKey + ":" + quota.QuotaAllocatedValue);
            }
            else if (quota.QuotaAllocatedValue == -1)
                return; // unlimited
            else if (quota.QuotaTypeId == 1 && val == 1 && quota.QuotaAllocatedValue == 0) // bool quota
                errors.Add(messageKey);
            else if (quota.QuotaTypeId == 2)
            {
                long maxValue = quota.QuotaAllocatedValue - quota.QuotaUsedValue + currentVal;
                if (val > maxValue)
                    errors.Add(messageKey + ":" + maxValue);
            }
            else if (quota.QuotaTypeId == 3 && val > quota.QuotaAllocatedValue)
            {
                int maxValue = quota.QuotaAllocatedValue;
                errors.Add(messageKey + ":" + maxValue);
            }
        }
    }
}
