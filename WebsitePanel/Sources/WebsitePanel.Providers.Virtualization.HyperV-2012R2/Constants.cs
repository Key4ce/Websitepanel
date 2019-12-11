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
using System.Threading.Tasks;

namespace WebsitePanel.Providers.Virtualization
{
    public static class Constants
    {
        public const string CONFIG_USE_DISKPART_TO_CLEAR_READONLY_FLAG = "WebsitePanel.HyperV.UseDiskPartClearReadOnlyFlag";
        public const string WMI_VIRTUALIZATION_NAMESPACE = @"root\virtualization\v2";
        public const string WMI_CIMV2_NAMESPACE = @"root\cimv2";

        public const string LIBRARY_INDEX_FILE_NAME = "index.xml";

        public const string EXTERNAL_NETWORK_ADAPTER_NAME = "External Network Adapter";
        public const string PRIVATE_NETWORK_ADAPTER_NAME = "Private Network Adapter";
        public const string MANAGEMENT_NETWORK_ADAPTER_NAME = "Management Network Adapter";

        public const Int64 Size1G = 0x40000000;
        public const Int64 Size1M = 0x100000;
        public const Int64 Size1K = 1024;

        public const string KVP_RAM_SUMMARY_KEY = "VM-RAM-Summary";
        public const string KVP_HDD_SUMMARY_KEY = "VM-HDD-Summary";
    }
}
