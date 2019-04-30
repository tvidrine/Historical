// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IRemunerationPayrollApplicationService
    {
        Task<ICreateResponse<IRemunerationPayroll>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IRemunerationPayroll>> GetAsync(int id);
        Task<GetResponse<IRemunerationPayroll>> GetAsync(RemunerationRequest request);
        Task<GetResponse<IReadOnlyList<IRemunerationPayroll>>> GetAllAsync(RemunerationRequest request);
        Task<SaveResponse<IRemunerationPayroll>> SaveAsync(IRemunerationPayroll remuneration);
        Task<SaveResponse<IReadOnlyList<IRemunerationPayroll>>> SaveAllAsync(IReadOnlyList<IRemunerationPayroll> remunerations);
        Task<ValidationResult> ValidateAsync(IRemunerationPayroll remuneration);
    }
}
