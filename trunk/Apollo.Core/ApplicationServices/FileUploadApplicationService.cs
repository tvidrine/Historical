// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class FileUploadApplicationService : IFileUploadApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditUploadRepository _auditUploadRepository;

        public FileUploadApplicationService(ILogManager logManager, IAuditUploadRepository auditUploadRepository)
        {
            _logManager = logManager;
            _auditUploadRepository = auditUploadRepository;
        }

        public async Task<ICreateResponse<IAuditUpload>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IAuditUpload>
            {
                Content = new AuditUpload(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _auditUploadRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete auditUpload");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IAuditUpload>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAuditUpload>();
            try
            {
                getResponse = await _auditUploadRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditUpload");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAuditUpload>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IAuditUpload>>();
            try
            {
                getResponse = await _auditUploadRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditUploads");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IAuditUpload>> SaveAsync(IAuditUpload auditUpload)
        {
            var saveResponse = new SaveResponse<IAuditUpload>();
            try
            {
                saveResponse = await _auditUploadRepository.SaveAsync(auditUpload);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditUpload");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IAuditUpload>>> SaveAllAsync(IReadOnlyList<IAuditUpload> auditUploads)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IAuditUpload>>();
            try
            {
                saveResponse = await _auditUploadRepository.SaveAllAsync(auditUploads);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditUploads");
            }

            return saveResponse;
        }

        public Task<ValidationResult> ValidateAsync(IAuditUpload auditUpload)
        {
            throw new NotImplementedException();
        }
    }
}
