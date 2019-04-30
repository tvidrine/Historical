// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IClientSettingRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IClientSetting>>> GetAllAsync(int clientId);
        Task<GetResponse<IClientSetting>> GetByIdAsync(int id);
        Task<SaveResponse<IClientSetting>> SaveAsync(IClientSetting clientSetting);
        Task<SaveResponse<IReadOnlyList<IClientSetting>>> SaveAllAsync(IReadOnlyList<IClientSetting> clientSetting);
    }
}
