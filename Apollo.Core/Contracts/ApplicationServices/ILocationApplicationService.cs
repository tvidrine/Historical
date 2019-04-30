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
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface ILocationApplicationService
    {
        Task<ICreateResponse<ILocation>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<ILocation>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<ILocation>>> GetAllAsync(int entityId);
        Task<GetResponse<IReadOnlyList<ILocationInfo>>> GetInfoListAsync(int entityId);
        Task<SaveResponse<ILocation>> SaveAsync(ILocation location);
        Task<SaveResponse<IReadOnlyList<ILocation>>> SaveAllAsync(IReadOnlyList<ILocation> locations);
        Task<ValidationResult> ValidateAsync(ILocation location);
    }
}
