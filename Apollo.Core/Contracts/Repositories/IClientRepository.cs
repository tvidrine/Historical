// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/6/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IClientRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync();
        Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync(ClientSettingsEnum setting, object value);
        Task<GetResponse<IReadOnlyList<IClient>>> GetAllAsync();
        Task<GetResponse<IClient>> GetByIdAsync(int id);
        Task<SaveResponse<IClient>> SaveAsync(IClient client);
        Task<SaveResponse<IReadOnlyList<IClient>>> SaveAllAsync(IReadOnlyList<IClient> clients);
        Task<GetResponse<IReadOnlyList<ClientConfiguration>>> GetConfigurationsAsync();
    }
}