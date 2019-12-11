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
namespace WebsitePanel.Providers.OCS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OCSEdgeServerSoap", Namespace="http://smbsaas/websitepanel/server/")]
    public partial class OCSEdgeServer : Microsoft.Web.Services3.WebServicesClientProtocol {
        
        public ServiceProviderSettingsSoapHeader ServiceProviderSettingsSoapHeaderValue;
        
        private System.Threading.SendOrPostCallback AddDomainOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteDomainOperationCompleted;
        
        /// <remarks/>
        public OCSEdgeServer() {
            this.Url = "http://exchange-dev:9003/OCSEdgeServer.asmx";
        }
        
        /// <remarks/>
        public event AddDomainCompletedEventHandler AddDomainCompleted;
        
        /// <remarks/>
        public event DeleteDomainCompletedEventHandler DeleteDomainCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/server/AddDomain", RequestNamespace="http://smbsaas/websitepanel/server/", ResponseNamespace="http://smbsaas/websitepanel/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddDomain(string domainName) {
            this.Invoke("AddDomain", new object[] {
                        domainName});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAddDomain(string domainName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("AddDomain", new object[] {
                        domainName}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAddDomain(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void AddDomainAsync(string domainName) {
            this.AddDomainAsync(domainName, null);
        }
        
        /// <remarks/>
        public void AddDomainAsync(string domainName, object userState) {
            if ((this.AddDomainOperationCompleted == null)) {
                this.AddDomainOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddDomainOperationCompleted);
            }
            this.InvokeAsync("AddDomain", new object[] {
                        domainName}, this.AddDomainOperationCompleted, userState);
        }
        
        private void OnAddDomainOperationCompleted(object arg) {
            if ((this.AddDomainCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddDomainCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/websitepanel/server/DeleteDomain", RequestNamespace="http://smbsaas/websitepanel/server/", ResponseNamespace="http://smbsaas/websitepanel/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteDomain(string domainName) {
            this.Invoke("DeleteDomain", new object[] {
                        domainName});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDeleteDomain(string domainName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DeleteDomain", new object[] {
                        domainName}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndDeleteDomain(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void DeleteDomainAsync(string domainName) {
            this.DeleteDomainAsync(domainName, null);
        }
        
        /// <remarks/>
        public void DeleteDomainAsync(string domainName, object userState) {
            if ((this.DeleteDomainOperationCompleted == null)) {
                this.DeleteDomainOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteDomainOperationCompleted);
            }
            this.InvokeAsync("DeleteDomain", new object[] {
                        domainName}, this.DeleteDomainOperationCompleted, userState);
        }
        
        private void OnDeleteDomainOperationCompleted(object arg) {
            if ((this.DeleteDomainCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteDomainCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void AddDomainCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void DeleteDomainCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}
