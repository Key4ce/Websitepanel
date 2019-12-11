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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using Cobalt;
using WebsitePanel.WebDav.Core.Client;
using WebsitePanel.WebDav.Core.Config;
using WebsitePanel.WebDav.Core.Interfaces.Managers;
using WebsitePanel.WebDav.Core.Interfaces.Owa;
using WebsitePanel.WebDav.Core.Interfaces.Storages;

namespace WebsitePanel.WebDav.Core.Owa
{
    public class CobaltSessionManager : IWopiFileManager
    {
        private readonly IWebDavManager _webDavManager;
        private readonly IAccessTokenManager _tokenManager;
        private readonly ITtlStorage _storage;

        public CobaltSessionManager(IWebDavManager webDavManager, IAccessTokenManager tokenManager, ITtlStorage storage)
        {
            _webDavManager = webDavManager;

            _tokenManager = tokenManager;
            _storage = storage;
        }

        public CobaltFile Create(int accessTokenId)
        {
            var disposal = new DisposalEscrow(accessTokenId.ToString(CultureInfo.InvariantCulture));

            var content = new CobaltFilePartitionConfig
            {
                IsNewFile = true,
                HostBlobStore = new TemporaryHostBlobStore(new TemporaryHostBlobStore.Config(), disposal, accessTokenId + @".Content"),
                cellSchemaIsGenericFda = true,
                CellStorageConfig = new CellStorageConfig(),
                Schema = CobaltFilePartition.Schema.ShreddedCobalt,
                PartitionId = FilePartitionId.Content
            };

            var coauth = new CobaltFilePartitionConfig
            {
                IsNewFile = true,
                HostBlobStore = new TemporaryHostBlobStore(new TemporaryHostBlobStore.Config(), disposal, accessTokenId + @".CoauthMetadata"),
                cellSchemaIsGenericFda = false,
                CellStorageConfig = new CellStorageConfig(),
                Schema = CobaltFilePartition.Schema.ShreddedCobalt,
                PartitionId = FilePartitionId.CoauthMetadata
            };

            var wacupdate = new CobaltFilePartitionConfig
            {
                IsNewFile = true,
                HostBlobStore = new TemporaryHostBlobStore(new TemporaryHostBlobStore.Config(), disposal, accessTokenId + @".WordWacUpdate"),
                cellSchemaIsGenericFda = false,
                CellStorageConfig = new CellStorageConfig(),
                Schema = CobaltFilePartition.Schema.ShreddedCobalt,
                PartitionId = FilePartitionId.WordWacUpdate
            };

            var partitionConfs = new Dictionary<FilePartitionId, CobaltFilePartitionConfig>
            {
                {FilePartitionId.Content, content},
                {FilePartitionId.WordWacUpdate, wacupdate},
                {FilePartitionId.CoauthMetadata, coauth}
            };

            var cobaltFile = new CobaltFile(disposal, partitionConfs, new CobaltHostLockingStore(), null);

            var token = _tokenManager.GetToken(accessTokenId);

            Atom atom;

            if (_webDavManager.FileExist(token.FilePath))
            {
                var fileBytes = _webDavManager.GetFileBytes(token.FilePath);

                atom = new AtomFromByteArray(fileBytes);
            }
            else
            {
                var filePath = HttpContext.Current.Server.MapPath(WebDavAppConfigManager.Instance.OfficeOnline.NewFilePath + Path.GetExtension(token.FilePath));

                atom = new AtomFromByteArray(File.ReadAllBytes(filePath));
            }

            Cobalt.Metrics o1;
            cobaltFile.GetCobaltFilePartition(FilePartitionId.Content).SetStream(RootId.Default.Value, atom, out o1);
            cobaltFile.GetCobaltFilePartition(FilePartitionId.Content).GetStream(RootId.Default.Value).Flush();

            Add(token.FilePath, cobaltFile);

            return cobaltFile;
        }

        public CobaltFile Get(string filePath)
        {
            return _storage.Get<CobaltFile>(GetSessionKey(filePath)); 
        }

        public bool Add(string filePath, CobaltFile file)
        {
            return _storage.Add(GetSessionKey(filePath), file);
        }

        public bool Delete(string filePath)
        {
            return _storage.Delete(GetSessionKey(filePath));
        }

        private string GetSessionKey(string filePath)
        {
            return string.Format("{0}/{1}", WspContext.User.AccountId, filePath);
        }
    }
}
