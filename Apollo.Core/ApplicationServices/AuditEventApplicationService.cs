// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Common;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class AuditEventApplicationService : IAuditEventApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditEventRepository _auditEventRepository;

        public AuditEventApplicationService(ILogManager logManager, IAuditEventRepository auditEventRepository)
        {
            _logManager = logManager;
            _auditEventRepository = auditEventRepository;
        }

        public async Task<ICreateResponse<IAuditEvent>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IAuditEvent>
            {
                Content = new AuditEvent(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _auditEventRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete auditEvent");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IAuditEvent>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAuditEvent>();
            try
            {
                getResponse = await _auditEventRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditEvent");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAuditEvent>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IAuditEvent>>();
            try
            {
                getResponse = await _auditEventRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditEvents");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IAuditEvent>> SaveAsync(IAuditEvent auditEvent)
        {
            var saveResponse = new SaveResponse<IAuditEvent>();
            try
            {
                saveResponse = await _auditEventRepository.SaveAsync(auditEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditEvent");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IAuditEvent>>> SaveAllAsync(IReadOnlyList<IAuditEvent> auditEvents)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IAuditEvent>>();
            try
            {
                saveResponse = await _auditEventRepository.SaveAllAsync(auditEvents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditEvents");
            }

            return saveResponse;
        }

        public Task<ValidationResult> ValidateAsync(IAuditEvent auditEvent)
        {
            throw new NotImplementedException();
        }
    }
}
