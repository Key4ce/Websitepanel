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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace WebsitePanel.Providers.Virtualization
{
    public class ConfigFile
    {
        const string resultTemplate = @"
<?xml version=""1.0""?>
<items>
  {0}
</items>";

        const string itemTemplate = @"
  <item path=""{0}"" legacyNetworkAdapter=""{1}"" remoteDesktop=""{2}"" processVolume=""{3}"">
    <name>{4}</name>
    <description>{5}</description>
    <provisioning>
      {6}
      <vmconfig computerName=""{7}"" administratorPassword=""{8}"" networkAdapters=""{9}"" />
    </provisioning>
  </item>";

        const string sysprepTemplate = @"<sysprep file=""{0}""/>";

        public ConfigFile(string xml)
        {
            Xml = xml;
            Load(xml);
        }

        public ConfigFile(LibraryItem[] libraryItems)
        {
            LibraryItems = libraryItems;
            Load(libraryItems);
        }


        public LibraryItem[] LibraryItems { get; private set; }

        public string Xml { get; private set; }


        private void Load(string xmlText)
        {
            // create list
            List<LibraryItem> items = new List<LibraryItem>();

            if (string.IsNullOrEmpty(xmlText))
            {
                LibraryItems = items.ToArray();
                return;
            }

            // load xml
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlText);

            XmlNodeList nodeItems = xml.SelectNodes("/items/item");

            foreach (XmlNode nodeItem in nodeItems)
            {
                LibraryItem item = new LibraryItem();
                item.Path = nodeItem.Attributes["path"].Value;

                // optional attributes
                if (nodeItem.Attributes["diskSize"] != null)
                    item.DiskSize = Int32.Parse(nodeItem.Attributes["diskSize"].Value);

                if (nodeItem.Attributes["legacyNetworkAdapter"] != null)
                    item.LegacyNetworkAdapter = Boolean.Parse(nodeItem.Attributes["legacyNetworkAdapter"].Value);

                item.ProcessVolume = 0; // process (extend and sysprep) 1st volume by default
                if (nodeItem.Attributes["processVolume"] != null)
                    item.ProcessVolume = Int32.Parse(nodeItem.Attributes["processVolume"].Value);

                if (nodeItem.Attributes["remoteDesktop"] != null)
                    item.RemoteDesktop = Boolean.Parse(nodeItem.Attributes["remoteDesktop"].Value);

                // inner nodes
                item.Name = nodeItem.SelectSingleNode("name").InnerText;
                var descriptionNode = nodeItem.SelectSingleNode("description");
                if (descriptionNode != null)
                    item.Description = descriptionNode.InnerText;

                // sysprep files
                XmlNodeList nodesSyspep = nodeItem.SelectNodes("provisioning/sysprep");
                List<string> sysprepFiles = new List<string>();
                foreach (XmlNode nodeSyspep in nodesSyspep)
                {
                    if (nodeSyspep.Attributes["file"] != null)
                        sysprepFiles.Add(nodeSyspep.Attributes["file"].Value);
                }
                item.SysprepFiles = sysprepFiles.ToArray();

                // vmconfig
                XmlNode nodeVmConfig = nodeItem.SelectSingleNode("provisioning/vmconfig");
                if (nodeVmConfig != null)
                {
                    if (nodeVmConfig.Attributes["computerName"] != null)
                        item.ProvisionComputerName = Boolean.Parse(nodeVmConfig.Attributes["computerName"].Value);

                    if (nodeVmConfig.Attributes["administratorPassword"] != null)
                        item.ProvisionAdministratorPassword =
                            Boolean.Parse(nodeVmConfig.Attributes["administratorPassword"].Value);

                    if (nodeVmConfig.Attributes["networkAdapters"] != null)
                        item.ProvisionNetworkAdapters = Boolean.Parse(nodeVmConfig.Attributes["networkAdapters"].Value);
                }

                items.Add(item);
            }

            LibraryItems = items.ToArray();
        }

        private void Load(LibraryItem[] libraryItems)
        {
            List<string> items = new List<string>();

            foreach (var libraryItem in libraryItems)
            {
                var sysprep = "";

                if (libraryItem.SysprepFiles != null && libraryItem.SysprepFiles.Any())
                    sysprep = string.Join("", libraryItem.SysprepFiles.Select(s => string.Format(sysprepTemplate, s)).ToArray());

                items.Add(string.Format(itemTemplate, libraryItem.Path, libraryItem.LegacyNetworkAdapter,
                    libraryItem.RemoteDesktop, libraryItem.ProcessVolume, libraryItem.Name, libraryItem.Description,
                    sysprep, libraryItem.ProvisionComputerName, libraryItem.ProvisionAdministratorPassword,
                    libraryItem.ProvisionNetworkAdapters));
            }

            Xml = string.Format(resultTemplate, string.Join("", items.ToArray()));
        }
    }
}
