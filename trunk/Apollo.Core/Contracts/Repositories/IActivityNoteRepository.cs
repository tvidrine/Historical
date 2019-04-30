// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IActivityNoteRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IActivityNote>>> GetAllAsync();
        Task<GetResponse<IActivityNote>> GetByIdAsync(int id);
        Task<SaveResponse<IActivityNote>> SaveAsync(IActivityNote activityNote);
        Task<SaveResponse<IReadOnlyList<IActivityNote>>> SaveAllAsync(IReadOnlyList<IActivityNote> activityNote);
    }
}
