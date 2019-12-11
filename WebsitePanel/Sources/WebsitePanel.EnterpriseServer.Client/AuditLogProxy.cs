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
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 
namespace WebsitePanel.EnterpriseServer {
    using System.Xml.Serialization;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Diagnostics;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="esAuditLogSoap", Namespace="http://smbsaas/websitepanel/enterpriseserver")]
    public partial class esAuditLog : Microsoft.Web.Services3.WebServicesClientProtocol {
        
        private System.Threading.SendOrPostCallback GetAuditLogRecordsPagedOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAuditLogSourcesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAuditLogTasksOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAuditLogRecordOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteAuditLogRecordsOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteAuditLogRecordsCompleteOperationCompleted;
        
        /// <remarks/>
        public esAuditLog() {
            this.Url = "http://127.0.0.1:9002/esAuditLog.asmx";
        }
        
        /// <remarks/>
        public event GetAuditLogRecordsPagedCompletedEventHandler GetAuditLogRecordsPagedCompleted;
        
        /// <remarks/>
        public event GetAuditLogSourcesCompletedEventHandler GetAuditLogSourcesCompleted;
        
        /// <remarks/>
        public event GetAuditLogTasksCompletedEventHandler GetAuditLogTasksCompleted;
        
        /// <remarks/>
        public event GetAuditLogRecordCompletedEventHandler GetAuditLogRecordCompleted;
        
        /// <remarks/>
        public event DeleteAuditLogRecordsCompletedEventHandler DeleteAuditLogRecordsCompleted;
        
        /// <remarks/>
        public event DeleteAuditLogRecordsCompleteCompletedEventHandler DeleteAuditLogRecordsCompleteCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetAuditLogRecordsPaged", RequestNamespace="http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace="http://smbsaas/websitepanel/enterpriseserver", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAuditLogRecordsPaged(int userId, int packageId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName, string sortColumn, int startRow, int maximumRows) {
            object[] results = this.Invoke("GetAuditLogRecordsPaged", new object[] {
                        userId,
                        packageId,
                        itemId,
                        itemName,
                        startDate,
                        endDate,
                        severityId,
                        sourceName,
                        taskName,
                        sortColumn,
                        startRow,
                        maximumRows});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAuditLogRecordsPaged(int userId, int packageId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName, string sortColumn, int startRow, int maximumRows, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAuditLogRecordsPaged", new object[] {
                        userId,
                        packageId,
                        itemId,
                        itemName,
                        startDate,
                        endDate,
                        severityId,
                        sourceName,
                        taskName,
                        sortColumn,
                        startRow,
                        maximumRows}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetAuditLogRecordsPaged(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAuditLogRecordsPagedAsync(int userId, int packageId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName, string sortColumn, int startRow, int maximumRows) {
            this.GetAuditLogRecordsPagedAsync(userId, packageId, itemId, itemName, startDate, endDate, severityId, sourceName, taskName, sortColumn, startRow, maximumRows, null);
        }
        
        /// <remarks/>
        public void GetAuditLogRecordsPagedAsync(int userId, int packageId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName, string sortColumn, int startRow, int maximumRows, object userState) {
            if ((this.GetAuditLogRecordsPagedOperationCompleted == null)) {
                this.GetAuditLogRecordsPagedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAuditLogRecordsPagedOperationCompleted);
            }
            this.InvokeAsync("GetAuditLogRecordsPaged", new object[] {
                        userId,
                        packageId,
                        itemId,
                        itemName,
                        startDate,
                        endDate,
                        severityId,
                        sourceName,
                        taskName,
                        sortColumn,
                        startRow,
                        maximumRows}, this.GetAuditLogRecordsPagedOperationCompleted, userState);
        }
        
        private void OnGetAuditLogRecordsPagedOperationCompleted(object arg) {
            if ((this.GetAuditLogRecordsPagedCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAuditLogRecordsPagedCompleted(this, new GetAuditLogRecordsPagedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetAuditLogSources", RequestNamespace="http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace="http://smbsaas/websitepanel/enterpriseserver", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAuditLogSources() {
            object[] results = this.Invoke("GetAuditLogSources", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAuditLogSources(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAuditLogSources", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetAuditLogSources(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAuditLogSourcesAsync() {
            this.GetAuditLogSourcesAsync(null);
        }
        
        /// <remarks/>
        public void GetAuditLogSourcesAsync(object userState) {
            if ((this.GetAuditLogSourcesOperationCompleted == null)) {
                this.GetAuditLogSourcesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAuditLogSourcesOperationCompleted);
            }
            this.InvokeAsync("GetAuditLogSources", new object[0], this.GetAuditLogSourcesOperationCompleted, userState);
        }
        
        private void OnGetAuditLogSourcesOperationCompleted(object arg) {
            if ((this.GetAuditLogSourcesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAuditLogSourcesCompleted(this, new GetAuditLogSourcesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetAuditLogTasks", RequestNamespace="http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace="http://smbsaas/websitepanel/enterpriseserver", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAuditLogTasks(string sourceName) {
            object[] results = this.Invoke("GetAuditLogTasks", new object[] {
                        sourceName});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAuditLogTasks(string sourceName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAuditLogTasks", new object[] {
                        sourceName}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataSet EndGetAuditLogTasks(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAuditLogTasksAsync(string sourceName) {
            this.GetAuditLogTasksAsync(sourceName, null);
        }
        
        /// <remarks/>
        public void GetAuditLogTasksAsync(string sourceName, object userState) {
            if ((this.GetAuditLogTasksOperationCompleted == null)) {
                this.GetAuditLogTasksOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAuditLogTasksOperationCompleted);
            }
            this.InvokeAsync("GetAuditLogTasks", new object[] {
                        sourceName}, this.GetAuditLogTasksOperationCompleted, userState);
        }
        
        private void OnGetAuditLogTasksOperationCompleted(object arg) {
            if ((this.GetAuditLogTasksCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAuditLogTasksCompleted(this, new GetAuditLogTasksCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/GetAuditLogRecord", RequestNamespace="http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace="http://smbsaas/websitepanel/enterpriseserver", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LogRecord GetAuditLogRecord(string recordId) {
            object[] results = this.Invoke("GetAuditLogRecord", new object[] {
                        recordId});
            return ((LogRecord)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAuditLogRecord(string recordId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAuditLogRecord", new object[] {
                        recordId}, callback, asyncState);
        }
        
        /// <remarks/>
        public LogRecord EndGetAuditLogRecord(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((LogRecord)(results[0]));
        }
        
        /// <remarks/>
        public void GetAuditLogRecordAsync(string recordId) {
            this.GetAuditLogRecordAsync(recordId, null);
        }
        
        /// <remarks/>
        public void GetAuditLogRecordAsync(string recordId, object userState) {
            if ((this.GetAuditLogRecordOperationCompleted == null)) {
                this.GetAuditLogRecordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAuditLogRecordOperationCompleted);
            }
            this.InvokeAsync("GetAuditLogRecord", new object[] {
                        recordId}, this.GetAuditLogRecordOperationCompleted, userState);
        }
        
        private void OnGetAuditLogRecordOperationCompleted(object arg) {
            if ((this.GetAuditLogRecordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAuditLogRecordCompleted(this, new GetAuditLogRecordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/DeleteAuditLogRecords", RequestNamespace="http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace="http://smbsaas/websitepanel/enterpriseserver", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteAuditLogRecords(int userId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName) {
            object[] results = this.Invoke("DeleteAuditLogRecords", new object[] {
                        userId,
                        itemId,
                        itemName,
                        startDate,
                        endDate,
                        severityId,
                        sourceName,
                        taskName});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDeleteAuditLogRecords(int userId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DeleteAuditLogRecords", new object[] {
                        userId,
                        itemId,
                        itemName,
                        startDate,
                        endDate,
                        severityId,
                        sourceName,
                        taskName}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDeleteAuditLogRecords(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteAuditLogRecordsAsync(int userId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName) {
            this.DeleteAuditLogRecordsAsync(userId, itemId, itemName, startDate, endDate, severityId, sourceName, taskName, null);
        }
        
        /// <remarks/>
        public void DeleteAuditLogRecordsAsync(int userId, int itemId, string itemName, System.DateTime startDate, System.DateTime endDate, int severityId, string sourceName, string taskName, object userState) {
            if ((this.DeleteAuditLogRecordsOperationCompleted == null)) {
                this.DeleteAuditLogRecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteAuditLogRecordsOperationCompleted);
            }
            this.InvokeAsync("DeleteAuditLogRecords", new object[] {
                        userId,
                        itemId,
                        itemName,
                        startDate,
                        endDate,
                        severityId,
                        sourceName,
                        taskName}, this.DeleteAuditLogRecordsOperationCompleted, userState);
        }
        
        private void OnDeleteAuditLogRecordsOperationCompleted(object arg) {
            if ((this.DeleteAuditLogRecordsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteAuditLogRecordsCompleted(this, new DeleteAuditLogRecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/enterpriseserver/DeleteAuditLogRecordsComplete", RequestNamespace="http://smbsaas/websitepanel/enterpriseserver", ResponseNamespace="http://smbsaas/websitepanel/enterpriseserver", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteAuditLogRecordsComplete() {
            object[] results = this.Invoke("DeleteAuditLogRecordsComplete", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDeleteAuditLogRecordsComplete(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DeleteAuditLogRecordsComplete", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDeleteAuditLogRecordsComplete(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteAuditLogRecordsCompleteAsync() {
            this.DeleteAuditLogRecordsCompleteAsync(null);
        }
        
        /// <remarks/>
        public void DeleteAuditLogRecordsCompleteAsync(object userState) {
            if ((this.DeleteAuditLogRecordsCompleteOperationCompleted == null)) {
                this.DeleteAuditLogRecordsCompleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteAuditLogRecordsCompleteOperationCompleted);
            }
            this.InvokeAsync("DeleteAuditLogRecordsComplete", new object[0], this.DeleteAuditLogRecordsCompleteOperationCompleted, userState);
        }
        
        private void OnDeleteAuditLogRecordsCompleteOperationCompleted(object arg) {
            if ((this.DeleteAuditLogRecordsCompleteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteAuditLogRecordsCompleteCompleted(this, new DeleteAuditLogRecordsCompleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetAuditLogRecordsPagedCompletedEventHandler(object sender, GetAuditLogRecordsPagedCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAuditLogRecordsPagedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAuditLogRecordsPagedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetAuditLogSourcesCompletedEventHandler(object sender, GetAuditLogSourcesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAuditLogSourcesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAuditLogSourcesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetAuditLogTasksCompletedEventHandler(object sender, GetAuditLogTasksCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAuditLogTasksCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAuditLogTasksCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetAuditLogRecordCompletedEventHandler(object sender, GetAuditLogRecordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAuditLogRecordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAuditLogRecordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public LogRecord Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LogRecord)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void DeleteAuditLogRecordsCompletedEventHandler(object sender, DeleteAuditLogRecordsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteAuditLogRecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteAuditLogRecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void DeleteAuditLogRecordsCompleteCompletedEventHandler(object sender, DeleteAuditLogRecordsCompleteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteAuditLogRecordsCompleteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteAuditLogRecordsCompleteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}
