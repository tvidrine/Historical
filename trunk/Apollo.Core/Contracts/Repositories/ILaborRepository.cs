// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface ILaborRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<ILabor>>> GetAllAsync(int auditId, int entityId);
        Task<GetResponse<ILabor>> GetByIdAsync(int id);
        Task<SaveResponse<ILabor>> SaveAsync(ILabor labor);
        Task<SaveResponse<IReadOnlyList<ILabor>>> SaveAllAsync(IReadOnlyList<ILabor> labor);
    }
}
