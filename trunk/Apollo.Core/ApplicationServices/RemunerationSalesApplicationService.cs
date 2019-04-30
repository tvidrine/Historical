// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Sales;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class RemunerationSalesApplicationService : IRemunerationSalesApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IRemunerationSalesRepository _remunerationSalesRepository;
        private readonly IRemunerationSalesValidator _validator;

        public RemunerationSalesApplicationService(ILogManager logManager, IRemunerationSalesRepository remunerationSalesRepository, IRemunerationSalesValidator validator)
        {
            _logManager = logManager;
            _remunerationSalesRepository = remunerationSalesRepository;
            _validator = validator;
        }

        public async Task<ICreateResponse<IRemunerationSales>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IRemunerationSales>
            {
                Content = new RemunerationSales(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _remunerationSalesRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete remunerationSales");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IRemunerationSales>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IRemunerationSales>();
            try
            {
                getResponse = await _remunerationSalesRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving remunerationSales");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IRemunerationSales>>> GetAllAsync(RemunerationRequest request)
        {
            var getResponse = new GetResponse<IReadOnlyList<IRemunerationSales>>();
            try
            {
                getResponse = await _remunerationSalesRepository.GetAllAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving remunerationSaless");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IRemunerationSales>> SaveAsync(IRemunerationSales remunerationSales)
        {
            var saveResponse = new SaveResponse<IRemunerationSales>();
            try
            {
                saveResponse = await _remunerationSalesRepository.SaveAsync(remunerationSales);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving remunerationSales");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IRemunerationSales>>> SaveAllAsync(IReadOnlyList<IRemunerationSales> remunerationSales)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IRemunerationSales>>();
            try
            {
                saveResponse = await _remunerationSalesRepository.SaveAllAsync(remunerationSales);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving remunerationSaless");
            }

            return saveResponse;
        }
        public Task<ValidationResult> ValidateAsync(IRemunerationSales remunerationSales)
        {
            throw new NotImplementedException();
        }
    }
}
