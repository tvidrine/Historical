// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/7/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface ILocationRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<ILocation>>> GetAllAsync();
        Task<GetResponse<IReadOnlyList<ILocation>>> GetAllAsync(int entityId);

        Task<GetResponse<IReadOnlyList<ILocationInfo>>> GetInfoListAsync(int entityId);
        Task<GetResponse<ILocation>> GetByIdAsync(int id);
        Task<SaveResponse<ILocation>> SaveAsync(ILocation location);
        Task<SaveResponse<IReadOnlyList<ILocation>>> SaveAllAsync(IReadOnlyList<ILocation> location);
    }
}
