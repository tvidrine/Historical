// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAuditWorkflowRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditWorkflow>>> GetAllAsync();
        Task<GetResponse<IAuditWorkflow>> GetByIdAsync(int id);
        Task<GetResponse<IAuditWorkflow>> GetWorkflowByKeyAsync(string workflowKey);
        Task<SaveResponse<IAuditWorkflow>> SaveAsync(IAuditWorkflow auditWorkflow);
        Task<SaveResponse<IReadOnlyList<IAuditWorkflow>>> SaveAllAsync(IReadOnlyList<IAuditWorkflow> auditWorkflow);
    }
}