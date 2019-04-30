// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IAuditStatusApplicationService
    {
        Task<ICreateResponse<IAuditStatus>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IAuditStatus>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditStatus>>> GetAllAsync();
        Task<SaveResponse<IAuditStatus>> SaveAsync(IAuditStatus auditStatus);
        Task<SaveResponse<IReadOnlyList<IAuditStatus>>> SaveAllAsync(IReadOnlyList<IAuditStatus> auditStatuses);
        Task<ValidationResult> ValidateAsync(IAuditStatus auditStatus);
    }
}
