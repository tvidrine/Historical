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
    public interface IAuditEventApplicationService
    {
        Task<ICreateResponse<IAuditEvent>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IAuditEvent>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditEvent>>> GetAllAsync();
        Task<SaveResponse<IAuditEvent>> SaveAsync(IAuditEvent auditEvent);
        Task<SaveResponse<IReadOnlyList<IAuditEvent>>> SaveAllAsync(IReadOnlyList<IAuditEvent> auditEvents);
        Task<ValidationResult> ValidateAsync(IAuditEvent auditEvent);
    }
}
