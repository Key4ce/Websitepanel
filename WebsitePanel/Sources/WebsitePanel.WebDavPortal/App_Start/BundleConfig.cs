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

using System.Web.Optimization;

namespace WebsitePanel.WebDavPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var jQueryBundle = new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.cookie.js");

            jQueryBundle.IncludeDirectory("~/Scripts", "jquery.dataTables.min.js", true);
            jQueryBundle.IncludeDirectory("~/Scripts", "dataTables.bootstrap.js", true);

            bundles.Add(jQueryBundle);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/appScripts/validation/passwordeditor.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/appScripts").Include(
                "~/Scripts/appScripts/messages.js",
                "~/Scripts/appScripts/fileBrowsing.js",
                "~/Scripts/appScripts/dialogs.js",
                "~/Scripts/appScripts/wsp.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/appScripts-webdav").Include(
                "~/Scripts/appScripts/wsp-webdav.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/bigIconsScripts").Include(
                "~/Scripts/appScripts/recalculateResourseHeight.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/authScripts").Include(
               "~/Scripts/appScripts/authentication.js"));

            bundles.Add(new ScriptBundle("~/bundles/file-upload").Include(
                "~/Scripts/jquery.ui.widget.js",
                "~/Scripts/jQuery.FileUpload/tmpl.min.js",
                "~/Scripts/jQuery.FileUpload/load-image.min.js",
                "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-image.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js"
               ));

            var styleBundle = new StyleBundle("~/Content/css");

            styleBundle.Include(
                "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css",
                "~/Content/bootstrap.css",
                "~/Content/site.css");

            styleBundle.IncludeDirectory("~/Content", "jquery.datatables.css", true);
            styleBundle.IncludeDirectory("~/Content", "dataTables.bootstrap.css", true);

            bundles.Add(styleBundle);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
