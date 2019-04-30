// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IPayrollLimitRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IPayrollLimit>> GetAsync(PayrollLimitRequest request);
        Task<GetResponse<IReadOnlyList<IPayrollLimit>>> GetAllAsync(PayrollLimitRequest request);
        Task<GetResponse<IPayrollLimit>> GetByIdAsync(int id);
        Task<SaveResponse<IPayrollLimit>> SaveAsync(IPayrollLimit payrollLimit);
        Task<SaveResponse<IReadOnlyList<IPayrollLimit>>> SaveAllAsync(IReadOnlyList<IPayrollLimit> payrollLimit);

    }
}
