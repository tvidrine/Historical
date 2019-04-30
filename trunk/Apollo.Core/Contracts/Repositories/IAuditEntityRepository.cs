// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/4/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAuditEntityRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditEntity>>> GetAllAsync(int auditId);
        Task<GetResponse<IAuditEntity>> GetByIdAsync(int id);
        Task<SaveResponse<IAuditEntity>> SaveAsync(IAuditEntity auditEntity);
        Task<SaveResponse<IReadOnlyList<IAuditEntity>>> SaveAllAsync(IReadOnlyList<IAuditEntity> auditEntity);
    }
}
