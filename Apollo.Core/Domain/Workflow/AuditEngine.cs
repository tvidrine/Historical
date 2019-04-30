// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Services;
using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Domain.Workflow
{
    public class AuditEngine : IAuditEngine
    {
        private readonly IAuditActionFactory _auditActionFactory;
        private readonly IAuditWorkflowService _workflowService;
        private readonly ILogManager _logManager;
        private readonly IAuditApplicationService _auditApplicationService;
        private IDictionary<string, IAuditWorkflowStep> _stepCache;
        private readonly IDictionary<int, IClient> _clientCache;

        public AuditEngine(
            IAuditApplicationService auditApplicationService,
            IClientApplicationService clientApplicationService,
            IAuditActionFactory auditStepFactory, 
            IAuditWorkflowService workflowService, 
            ILogManager logManager)
        {
            _auditApplicationService = auditApplicationService;
            _auditActionFactory = auditStepFactory;
            _workflowService = workflowService;
            _logManager = logManager;
            _stepCache = new Dictionary<string, IAuditWorkflowStep>();
            _clientCache = GetClientCache(clientApplicationService);
        }

        public void Begin(string workflowKey)
        {
            try
            {
                // 1. Load the workflow
                var workflowResponse = _workflowService.GetWorkflowByKeyAsync(workflowKey).Result;

                if (workflowResponse.IsSuccessful)
                {
                    // 2. Execute the workflow
                    var workflow = workflowResponse.Content;
                    ExecuteWorkflowAsync(workflow).GetAwaiter().GetResult();
                }

            }
            catch (Exception ex)
            {
                _logManager.LogError(ex, "AuditEngine.BeginAsync");
            }
        }
        public async Task BeginAsync(string workflowKey)
        {
            try
            {
                // 1. Load the workflow
                var workflowResponse = await _workflowService.GetWorkflowByKeyAsync(workflowKey);

                if (workflowResponse.IsSuccessful)
                {
                    // 2. Execute the workflow
                    var workflow = workflowResponse.Content;
                    await ExecuteWorkflowAsync(workflow);
                }
                
            }
            catch (Exception ex)
            {
                _logManager.LogError(ex, "AuditEngine.BeginAsync");
            }
            
        }

        private async Task ExecuteWorkflowAsync(IAuditWorkflow workflow)
        {
            try
            {
                
                // Get all audits that are not complete or canceled.
                var auditsToProcessResponse = await _auditApplicationService.GetAllAsync(true);
                _stepCache = workflow.Steps;

                if (auditsToProcessResponse.IsSuccessful)
                {
                    var auditsToProcess = auditsToProcessResponse.Content;
                    foreach (var audit in auditsToProcess)
                    {
                        var token = new StepToken
                        {
                            Audit = audit,
                            Client = _clientCache[audit.Policy.Client.Id]
                        };
                        var rootStep = workflow.RootStepKey;
                        ExecuteStep(rootStep, token);
                    }
                    
                }

                
            }
            catch (Exception ex)
            {
                _logManager.LogError(ex, "AuditEngine.BeginWorkflow");
            }
        }

        private void ExecuteStep(string currentStepKey, IToken token)
        {
            while (!string.IsNullOrEmpty(currentStepKey))
            {
                var currentStep = _stepCache[currentStepKey];

                // Execute the current step
                token = _auditActionFactory.Get(currentStepKey)
                    .Execute(token);
                
                currentStepKey = token.IsSuccessful ? currentStep.OnSuccessStepKey : currentStep.OnFailureStepKey;
            }
        }

        private void ExecuteAggregateAction(IAuditWorkflowStep currentStep, IToken token)
        {
            throw new NotImplementedException();
        }

        private IDictionary<int, IClient> GetClientCache(IClientApplicationService clientApplicationService)
        {
            var result = clientApplicationService.GetAllAsync().Result;

            if (result.IsSuccessful)
                return result.Content.ToDictionary(x => x.Id, x => x);

            throw new Exception(result.Message);
        }
    }
}