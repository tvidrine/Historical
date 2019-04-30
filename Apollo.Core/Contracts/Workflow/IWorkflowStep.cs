// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;

namespace Apollo.Core.Contracts.Workflow
{
    public interface IAuditWorkflowStep : IHaveId, IHaveAuditData
    {
        string Key { get; set; }
        string RuleSetKey { get; set; }
        string OnFailureStepKey { get; set; }
        string OnSuccessStepKey { get; set; }
        string AggregateAction { get; set; }
        int WorkflowId { get; set; }
        bool HasAggregateAction { get; }
    }
}