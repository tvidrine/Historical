// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Workflow
{
    public class AuditWorkflow : ModelBase, IAuditWorkflow
    {
        public AuditWorkflow()
        {
            Steps = new Dictionary<string, IAuditWorkflowStep>();
        }

        public string RootStepKey { get; set; }
        public IDictionary<string, IAuditWorkflowStep> Steps { get; set; }
        public string Name { get; set; }
        
    }
}