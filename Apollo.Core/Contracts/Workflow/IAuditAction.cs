// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Workflow
{
    public interface IAuditAction
    {
        string RuleSetName { get; set; }
        IToken Execute(IToken token);
        
    }
}