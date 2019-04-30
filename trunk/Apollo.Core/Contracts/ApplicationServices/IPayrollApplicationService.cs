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
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IPayrollApplicationService
    {
        Task<ICreateResponse<IPayroll>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IPayroll>> GetPayrollAsync(int id);
        Task<GetResponse<IReadOnlyList<IPayroll>>> GetAllPayrollAsync(int entityId);
        Task<GetResponse<IReadOnlyList<IPayrollClassification>>> GetAllPayrollClassificationsAsync(IAudit audit);
        Task<SaveResponse<IPayroll>> SavePayrollAsync(IPayroll payroll);
        Task<SaveResponse<IReadOnlyList<IPayroll>>> SaveAllPayrollAsync(IReadOnlyList<IPayroll> payrolls);

        Task<SaveResponse<IReadOnlyList<IPayrollClassification>>> SaveAllPayrollClassification(IReadOnlyList<IPayrollClassification> classifications);
        Task<ValidationResult> ValidateAsync(IPayroll payroll);
    }
}
