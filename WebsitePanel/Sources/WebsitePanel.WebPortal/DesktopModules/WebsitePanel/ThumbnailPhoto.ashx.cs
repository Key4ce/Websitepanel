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
using WebsitePanel.EnterpriseServer;
using WebsitePanel.EnterpriseServer.Base.HostedSolution;
using WebsitePanel.Providers.HostedSolution;
using WebsitePanel.Providers.Common;
using WebsitePanel.Providers.ResultObjects;
using System.Drawing;
using System.IO;

namespace WebsitePanel.Portal
{
    /// <summary>
    /// Summary description for ThumbnailPhoto
    /// </summary>
    public class ThumbnailPhoto : IHttpHandler
    {
        public HttpContext Context = null;

        public int Param(string key)
        {
            string val = Context.Request.QueryString[key];
            if (val == null)
            {
                val = Context.Request.Form[key];
                if (val == null) return 0;
            }

            int res = 0;
            int.TryParse(val, out res);

            return res;
        }



        public void ProcessRequest(HttpContext context)
        {
            Context = context;

            int ItemID = Param("ItemID");
            int AccountID = Param("AccountID");

            BytesResult res = ES.Services.ExchangeServer.GetPicture(ItemID, AccountID);
            if ((res!=null)&&(res.IsSuccess) && (res.Value != null))
            {
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(res.Value);
            }
            else
            {
                MemoryStream pictureStream = new MemoryStream();
                Bitmap emptyBmp = new Bitmap(1, 1);
                emptyBmp.Save(pictureStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(pictureStream.ToArray());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
