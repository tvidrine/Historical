// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Contracts.Services;
using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class WorkflowService : IAuditWorkflowService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditWorkflowRepository _workflowRepository;

        public WorkflowService(IAuditWorkflowRepository workflowRepository, ILogManager logManager)
        {
            _logManager = logManager;
            _workflowRepository = workflowRepository;
        }
        public async Task<GetResponse<IAuditWorkflow>> GetWorkflowByKeyAsync(string workflowKey)
        {
            var response = new GetResponse<IAuditWorkflow>();
            try
            {
                response =  await _workflowRepository.GetWorkflowByKeyAsync(workflowKey);
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "WorkflowService.GetWorkflowByKeyAsync");
                response.AddError(e);
            }

            return response;
        }
    }
}