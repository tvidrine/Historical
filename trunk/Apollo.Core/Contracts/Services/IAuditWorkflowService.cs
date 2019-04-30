// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Services
{
    public interface IAuditWorkflowService
    {
        Task<GetResponse<IAuditWorkflow>> GetWorkflowByKeyAsync(string workflowKey);
    }
}