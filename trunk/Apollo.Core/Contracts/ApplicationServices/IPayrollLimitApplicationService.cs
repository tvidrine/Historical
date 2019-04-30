// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IPayrollLimitApplicationService
    {
        Task<ICreateResponse<IPayrollLimit>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IPayrollLimit>> GetAsync(int id);
        Task<GetResponse<IPayrollLimit>> GetAsync(PayrollLimitRequest request);
        Task<GetResponse<IReadOnlyList<IPayrollLimit>>> GetAllAsync(PayrollLimitRequest request);
        Task<SaveResponse<IPayrollLimit>> SaveAsync(IPayrollLimit payrollLimit);
        Task<SaveResponse<IReadOnlyList<IPayrollLimit>>> SaveAllAsync(IReadOnlyList<IPayrollLimit> payrollLimits);
        Task<ValidationResult> ValidateAsync(IPayrollLimit payrollLimit);
    }
}
