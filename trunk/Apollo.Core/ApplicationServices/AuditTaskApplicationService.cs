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
    public class AuditTaskApplicationService : IAuditTaskApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditTaskRepository _auditTaskRepository;

        public AuditTaskApplicationService(ILogManager logManager, IAuditTaskRepository auditTaskRepository)
        {
            _logManager = logManager;
            _auditTaskRepository = auditTaskRepository;
        }

        public async Task<ICreateResponse<IAuditTask>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IAuditTask>
            {
                Content = new AuditTask(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _auditTaskRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete auditTask");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IAuditTask>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAuditTask>();
            try
            {
                getResponse = await _auditTaskRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditTask");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAuditTask>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IAuditTask>>();
            try
            {
                getResponse = await _auditTaskRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditTasks");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IAuditTask>> SaveAsync(IAuditTask auditTask)
        {
            var saveResponse = new SaveResponse<IAuditTask>();
            try
            {
                saveResponse = await _auditTaskRepository.SaveAsync(auditTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditTask");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IAuditTask>>> SaveAllAsync(IReadOnlyList<IAuditTask> auditTasks)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IAuditTask>>();
            try
            {
                saveResponse = await _auditTaskRepository.SaveAllAsync(auditTasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditTasks");
            }

            return saveResponse;
        }

        public Task<ValidationResult> ValidateAsync(IAuditTask auditTask)
        {
            throw new NotImplementedException();
        }
    }
}
