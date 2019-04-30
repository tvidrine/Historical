// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/03/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Base;
using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Domain.Workflow.Steps
{
    [AuditActionKey("SaveBatchFile")]
    public class SaveBatchFileStep : IAuditAction
    {
        public string RuleSetName { get; set; }
        public IToken Execute(IToken token)
        {
            // Need a file name
            // Create the file if it doesn't exists


            return token;
        }
    }
}