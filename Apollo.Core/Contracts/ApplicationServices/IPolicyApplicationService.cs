// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/22/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IPolicyApplicationService
    {
        Task<ICreateResponse<IPolicy>> CreateAsync(AuditRequest request);
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IPolicy>> GetAsync(int id);
        Task<GetResponse<IPolicy>> GetAsync(AuditRequest request);
        Task<GetResponse<IReadOnlyList<IPolicy>>> GetAllAsync();
        Task<SaveResponse<IPolicy>> SaveAsync(IPolicy policy);
        Task<ValidationResult> ValidateAsync(IPolicy policy);
        
    }
}
