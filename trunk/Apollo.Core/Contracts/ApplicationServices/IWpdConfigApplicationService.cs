// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IWpdConfigApplicationService
    {
        Task<ICreateResponse<IWpdConfig>> CreateAsync(int clientId);
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IWpdConfig>> GetAsync(int clientId);
        Task<SaveResponse<IWpdConfig>> SaveAsync(IWpdConfig wpdConfig);
    }
}
