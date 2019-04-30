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
    public interface IContactRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IContact>>> GetAllAsync(int entityId);
        Task<GetResponse<IContact>> GetByIdAsync(int id);
        Task<SaveResponse<IContact>> SaveAsync(IContact contact);
        Task<SaveResponse<IReadOnlyList<IContact>>> SaveAllAsync(IReadOnlyList<IContact> clients);
    }
}
