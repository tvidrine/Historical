// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IFileUploadApplicationService
    {
        Task<ICreateResponse<IAuditUpload>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IAuditUpload>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditUpload>>> GetAllAsync();
        Task<SaveResponse<IAuditUpload>> SaveAsync(IAuditUpload auditUpload);
        Task<SaveResponse<IReadOnlyList<IAuditUpload>>> SaveAllAsync(IReadOnlyList<IAuditUpload> auditUploads);
        Task<ValidationResult> ValidateAsync(IAuditUpload auditUpload);
    }
}
