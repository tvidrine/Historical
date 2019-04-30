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
    public class AuditStatusApplicationService : IAuditStatusApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditStatusRepository _auditStatusRepository;

        public AuditStatusApplicationService(ILogManager logManager, IAuditStatusRepository auditStatusRepository)
        {
            _logManager = logManager;
            _auditStatusRepository = auditStatusRepository;
        }

        public async Task<ICreateResponse<IAuditStatus>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IAuditStatus>
            {
                Content = new AuditStatus(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _auditStatusRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete auditStatus");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IAuditStatus>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAuditStatus>();
            try
            {
                getResponse = await _auditStatusRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditStatus");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAuditStatus>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IAuditStatus>>();
            try
            {
                getResponse = await _auditStatusRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditStatuses");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IAuditStatus>> SaveAsync(IAuditStatus auditStatus)
        {
            var saveResponse = new SaveResponse<IAuditStatus>();
            try
            {
                saveResponse = await _auditStatusRepository.SaveAsync(auditStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditStatus");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IAuditStatus>>> SaveAllAsync(IReadOnlyList<IAuditStatus> auditStatuses)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IAuditStatus>>();
            try
            {
                saveResponse = await _auditStatusRepository.SaveAllAsync(auditStatuses);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditStatuses");
            }

            return saveResponse;
        }

        public Task<ValidationResult> ValidateAsync(IAuditStatus auditStatus)
        {
            throw new NotImplementedException();
        }
    }
}
