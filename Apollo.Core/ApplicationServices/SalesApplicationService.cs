// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Domain.Sales;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class SalesApplicationService : ISalesApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesValidator _salesValidator;
        private readonly IFileUploadApplicationService _fileUploadApplicationService;

        public SalesApplicationService(
            ILogManager logManager, 
            ISalesRepository salesRepository, 
            ISalesValidator salesValidator,
            IFileUploadApplicationService fileUploadApplicationService)
        {
            _logManager = logManager;
            _salesRepository = salesRepository;
            _salesValidator = salesValidator;
            _fileUploadApplicationService = fileUploadApplicationService;
        }

        public async Task<ICreateResponse<ISales>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<ISales>
            {
                Content = new Sales(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _salesRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete sales");
            }

            return deleteResponse;
        }
        public async Task<DeleteResponse> DeleteAllAsync(int auditId, int entityId, int locationId)
        {
            var deleteResponse = new DeleteResponse();

            try
            {
                deleteResponse = await _salesRepository.DeleteAllAsync(auditId, entityId, locationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete sales");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<ISales>> GetAsync(int id)
        {
            var getResponse = new GetResponse<ISales>();
            try
            {
                getResponse = await _salesRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving sales");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<ISales>>> GetAllSalesAsync(int auditId, int entityId, SalesPeriodType periodType)
        {
            var getResponse = new GetResponse<IReadOnlyList<ISales>>();
            try
            {
                getResponse = await _salesRepository.GetAllSalesAsync(auditId, entityId, periodType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving saless");
            }

            return getResponse;
        }
        public async Task<GetResponse<IReadOnlyList<ISales>>> GetAllSalesAsync(int auditId, int entityId, int locationId, SalesPeriodType periodType)
        {
            var getResponse = new GetResponse<IReadOnlyList<ISales>>();
            try
            {
                getResponse = await _salesRepository.GetAllSalesAsync(auditId, entityId, periodType);

                if (getResponse.IsSuccessful)
                    getResponse.Content = getResponse.Content
                        .Where(s => s.LocationId == locationId)
                        .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving saless");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<ISalesVerification>>> GetAllSalesVerificationAsync(int auditId, int entityId, string state, int locationId, SalesPeriodType periodType)
        {
            var getResponse = new GetResponse<IReadOnlyList<ISalesVerification>>();
            try
            {
                getResponse = await _salesRepository.GetAllSalesVerificationAsync(auditId, entityId, state, locationId, periodType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving saless");
            }

            return getResponse;
        }

        public async Task<SaveResponse<ISales>> SaveAsync(ISales sales)
        {
            var saveResponse = new SaveResponse<ISales>();
            try
            {
                saveResponse = await _salesRepository.SaveAsync(sales);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving sales");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<ISales>>> SaveAllSalesAsync(IReadOnlyList<ISales> sales)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<ISales>>();
            try
            {
                saveResponse = await _salesRepository.SaveAllSalesAsync(sales);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving saless");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<ISalesVerification>>> SaveAllSalesVerficationAsync(List<ISalesVerification> salesVerifications)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<ISalesVerification>>();
            try
            {
                // Save verification files
                var filesToSave = salesVerifications
                    .Where(s => s.VerificationFile != null)
                    .Select(s => s.VerificationFile)
                    .ToList();

                var uploadSaveResponse = await _fileUploadApplicationService.SaveAllAsync(filesToSave);

                if (uploadSaveResponse.IsSuccessful)
                {
                    foreach (var file in uploadSaveResponse.Content)
                    {
                        var sale = salesVerifications.First(s => s.VerificationFile.OriginalFileName == file.OriginalFileName)
                            .VerificationFile = file;
                    }
                }

                saveResponse = await _salesRepository.SaveAllSalesVerificationAsync(salesVerifications);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving saless");
            }

            return saveResponse;
        }

        public async Task<ValidationResult> ValidateAsync(ISales sales)
        {
            return await _salesValidator.ValidateAsync(sales);
        }
    }
}
