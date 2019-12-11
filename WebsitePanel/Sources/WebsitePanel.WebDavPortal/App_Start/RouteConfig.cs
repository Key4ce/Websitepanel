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

using System.Web.Mvc;
using System.Web.Routing;
using WebsitePanel.WebDavPortal.UI.Routes;

namespace WebsitePanel.WebDavPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Account

            routes.MapRoute(
               name: AccountRouteNames.PhoneNumberIsAvailible,
               url: "account/profile/phone-number-availible",
               defaults: new { controller = "Account", action = "PhoneNumberIsAvailible" }
               );

            routes.MapRoute(
                name: AccountRouteNames.UserProfile,
                url: "account/profile",
                defaults: new { controller = "Account", action = "UserProfile" }
                );

            routes.MapRoute(
                name: AccountRouteNames.PasswordResetLogin,
                url: "account/password-reset/step-1",
                defaults: new { controller = "Account", action = "PasswordResetLogin" }
                );

            routes.MapRoute(
                name: AccountRouteNames.PasswordResetPincodeSendOptions,
                url: "account/password-reset/step-2/{token}",
                defaults: new { controller = "Account", action = "PasswordResetPincodeSendOptions" }
                );

            routes.MapRoute(
                name: AccountRouteNames.PasswordResetPincode,
                url: "account/password-reset/step-3/{token}",
                defaults: new { controller = "Account", action = "PasswordResetPincode" }
                );

            routes.MapRoute(
                name: AccountRouteNames.PasswordResetFinalStep,
                url: "account/password-reset/step-final/{token}/{pincode}",
                defaults: new { controller = "Account", action = "PasswordResetFinalStep", pincode = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: AccountRouteNames.PasswordResetSuccess,
                url: "account/password-reset/success",
                defaults: new { controller = "Account", action = "PasswordSuccessfullyChanged" }
                );

            routes.MapRoute(
                name: AccountRouteNames.PasswordChange,
                url: "account/profile/password-change",
                defaults: new { controller = "Account", action = "PasswordChange" }
                );

            routes.MapRoute(
                name: AccountRouteNames.Logout,
                url: "account/logout",
                defaults: new { controller = "Account", action = "Logout" }
                );

            routes.MapRoute(
                name: AccountRouteNames.Login,
                url: "account/login",
                defaults: new { controller = "Account", action = "Login" }
                ); 

            #endregion

            #region Owa

            routes.MapRoute(
                name: FileSystemRouteNames.ViewOfficeOnline,
                url: "office365/view/{org}/{*pathPart}",
                defaults:
                    new {controller = "FileSystem", action = "ViewOfficeDocument", pathPart = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: FileSystemRouteNames.EditOfficeOnline,
                url: "office365/edit/{org}/{*pathPart}",
                defaults:
                    new {controller = "FileSystem", action = "EditOfficeDocument", pathPart = UrlParameter.Optional}
                );

            #endregion

            #region Enterprise storage 

            routes.MapRoute(
                name: FileSystemRouteNames.ItemExist,
                url: "storage/item-exist/{org}/{*pathPart}",
                defaults:
                    new { controller = "FileSystem", action = "ItemExist", pathPart = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: FileSystemRouteNames.NewWebDavItem,
                url: "storage/new/{org}/{*pathPart}",
                defaults:
                    new { controller = "FileSystem", action = "NewWebDavItem", pathPart = UrlParameter.Optional }
                );

            routes.MapRoute(
                    name: FileSystemRouteNames.SearchFiles,
                    url: "storage/search/{org}/{*pathPart}",
                    defaults: new { controller = "FileSystem", action = "SearchFiles", pathPart = UrlParameter.Optional }
                    );

            routes.MapRoute(
                    name: FileSystemRouteNames.SearchFilesContent,
                    url: "storage/ajax/search/{org}/{*pathPart}",
                    defaults: new { controller = "FileSystem", action = "SearchFilesContent", pathPart = UrlParameter.Optional }
                    );

            routes.MapRoute(
                    name: FileSystemRouteNames.ChangeWebDavViewType,
                    url: "storage/change-view-type/{viewType}",
                    defaults: new { controller = "FileSystem", action = "ChangeViewType" }
                    );

            routes.MapRoute(
                    name: FileSystemRouteNames.DeleteFiles,
                    url: "storage/files-group-action/delete",
                    defaults: new { controller = "FileSystem", action = "DeleteFiles" }
                    );

            routes.MapRoute(
                name: FileSystemRouteNames.UploadFile,
                url: "storage/upload-files/{org}/{*pathPart}",
                defaults: new { controller = "FileSystem", action = "UploadFiles" }
                );

            routes.MapRoute(
                name: FileSystemRouteNames.DownloadFile,
                url: "storage/download-file/{org}/{*pathPart}",
                defaults: new { controller = "FileSystem", action = "DownloadFile" }
                );

            routes.MapRoute(
                name: FileSystemRouteNames.ShowAdditionalContent,
                url: "storage/show-additional-content/{*path}",
                defaults: new { controller = "FileSystem", action = "ShowAdditionalContent", path = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: FileSystemRouteNames.ShowContentDetails,
                url: "storage/details/{org}/{*pathPart}",
                defaults: new { controller = "FileSystem", action = "GetContentDetails", pathPart = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: FileSystemRouteNames.ShowContentPath,
                url: "{org}/{*pathPart}",
                defaults: new { controller = "FileSystem", action = "ShowContent", pathPart = UrlParameter.Optional }
                ); 
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "Login" }
            );
        }
    }
}
