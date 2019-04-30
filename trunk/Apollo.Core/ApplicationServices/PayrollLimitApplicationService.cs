// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Payroll;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class PayrollLimitApplicationService : IPayrollLimitApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IPayrollLimitRepository _payrollLimitRepository;
        private readonly IPayrollLimitValidator _payrollLimitValidator;

        public PayrollLimitApplicationService(ILogManager logManager, IPayrollLimitRepository payrollLimitRepository, IPayrollLimitValidator payrollLimitValidator)
        {
            _logManager = logManager;
            _payrollLimitRepository = payrollLimitRepository;
            _payrollLimitValidator = payrollLimitValidator;
        }

        public async Task<ICreateResponse<IPayrollLimit>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IPayrollLimit>
            {
                Content = new PayrollLimit(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _payrollLimitRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete payrollLimit");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IPayrollLimit>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IPayrollLimit>();
            try
            {
                getResponse = await _payrollLimitRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving payrollLimit");
            }

            return getResponse;
        }

        public async Task<GetResponse<IPayrollLimit>> GetAsync(PayrollLimitRequest request)
        {
            var getResponse = new GetResponse<IPayrollLimit>();
            try
            {
                getResponse = await _payrollLimitRepository.GetAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving payrollLimit");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IPayrollLimit>>> GetAllAsync(PayrollLimitRequest request)
        {
            var getResponse = new GetResponse<IReadOnlyList<IPayrollLimit>>();
            try
            {
                getResponse = await _payrollLimitRepository.GetAllAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving payrollLimits");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IPayrollLimit>> SaveAsync(IPayrollLimit payrollLimit)
        {
            var saveResponse = new SaveResponse<IPayrollLimit>();
            try
            {
                saveResponse = await _payrollLimitRepository.SaveAsync(payrollLimit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving payrollLimit");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IPayrollLimit>>> SaveAllAsync(IReadOnlyList<IPayrollLimit> payrollLimits)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IPayrollLimit>>();
            try
            {
                saveResponse = await _payrollLimitRepository.SaveAllAsync(payrollLimits);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving payrollLimits");
            }

            return saveResponse;
        }
        public async Task<ValidationResult> ValidateAsync(IPayrollLimit payrollLimit)
        {
            var result = await _payrollLimitValidator.ValidateAsync(payrollLimit);

            return result;
        }
    }
}
