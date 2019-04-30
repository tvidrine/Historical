// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface ICertificateOfInsuranceApplicationService
    {
        Task<ICreateResponse<ICertificateOfInsurance>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<ICertificateOfInsurance>> GetAsync(int id);
        Task<GetResponse<ICertificateOfInsurance>> GetForLaborAsync(int laborId);
        Task<SaveResponse<ICertificateOfInsurance>> SaveAsync(ICertificateOfInsurance certificateOfInsurance);
        Task<SaveResponse<IReadOnlyList<ICertificateOfInsurance>>> SaveAllAsync(IReadOnlyList<ICertificateOfInsurance> certificateOfInsurances);
        Task<ValidationResult> ValidateAsync(ICertificateOfInsurance certificateOfInsurance);
    }
}
