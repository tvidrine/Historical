// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface ILaborApplicationService
    {
        Task<ICreateResponse<ILabor>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<ILabor>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<ILabor>>> GetAllAsync(int auditId, int entityId);
        Task<SaveResponse<ILabor>> SaveAsync(ILabor labor);
        Task<SaveResponse<IReadOnlyList<ILabor>>> SaveAllAsync(IReadOnlyList<ILabor> labors);
        Task<ValidationResult> ValidateAsync(ILabor labor);
    }
}
