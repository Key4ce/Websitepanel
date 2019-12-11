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
using System.Net;

namespace WebsitePanel.WebDav.Core
{
    /// <summary>
    /// WebDav.Client Namespace.
    /// </summary>
    /// <see cref="http://doc.webdavsystem.com/ITHit.WebDAV.Client.html"/>
    namespace Client
    {
        public class WebDavSession
        {
            public NetworkCredential Credentials { get; set; }

            /// <summary>
            ///     Returns IFolder corresponding to path.
            /// </summary>
            /// <param name="path">Path to the folder.</param>
            /// <returns>Folder corresponding to requested path.</returns>
            public IFolder OpenFolder(string path)
            {
                var folder = new WebDavFolder();
                folder.SetCredentials(Credentials);
                folder.Open(path);
                return folder;
            }

            /// <summary>
            ///     Returns IFolder corresponding to path.
            /// </summary>
            /// <param name="path">Path to the folder.</param>
            /// <returns>Folder corresponding to requested path.</returns>
            public IFolder OpenFolder(Uri path)
            {
                var folder = new WebDavFolder();
                folder.SetCredentials(Credentials);
                folder.Open(path);
                return folder;
            }

            /// <summary>
            ///     Returns IFolder corresponding to path.
            /// </summary>
            /// <param name="path">Path to the folder.</param>
            /// <returns>Folder corresponding to requested path.</returns>
            public IFolder OpenFolderPaged(string path)
            {
                var folder = new WebDavFolder();
                folder.SetCredentials(Credentials);
                folder.OpenPaged(path);
                return folder;
            }

            /// <summary>
            ///     Returns IResource corresponding to path.
            /// </summary>
            /// <param name="path">Path to the resource.</param>
            /// <returns>Resource corresponding to requested path.</returns>
            public IResource OpenResource(string path)
            {
                return OpenResource(new Uri(path));
            }

            /// <summary>
            ///     Returns IResource corresponding to path.
            /// </summary>
            /// <param name="path">Path to the resource.</param>
            /// <returns>Resource corresponding to requested path.</returns>
            public IResource OpenResource(Uri path)
            {
                IFolder folder = OpenFolder(path);
                return folder.GetResource(path.Segments[path.Segments.Length - 1]);
            }
        }
    }
}
