// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class AuditApplicationService : IAuditApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditRepository _auditRepository;
        private readonly IPolicyApplicationService _policyApplicationService;
        private readonly IClassCodeApplicationService _classCodeApplicationService;

        public AuditApplicationService(ILogManager logManager, 
            IAuditRepository auditRepository, 
            IPolicyApplicationService policyApplicationService,
            IClassCodeApplicationService classCodeApplicationService)
        {
            _logManager = logManager;
            _auditRepository = auditRepository;
            _policyApplicationService = policyApplicationService;
            _classCodeApplicationService = classCodeApplicationService;
        }

        public async Task<ICreateResponse<IAudit>> CreateAsync(AuditRequest request)
        {
            var response = new CreateResponse<Audit>();
            try
            {
                var audit = new Audit();
                var policyCreateResponse = await _policyApplicationService.CreateAsync(request);

                response.Join<CreateResponse<Audit>>(policyCreateResponse);

                if (policyCreateResponse.IsSuccessful)
                {
                    audit.Policy = (Policy) policyCreateResponse.Content;
                }

            }
            catch (Exception ex)
            {
                _logManager.LogError(ex, "AuditApplicationService.CreateAsync");
                response.AddError(ex);
            }

            return response;
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _auditRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete audit");
            }

            return deleteResponse;
        }

        public GetResponse<IAudit> Get(int id)
        {
            return GetAsync(id).Result;
        }
        public async Task<GetResponse<IAudit>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAudit>();
            try
            {
                getResponse = await _auditRepository.GetByIdAsync(id);

                // Get the standard exceptions for the audit
                if (getResponse.IsSuccessful)
                {
                    var audit = getResponse.Content;
                    var standardExceptionsResponse = await _classCodeApplicationService.GetStandardExceptionsForAudit(audit);
                    audit.StandardExceptions = standardExceptionsResponse.Content;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving audit");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAudit>>> GetAllAsync(bool activeOnly = false)
        {
            var getResponse = new GetResponse<IReadOnlyList<IAudit>>();
            try
            {
                getResponse = await _auditRepository.GetAllAsync(activeOnly);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving audits");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IAudit>> SaveAsync(IAudit audit)
        {
            var saveResponse = new SaveResponse<IAudit>();
            try
            {
                saveResponse = await _auditRepository.SaveAsync(audit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving audit");
            }

            return saveResponse;
        }

        public Task<SaveResponse<IReadOnlyList<IAudit>>> SaveAllAsync(IReadOnlyList<IAudit> audits)
        {
            throw new NotImplementedException();
        }

        
        public Task SendWorkProductDeliveryDocumentAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(IAudit audit)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResponse> UpdateAuditStatus(int auditId, AuditStatuses status, int userId)
        {
            var response = new SaveResponse();

            try
            {
                response = await _auditRepository.UpdateAuditStatus(auditId, status, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response.AddError(ex);
                _logManager.LogError(ex, "Error updating audit status");
            }
            return response;
        }

        public async Task<SaveResponse> NotifyPolicyHolder(int auditId, int notificationType, DateTime sentDate)
        {
            var response = new SaveResponse();

            try
            {
                response = await _auditRepository.NotifyPolicyHolder(auditId, notificationType, sentDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response.AddError(ex);
                _logManager.LogError(ex, "Error updating audit status");
            }
            return response;
        }
    }
}
