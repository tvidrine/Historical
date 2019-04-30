// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 3/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IPayrollRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IPayroll>>> GetAllPayrollAsync(int entityId);
        Task<GetResponse<IPayroll>> GetPayrollByIdAsync(int id);
        Task<SaveResponse<IPayroll>> SavePayrollAsync(IPayroll payroll);
        Task<SaveResponse<IReadOnlyList<IPayroll>>> SaveAllPayrollAsync(IReadOnlyList<IPayroll> payroll);
        Task<GetResponse<IReadOnlyList<IPayrollClassification>>> GetAllPayrollClassificationsAsync(IAudit audit);
        Task<SaveResponse<IReadOnlyList<IPayrollClassification>>> SaveAllPayrollClassification(IReadOnlyList<IPayrollClassification> classifications);
    }
}
