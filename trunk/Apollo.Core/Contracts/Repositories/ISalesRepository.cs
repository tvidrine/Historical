// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface ISalesRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<DeleteResponse> DeleteAllAsync(int auditId, int entityId, int locationId);
        Task<GetResponse<IReadOnlyList<ISales>>> GetAllSalesAsync(int auditId, int entityId, SalesPeriodType periodType);
        Task<GetResponse<IReadOnlyList<ISalesVerification>>> GetAllSalesVerificationAsync(int auditId, int entityId, string state, int locationId, SalesPeriodType periodType);
        Task<GetResponse<ISales>> GetByIdAsync(int id);
        Task<SaveResponse<ISales>> SaveAsync(ISales sales);
        Task<SaveResponse<IReadOnlyList<ISales>>> SaveAllSalesAsync(IReadOnlyList<ISales> sales);
        Task<SaveResponse<IReadOnlyList<ISalesVerification>>> SaveAllSalesVerificationAsync(List<ISalesVerification> salesVerifications);
    }
}
