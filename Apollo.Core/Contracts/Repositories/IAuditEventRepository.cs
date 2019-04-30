// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAuditEventRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditEvent>>> GetAllAsync();
        Task<GetResponse<IAuditEvent>> GetByIdAsync(int id);
        Task<SaveResponse<IAuditEvent>> SaveAsync(IAuditEvent auditEvent);
        Task<SaveResponse<IReadOnlyList<IAuditEvent>>> SaveAllAsync(IReadOnlyList<IAuditEvent> auditEvent);
    }
}
