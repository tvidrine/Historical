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
    public interface IAuditTaskRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditTask>>> GetAllAsync();
        Task<GetResponse<IAuditTask>> GetByIdAsync(int id);
        Task<SaveResponse<IAuditTask>> SaveAsync(IAuditTask auditTask);
        Task<SaveResponse<IReadOnlyList<IAuditTask>>> SaveAllAsync(IReadOnlyList<IAuditTask> auditTask);
    }
}
