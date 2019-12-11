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
using System.IO;
using System.Threading;
using Cobalt;
using WebsitePanel.WebDav.Core.Interfaces.Managers;
using WebsitePanel.WebDav.Core.Interfaces.Owa;

namespace WebsitePanel.WebDav.Core.Owa
{
    public class CobaltManager : ICobaltManager
    {
        private readonly IWebDavManager _webDavManager;
        private readonly IWopiFileManager _fileManager;
        private readonly IAccessTokenManager _tokenManager;

        public CobaltManager(IWebDavManager webDavManager, IWopiFileManager fileManager,
            IAccessTokenManager tokenManager)
        {
            _webDavManager = webDavManager;
            _fileManager = fileManager;
            _tokenManager = tokenManager;
        }

        public Atom ProcessRequest(int accessTokenId, Stream requestStream)
        {
            var token = _tokenManager.GetToken(accessTokenId);

            var atomRequest = new AtomFromStream(requestStream);

            var requestBatch = new RequestBatch();

            try
            {
                var cobaltFile = _fileManager.Get(token.FilePath) ?? _fileManager.Create(accessTokenId);

                Object ctx;
                ProtocolVersion protocolVersion;

                requestBatch.DeserializeInputFromProtocol(atomRequest, out ctx, out protocolVersion);
                cobaltFile.CobaltEndpoint.ExecuteRequestBatch(requestBatch);


                foreach (var request in requestBatch.Requests)
                {

                    if (request.GetType() == typeof (PutChangesRequest) &&
                        request.PartitionId == FilePartitionId.Content && request.CompletedSuccessfully)
                    {
                        using (var saveStream = new MemoryStream())
                        {
                            CopyStream(cobaltFile, saveStream);
                            _webDavManager.UploadFile(token.FilePath, saveStream.ToArray());
                        }
                    }
                }


                return requestBatch.SerializeOutputToProtocol(protocolVersion);
            }

            catch (Exception e)
            {
                Server.Utils.Log.WriteError("Cobalt manager Process request", e);

                throw;
            }
        }

        private void CopyStream(CobaltFile file, Stream stream)
        {
            var tries = 3;

            for (int i = 0; i < tries; i++)
            {
                try
                {
                    GenericFdaStream myCobaltStream = new GenericFda(file.CobaltEndpoint, null).GetContentStream();

                    myCobaltStream.CopyTo(stream);

                    break;
                }
                catch (Exception)
                {
                    //unable to read update - save failed
                    if (i == tries - 1)
                    {
                        throw;
                    }

                    //waiting for cobalt completion
                    Thread.Sleep(50);
                }
            }
        }
    }
} 
