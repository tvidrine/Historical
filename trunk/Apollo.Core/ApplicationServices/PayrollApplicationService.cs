// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 3/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Payroll;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class PayrollApplicationService : IPayrollApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IPayrollRepository _payrollRepository;

        public PayrollApplicationService(ILogManager logManager, IPayrollRepository payrollRepository)
        {
            _logManager = logManager;
            _payrollRepository = payrollRepository;
        }

        public async Task<ICreateResponse<IPayroll>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IPayroll>
            {
                Content = new Payroll(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _payrollRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete payroll");
            }

            return deleteResponse;
        }

        public Task<GetResponse<IPayroll>> GetPayrollAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetResponse<IReadOnlyList<IPayroll>>> GetAllPayrollAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetResponse<IReadOnlyList<IPayrollClassification>>> GetAllPayrollClassificationsAsync(IAudit audit)
        {
            var getResponse = new GetResponse<IReadOnlyList<IPayrollClassification>>();
            try
            {
                getResponse = await _payrollRepository.GetAllPayrollClassificationsAsync(audit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving payroll");
            }

            return getResponse;
        }

        public Task<SaveResponse<IPayroll>> SavePayrollAsync(IPayroll payroll)
        {
            throw new NotImplementedException();
        }

        public Task<SaveResponse<IReadOnlyList<IPayroll>>> SaveAllPayrollAsync(IReadOnlyList<IPayroll> payrolls)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResponse<IReadOnlyList<IPayrollClassification>>> SaveAllPayrollClassification(IReadOnlyList<IPayrollClassification> classifications)
        {
            var saveReponse = new SaveResponse<IReadOnlyList<IPayrollClassification>>();

            try
            {
                saveReponse = await _payrollRepository.SaveAllPayrollClassification(classifications);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveReponse.AddError(ex);
                _logManager.LogError(ex, "Error saving payroll classification");
            }


            return saveReponse;
        }

        public Task<ValidationResult> ValidateAsync(IPayroll payroll)
        {
            throw new NotImplementedException();
        }
    }
}
