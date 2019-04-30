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
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IActivityNoteApplicationService
    {
        Task<ICreateResponse<IActivityNote>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IActivityNote>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IActivityNote>>> GetAllAsync();
        Task<SaveResponse<IActivityNote>> SaveAsync(IActivityNote activityNote);
        Task<SaveResponse<IReadOnlyList<IActivityNote>>> SaveAllAsync(IReadOnlyList<IActivityNote> activityNotes);
        Task<ValidationResult> ValidateAsync(IActivityNote activityNote);
    }
}
