// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Base;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Domain.Workflow.Steps
{
    [AuditActionKey("LogMessage")]
    public class LogMessageStep : IAuditAction
    {
        private readonly ILogManager _logManager;

        public LogMessageStep(ILogManager logManager)
        {
            _logManager = logManager;
        }

        public string RuleSetName { get; set; }

        public IToken Execute(IToken token)
        {
            return LogMessage(token.Message);
        }

        private StepToken LogMessage(string message)
        {
            var result = new StepToken();

            try
            {
                _logManager.LogInfo(message);
            }
            catch (Exception e)
            {
                result.SetException(e);
                _logManager.LogError(e, "LogMessage.LogMessage");
            }
            return result;

        }
    }
}