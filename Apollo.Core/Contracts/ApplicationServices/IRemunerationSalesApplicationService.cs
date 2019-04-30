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
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IRemunerationSalesApplicationService
    {
        Task<ICreateResponse<IRemunerationSales>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IRemunerationSales>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IRemunerationSales>>> GetAllAsync(RemunerationRequest request);
        Task<SaveResponse<IRemunerationSales>> SaveAsync(IRemunerationSales remunerationSales);
        Task<SaveResponse<IReadOnlyList<IRemunerationSales>>> SaveAllAsync(IReadOnlyList<IRemunerationSales> remunerationSales);
        Task<ValidationResult> ValidateAsync(IRemunerationSales remunerationSales);
    }
}
