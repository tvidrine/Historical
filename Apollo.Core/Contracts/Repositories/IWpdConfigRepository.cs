// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IWpdConfigRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IWpdConfig>> GetByIdAsync(int clientId);
        Task<SaveResponse<IWpdConfig>> SaveAsync(IWpdConfig wpdConfig);
    }
}
