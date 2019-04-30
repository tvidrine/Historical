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
    public interface IAuditTaskApplicationService
    {
        Task<ICreateResponse<IAuditTask>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IAuditTask>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditTask>>> GetAllAsync();
        Task<SaveResponse<IAuditTask>> SaveAsync(IAuditTask auditTask);
        Task<SaveResponse<IReadOnlyList<IAuditTask>>> SaveAllAsync(IReadOnlyList<IAuditTask> auditTasks);
        Task<ValidationResult> ValidateAsync(IAuditTask auditTask);
    }
}
