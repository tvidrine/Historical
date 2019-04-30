// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAddressRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAddress>>> GetAllAsync(int entityId);
        Task<GetResponse<IAddress>> GetByIdAsync(int id);
        Task<SaveResponse<IAddress>> SaveAsync(IAddress address);
        Task<SaveResponse<IReadOnlyList<IAddress>>> SaveAllAsync(IReadOnlyList<IAddress> modelList);
    }
}
