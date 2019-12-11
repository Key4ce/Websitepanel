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
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebsitePanel.WebDavPortal.Models.Common.DataTable;


namespace WebsitePanel.WebDavPortal.ModelBinders.DataTables
{
    public class JqueryDataTableModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.BindModel(controllerContext, bindingContext);
            HttpRequestBase request = controllerContext.HttpContext.Request;

            // Retrieve request data
            int draw = Convert.ToInt32(request["draw"]);
            int start = Convert.ToInt32(request["start"]);
            int count = Convert.ToInt32(request["length"]);

            // Search
            var search = new JqueryDataTableSearch
            {
                Value = request["search[value]"],
                IsRegex = Convert.ToBoolean(request["search[regex]"])
            };

            var orderIndex = 0;

            var orders = new List<JqueryDataTableOrder>();

            while (request["order[" + orderIndex + "][column]"] != null)
            {
                orders.Add(new JqueryDataTableOrder()
                {
                    Column = Convert.ToInt32(request["order[" + orderIndex + "][column]"]),
                    Ascending = (request["order[" + orderIndex + "][dir]"] == "asc")
                });

                orderIndex++;
            }

            // Columns
            var columnsIndex = 0;
            var columns = new List<JqueryDataTableColumn>();

            while (request["columns[" + columnsIndex + "][name]"] != null)
            {
                columns.Add(new JqueryDataTableColumn
                {
                    Data = request["columns[" + columnsIndex + "][data]"],
                    Name = request["columns[" + columnsIndex + "][name]"],
                    Orderable = Convert.ToBoolean(request["columns[" + columnsIndex + "][orderable]"]),
                    Search = new JqueryDataTableSearch
                    {
                        Value = request["columns[" + columnsIndex + "][search][value]"],
                        IsRegex = Convert.ToBoolean(request["columns[" + columnsIndex + "][search][regex]"])
                    }
                });

                columnsIndex++;
            }

            return new JqueryDataTableRequest
            {
                Draw = draw,
                Start = start,
                Count = count,
                Search = search,
                Orders = orders,
                Columns = columns
            };
        }
         
    }
}
