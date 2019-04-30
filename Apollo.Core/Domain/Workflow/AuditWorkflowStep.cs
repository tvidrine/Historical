// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Workflow
{
    public class AuditWorkflowStep : ModelBase, IAuditWorkflowStep
    {
        public string Key { get; set; }
        public string RuleSetKey { get; set; }
        public string OnFailureStepKey { get; set; }
        public string OnSuccessStepKey { get; set; }
        public string AggregateAction { get; set; }
        public int WorkflowId { get; set; }
        public bool HasAggregateAction => string.IsNullOrEmpty(AggregateAction);
    }
}