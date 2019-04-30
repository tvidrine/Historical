// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IClientSettingsRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IClientSettings>>> GetAllAsync();
        Task<GetResponse<IClientSettings>> GetByIdAsync(int id);
        Task<SaveResponse<IClientSettings>> SaveAsync(IClientSettings clientSettings);
        Task<SaveResponse<IReadOnlyList<IClientSettings>>> SaveAllAsync(IReadOnlyList<IClientSettings> clientSettings);
    }
}
