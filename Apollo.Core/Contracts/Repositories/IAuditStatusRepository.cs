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
    public interface IAuditStatusRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditStatus>>> GetAllAsync();
        Task<GetResponse<IAuditStatus>> GetByIdAsync(int id);
        Task<SaveResponse<IAuditStatus>> SaveAsync(IAuditStatus auditStatus);
        Task<SaveResponse<IReadOnlyList<IAuditStatus>>> SaveAllAsync(IReadOnlyList<IAuditStatus> auditStatus);
    }
}
