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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebsitePanel.Providers.HostedSolution;
using WebsitePanel.WebDav.Core;
using WebsitePanel.WebDav.Core.Config;

namespace WebsitePanel.WebDavPortal.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class OrganizationPasswordPolicyAttribute : ValidationAttribute, IClientValidatable
    {
        public int ItemId { get; private set; }

        public OrganizationPasswordPolicyAttribute()
        {
            if (WspContext.User != null)
            {
                ItemId = WspContext.User.ItemId;
            }
            else if (HttpContext.Current != null && HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.ItemId] != null)
            {
                ItemId = (int)HttpContext.Current.Session[WebDavAppConfigManager.Instance.SessionKeys.ItemId];
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var resultMessages = new List<string>();

                var settings = WspContext.Services.Organizations.GetOrganizationPasswordSettings(ItemId);

                if (settings != null)
                {
                    var valueString = value.ToString();

                    if (valueString.Length < settings.MinimumLength)
                    {
                        resultMessages.Add(string.Format(Resources.Messages.PasswordMinLengthFormat,
                            settings.MinimumLength));
                    }

                    if (valueString.Length > settings.MaximumLength)
                    {
                        resultMessages.Add(string.Format(Resources.Messages.PasswordMaxLengthFormat,
                            settings.MaximumLength));
                    }

                    if (settings.PasswordComplexityEnabled)
                    {
                        var numbersCount = valueString.Count(Char.IsDigit);
                        var upperLetterCount = valueString.Count(Char.IsUpper);
                        var symbolsCount = Regex.Matches(valueString, @"[~!@#$%^&*_\-+'\|\\(){}\[\]:;\""'<>,.?/]").Count;

                        if (upperLetterCount < settings.UppercaseLettersCount)
                        {
                            resultMessages.Add(string.Format(Resources.Messages.PasswordUppercaseCountFormat,
                                settings.UppercaseLettersCount));
                        }

                        if (numbersCount < settings.NumbersCount)
                        {
                            resultMessages.Add(string.Format(Resources.Messages.PasswordNumbersCountFormat,
                                settings.NumbersCount));
                        }

                        if (symbolsCount < settings.SymbolsCount)
                        {
                            resultMessages.Add(string.Format(Resources.Messages.PasswordSymbolsCountFormat,
                                settings.SymbolsCount));
                        }
                    }

                }

                return resultMessages.Any()?  new ValidationResult(string.Join("<br>", resultMessages)) : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var settings = WspContext.Services.Organizations.GetOrganizationPasswordSettings(ItemId);

            var rule = new ModelClientValidationRule();

            rule.ErrorMessage = string.Format(Resources.Messages.PasswordMinLengthFormat, settings.MinimumLength);
            rule.ValidationParameters.Add("count", settings.MinimumLength);
            rule.ValidationType = "minimumlength";

            yield return rule;

            rule = new ModelClientValidationRule();

            rule.ErrorMessage = string.Format(Resources.Messages.PasswordMaxLengthFormat, settings.MaximumLength);
            rule.ValidationParameters.Add("count", settings.MaximumLength);
            rule.ValidationType = "maximumlength";

            yield return rule;

            if (settings.PasswordComplexityEnabled)
            {
                rule = new ModelClientValidationRule();

                rule.ErrorMessage = string.Format(Resources.Messages.PasswordUppercaseCountFormat, settings.UppercaseLettersCount);
                rule.ValidationParameters.Add("count", settings.UppercaseLettersCount);
                rule.ValidationType = "uppercasecount";

                yield return rule;

                rule = new ModelClientValidationRule();

                rule.ErrorMessage = string.Format(Resources.Messages.PasswordNumbersCountFormat, settings.NumbersCount);
                rule.ValidationParameters.Add("count", settings.NumbersCount);
                rule.ValidationType = "numberscount";

                yield return rule;

                rule = new ModelClientValidationRule();

                rule.ErrorMessage = string.Format(Resources.Messages.PasswordSymbolsCountFormat, settings.SymbolsCount);
                rule.ValidationParameters.Add("count", settings.SymbolsCount);
                rule.ValidationType = "symbolscount";

                yield return rule;
            }
        }

    }
}
