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

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 
namespace WebsitePanel.EnterpriseServer
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;

    using WebsitePanel.Providers;
    using WebsitePanel.Providers.OS;

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "esOperatingSystemsSoap", Namespace = "http://smbsaas/websitepanel/enterpriseserver")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ServiceProviderItem))]
    public partial class esOperatingSystems : Microsoft.Web.Services3.WebServicesClientProtocol
    {

        private System.Threading.SendOrPostCallback GetRawOdbcSourcesPagedOperationCompleted;

        private System.Threading.SendOrPostCallback GetInstalledOdbcDriversOperationCompleted;

        private System.Threading.SendOrPostCallback GetOdbcSourcesOperationCompleted;

        private System.Threading.SendOrPostCallback GetOdbcSourceOperationCompleted;

        private System.Threading.SendOrPostCallback AddOdbcSourceOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateOdbcSourceOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteOdbcSourceOperationCompleted;

        /// <remarks/>
        public esOperatingSystems()
        {
            this.Url = "http://localhost/WebsitePanelEnterpriseServer11/esOperatingSystems.asmx";
        }

        /// <remarks/>
        public event GetRawOdbcSourcesPagedCompletedEventHandler GetRawOdbcSourcesPagedCompleted;

        /// <remarks/>
        public event GetInstalledOdbcDriversCompletedEventHandler GetInstalledOdbcDriversCompleted;

        /// <remarks/>
        public event GetOdbcSourcesCompletedEventHandler GetOdbcSourcesCompleted;

        /// <remarks/>
        public event GetOdbcSourceCompletedEventHandler GetOdbcSourceCompleted;

        /// <remarks/>
        public event AddOdbcSourceCompletedEventHandler AddOdbcSourceCompleted;

        /// <remarks/>
        public event UpdateOdbcSourceCompletedEventHandler UpdateOdbcSourceCompleted;

        /// <remarks/>
        public event DeleteOdbcSourceCompletedEventHandler DeleteOdbcSourceCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetRawOdbcSourcesPaged", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetRawOdbcSourcesPaged(int packageId, string filterColumn, string filterValue, string sortColumn, int startRow, int maximumRows)
        {
            object[] results = this.Invoke("GetRawOdbcSourcesPaged", new object[] {
                        packageId,
                        filterColumn,
                        filterValue,
                        sortColumn,
                        startRow,
                        maximumRows});
            return ((System.Data.DataSet)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetRawOdbcSourcesPaged(int packageId, string filterColumn, string filterValue, string sortColumn, int startRow, int maximumRows, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetRawOdbcSourcesPaged", new object[] {
                        packageId,
                        filterColumn,
                        filterValue,
                        sortColumn,
                        startRow,
                        maximumRows}, callback, asyncState);
        }

        /// <remarks/>
        public System.Data.DataSet EndGetRawOdbcSourcesPaged(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }

        /// <remarks/>
        public void GetRawOdbcSourcesPagedAsync(int packageId, string filterColumn, string filterValue, string sortColumn, int startRow, int maximumRows)
        {
            this.GetRawOdbcSourcesPagedAsync(packageId, filterColumn, filterValue, sortColumn, startRow, maximumRows, null);
        }

        /// <remarks/>
        public void GetRawOdbcSourcesPagedAsync(int packageId, string filterColumn, string filterValue, string sortColumn, int startRow, int maximumRows, object userState)
        {
            if ((this.GetRawOdbcSourcesPagedOperationCompleted == null))
            {
                this.GetRawOdbcSourcesPagedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRawOdbcSourcesPagedOperationCompleted);
            }
            this.InvokeAsync("GetRawOdbcSourcesPaged", new object[] {
                        packageId,
                        filterColumn,
                        filterValue,
                        sortColumn,
                        startRow,
                        maximumRows}, this.GetRawOdbcSourcesPagedOperationCompleted, userState);
        }

        private void OnGetRawOdbcSourcesPagedOperationCompleted(object arg)
        {
            if ((this.GetRawOdbcSourcesPagedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRawOdbcSourcesPagedCompleted(this, new GetRawOdbcSourcesPagedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetInstalledOdbcDrivers", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] GetInstalledOdbcDrivers(int packageId)
        {
            object[] results = this.Invoke("GetInstalledOdbcDrivers", new object[] {
                        packageId});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetInstalledOdbcDrivers(int packageId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetInstalledOdbcDrivers", new object[] {
                        packageId}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndGetInstalledOdbcDrivers(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void GetInstalledOdbcDriversAsync(int packageId)
        {
            this.GetInstalledOdbcDriversAsync(packageId, null);
        }

        /// <remarks/>
        public void GetInstalledOdbcDriversAsync(int packageId, object userState)
        {
            if ((this.GetInstalledOdbcDriversOperationCompleted == null))
            {
                this.GetInstalledOdbcDriversOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetInstalledOdbcDriversOperationCompleted);
            }
            this.InvokeAsync("GetInstalledOdbcDrivers", new object[] {
                        packageId}, this.GetInstalledOdbcDriversOperationCompleted, userState);
        }

        private void OnGetInstalledOdbcDriversOperationCompleted(object arg)
        {
            if ((this.GetInstalledOdbcDriversCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetInstalledOdbcDriversCompleted(this, new GetInstalledOdbcDriversCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetOdbcSources", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SystemDSN[] GetOdbcSources(int packageId, bool recursive)
        {
            object[] results = this.Invoke("GetOdbcSources", new object[] {
                        packageId,
                        recursive});
            return ((SystemDSN[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetOdbcSources(int packageId, bool recursive, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetOdbcSources", new object[] {
                        packageId,
                        recursive}, callback, asyncState);
        }

        /// <remarks/>
        public SystemDSN[] EndGetOdbcSources(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((SystemDSN[])(results[0]));
        }

        /// <remarks/>
        public void GetOdbcSourcesAsync(int packageId, bool recursive)
        {
            this.GetOdbcSourcesAsync(packageId, recursive, null);
        }

        /// <remarks/>
        public void GetOdbcSourcesAsync(int packageId, bool recursive, object userState)
        {
            if ((this.GetOdbcSourcesOperationCompleted == null))
            {
                this.GetOdbcSourcesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetOdbcSourcesOperationCompleted);
            }
            this.InvokeAsync("GetOdbcSources", new object[] {
                        packageId,
                        recursive}, this.GetOdbcSourcesOperationCompleted, userState);
        }

        private void OnGetOdbcSourcesOperationCompleted(object arg)
        {
            if ((this.GetOdbcSourcesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetOdbcSourcesCompleted(this, new GetOdbcSourcesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetOdbcSource", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SystemDSN GetOdbcSource(int itemId)
        {
            object[] results = this.Invoke("GetOdbcSource", new object[] {
                        itemId});
            return ((SystemDSN)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetOdbcSource(int itemId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetOdbcSource", new object[] {
                        itemId}, callback, asyncState);
        }

        /// <remarks/>
        public SystemDSN EndGetOdbcSource(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((SystemDSN)(results[0]));
        }

        /// <remarks/>
        public void GetOdbcSourceAsync(int itemId)
        {
            this.GetOdbcSourceAsync(itemId, null);
        }

        /// <remarks/>
        public void GetOdbcSourceAsync(int itemId, object userState)
        {
            if ((this.GetOdbcSourceOperationCompleted == null))
            {
                this.GetOdbcSourceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetOdbcSourceOperationCompleted);
            }
            this.InvokeAsync("GetOdbcSource", new object[] {
                        itemId}, this.GetOdbcSourceOperationCompleted, userState);
        }

        private void OnGetOdbcSourceOperationCompleted(object arg)
        {
            if ((this.GetOdbcSourceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetOdbcSourceCompleted(this, new GetOdbcSourceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/AddOdbcSource", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AddOdbcSource(SystemDSN item)
        {
            object[] results = this.Invoke("AddOdbcSource", new object[] {
                        item});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginAddOdbcSource(SystemDSN item, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("AddOdbcSource", new object[] {
                        item}, callback, asyncState);
        }

        /// <remarks/>
        public int EndAddOdbcSource(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void AddOdbcSourceAsync(SystemDSN item)
        {
            this.AddOdbcSourceAsync(item, null);
        }

        /// <remarks/>
        public void AddOdbcSourceAsync(SystemDSN item, object userState)
        {
            if ((this.AddOdbcSourceOperationCompleted == null))
            {
                this.AddOdbcSourceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddOdbcSourceOperationCompleted);
            }
            this.InvokeAsync("AddOdbcSource", new object[] {
                        item}, this.AddOdbcSourceOperationCompleted, userState);
        }

        private void OnAddOdbcSourceOperationCompleted(object arg)
        {
            if ((this.AddOdbcSourceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddOdbcSourceCompleted(this, new AddOdbcSourceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/UpdateOdbcSource", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int UpdateOdbcSource(SystemDSN item)
        {
            object[] results = this.Invoke("UpdateOdbcSource", new object[] {
                        item});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdateOdbcSource(SystemDSN item, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UpdateOdbcSource", new object[] {
                        item}, callback, asyncState);
        }

        /// <remarks/>
        public int EndUpdateOdbcSource(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void UpdateOdbcSourceAsync(SystemDSN item)
        {
            this.UpdateOdbcSourceAsync(item, null);
        }

        /// <remarks/>
        public void UpdateOdbcSourceAsync(SystemDSN item, object userState)
        {
            if ((this.UpdateOdbcSourceOperationCompleted == null))
            {
                this.UpdateOdbcSourceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOdbcSourceOperationCompleted);
            }
            this.InvokeAsync("UpdateOdbcSource", new object[] {
                        item}, this.UpdateOdbcSourceOperationCompleted, userState);
        }

        private void OnUpdateOdbcSourceOperationCompleted(object arg)
        {
            if ((this.UpdateOdbcSourceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateOdbcSourceCompleted(this, new UpdateOdbcSourceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/DeleteOdbcSource", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteOdbcSource(int itemId)
        {
            object[] results = this.Invoke("DeleteOdbcSource", new object[] {
                        itemId});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginDeleteOdbcSource(int itemId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("DeleteOdbcSource", new object[] {
                        itemId}, callback, asyncState);
        }

        /// <remarks/>
        public int EndDeleteOdbcSource(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void DeleteOdbcSourceAsync(int itemId)
        {
            this.DeleteOdbcSourceAsync(itemId, null);
        }

        /// <remarks/>
        public void DeleteOdbcSourceAsync(int itemId, object userState)
        {
            if ((this.DeleteOdbcSourceOperationCompleted == null))
            {
                this.DeleteOdbcSourceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteOdbcSourceOperationCompleted);
            }
            this.InvokeAsync("DeleteOdbcSource", new object[] {
                        itemId}, this.DeleteOdbcSourceOperationCompleted, userState);
        }

        private void OnDeleteOdbcSourceOperationCompleted(object arg)
        {
            if ((this.DeleteOdbcSourceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteOdbcSourceCompleted(this, new DeleteOdbcSourceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        //CO Changes
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/CheckFileServicesInstallation", RequestNamespace = "http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace = "http://smbsaas/websitepanel/enterpriseserver", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckFileServicesInstallation(int serviceId)
        {
            object[] results = this.Invoke("CheckFileServicesInstallation", new object[] {
                        serviceId});
            return ((bool)(results[0]));
        }
        //END

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetRawOdbcSourcesPagedCompletedEventHandler(object sender, GetRawOdbcSourcesPagedCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRawOdbcSourcesPagedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetRawOdbcSourcesPagedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public System.Data.DataSet Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetInstalledOdbcDriversCompletedEventHandler(object sender, GetInstalledOdbcDriversCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetInstalledOdbcDriversCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetInstalledOdbcDriversCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetOdbcSourcesCompletedEventHandler(object sender, GetOdbcSourcesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetOdbcSourcesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetOdbcSourcesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public SystemDSN[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SystemDSN[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetOdbcSourceCompletedEventHandler(object sender, GetOdbcSourceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetOdbcSourceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetOdbcSourceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public SystemDSN Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SystemDSN)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void AddOdbcSourceCompletedEventHandler(object sender, AddOdbcSourceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddOdbcSourceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal AddOdbcSourceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void UpdateOdbcSourceCompletedEventHandler(object sender, UpdateOdbcSourceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateOdbcSourceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UpdateOdbcSourceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void DeleteOdbcSourceCompletedEventHandler(object sender, DeleteOdbcSourceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteOdbcSourceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DeleteOdbcSourceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}
