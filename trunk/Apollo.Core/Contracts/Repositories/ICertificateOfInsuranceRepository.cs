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

namespace Apollo.Core.Contracts.Repositories
{
    public interface ICertificateOfInsuranceRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<ICertificateOfInsurance>> GetForLaborAsync(int laborId);
        Task<GetResponse<ICertificateOfInsurance>> GetByIdAsync(int id);
        Task<SaveResponse<ICertificateOfInsurance>> SaveAsync(ICertificateOfInsurance certificateOfInsurance);
        Task<SaveResponse<IReadOnlyList<ICertificateOfInsurance>>> SaveAllAsync(IReadOnlyList<ICertificateOfInsurance> certificateOfInsurance);
    }
}
