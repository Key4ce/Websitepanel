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
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using WebsitePanel.Providers.HostedSolution;

namespace WebsitePanel.Providers.Virtualization
{
    public class PowerShellManager : IDisposable
    {
        private readonly string _remoteComputerName;
        protected static InitialSessionState session = null;

        protected Runspace RunSpace { get; set; }

        public PowerShellManager(string remoteComputerName)
        {
            _remoteComputerName = remoteComputerName;
            OpenRunspace();
        }

        protected void OpenRunspace()
        {
            HostedSolutionLog.LogStart("OpenRunspace");

            if (session == null)
            {
                session = InitialSessionState.CreateDefault();
                session.ImportPSModule(new[] {"Hyper-V"});
            }

            Runspace runSpace = RunspaceFactory.CreateRunspace(session);
            runSpace.Open();
            runSpace.SessionStateProxy.SetVariable("ConfirmPreference", "none");

            RunSpace = runSpace;
   
            HostedSolutionLog.LogEnd("OpenRunspace");
        }

        public void Dispose()
        {
            try
            {
                if (RunSpace != null && RunSpace.RunspaceStateInfo.State == RunspaceState.Opened)
                {
                    RunSpace.Close();
                    RunSpace = null;
                }
            }
            catch (Exception ex)
            {
                HostedSolutionLog.LogError("Runspace error", ex);
            }
        }

        public Collection<PSObject> Execute(Command cmd)
        {
            return Execute(cmd, true);
        }

        public Collection<PSObject> Execute(Command cmd, bool addComputerNameParameter)
        {
            return Execute(cmd, addComputerNameParameter, false);
        }

        public Collection<PSObject> Execute(Command cmd, bool addComputerNameParameter, bool withExceptions)
        {
            HostedSolutionLog.LogStart("Execute");

            List<object> errorList = new List<object>();

            HostedSolutionLog.DebugCommand(cmd);
            Collection<PSObject> results = null;

            // Add computerName parameter to command if it is remote server
            if (addComputerNameParameter)
            {
                if (!string.IsNullOrEmpty(_remoteComputerName))
                    cmd.Parameters.Add("ComputerName", _remoteComputerName);
            }

            // Create a pipeline
            Pipeline pipeLine = RunSpace.CreatePipeline();
            using (pipeLine)
            {
                // Add the command
                pipeLine.Commands.Add(cmd);
                // Execute the pipeline and save the objects returned.
                results = pipeLine.Invoke();

                // Only non-terminating errors are delivered here.
                // Terminating errors raise exceptions instead.
                // Log out any errors in the pipeline execution
                // NOTE: These errors are NOT thrown as exceptions! 
                // Be sure to check this to ensure that no errors 
                // happened while executing the command.
                if (pipeLine.Error != null && pipeLine.Error.Count > 0)
                {
                    foreach (object item in pipeLine.Error.ReadToEnd())
                    {
                        errorList.Add(item);
                        string errorMessage = string.Format("Invoke error: {0}", item);
                        HostedSolutionLog.LogWarning(errorMessage);
                    }
                }
            }
            pipeLine = null;

            if (withExceptions)
                ExceptionIfErrors(errorList);

            HostedSolutionLog.LogEnd("Execute");
            return results;
        }

        private static void ExceptionIfErrors(List<object> errors)
        {
            if (errors != null && errors.Count > 0)
                throw new Exception("Invoke error: " + string.Join("; ", errors.Select(e => e.ToString())));
        }
    }
}
