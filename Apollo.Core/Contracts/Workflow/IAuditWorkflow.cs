// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain;

namespace Apollo.Core.Contracts.Workflow
{
    public interface IAuditWorkflow : IHaveId, IHaveAuditData
    {
        string Name { get; set; }
        string RootStepKey { get; set; }

        IDictionary<string,IAuditWorkflowStep> Steps { get; set; }
    }
}