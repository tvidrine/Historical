// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IRemunerationSalesRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IRemunerationSales>>> GetAllAsync(RemunerationRequest request);
        Task<GetResponse<IRemunerationSales>> GetByIdAsync(int id);
        Task<SaveResponse<IRemunerationSales>> SaveAsync(IRemunerationSales remunerationSales);
        Task<SaveResponse<IReadOnlyList<IRemunerationSales>>> SaveAllAsync(IReadOnlyList<IRemunerationSales> remunerationSales);
    }
}
