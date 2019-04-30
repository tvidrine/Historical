// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class CertificateOfInsuranceApplicationService : ICertificateOfInsuranceApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly ICertificateOfInsuranceRepository _certificateOfInsuranceRepository;
        private readonly ICertificateOfInsuranceValidator _certificateOfInsuranceValidator;
        private readonly IFileUploadApplicationService _fileUploadApplicationService;

        public CertificateOfInsuranceApplicationService(ILogManager logManager, 
            ICertificateOfInsuranceRepository certificateOfInsuranceRepository,
            ICertificateOfInsuranceValidator certificateOfInsuranceValidator,
            IFileUploadApplicationService fileUploadApplicationService)
        {
            _logManager = logManager;
            _certificateOfInsuranceRepository = certificateOfInsuranceRepository;
            _certificateOfInsuranceValidator = certificateOfInsuranceValidator;
            _fileUploadApplicationService = fileUploadApplicationService;
        }

        public async Task<ICreateResponse<ICertificateOfInsurance>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<ICertificateOfInsurance>
            {
                Content = new CertificateOfInsurance(),
            });
        }
        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _certificateOfInsuranceRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete certificateOfInsurance");
            }

            return deleteResponse;
        }
        public async Task<GetResponse<ICertificateOfInsurance>> GetAsync(int id)
        {
            var getResponse = new GetResponse<ICertificateOfInsurance>();
            try
            {
                getResponse = await _certificateOfInsuranceRepository.GetByIdAsync(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving certificateOfInsurance");
            }

            return getResponse;
        }
        public async Task<GetResponse<ICertificateOfInsurance>> GetForLaborAsync(int laborId)
        {
            var getResponse = new GetResponse<ICertificateOfInsurance>();
            try
            {
                getResponse = await _certificateOfInsuranceRepository.GetForLaborAsync(laborId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving certificateOfInsurances");
            }

            return getResponse;
        }

        public async Task<SaveResponse<ICertificateOfInsurance>> SaveAsync(ICertificateOfInsurance certificateOfInsurance)
        {
            var saveResponse = new SaveResponse<ICertificateOfInsurance>();
            try
            {
                // Save file upload if a file was uploaded
                if (certificateOfInsurance.File != null)
                {
                    var fileUploadResponse = await _fileUploadApplicationService.SaveAsync(certificateOfInsurance.File);
                    certificateOfInsurance.File = fileUploadResponse.Content;
                }
                saveResponse = await _certificateOfInsuranceRepository.SaveAsync(certificateOfInsurance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving certificateOfInsurance");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<ICertificateOfInsurance>>> SaveAllAsync(IReadOnlyList<ICertificateOfInsurance> certificateOfInsurances)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<ICertificateOfInsurance>>();
            try
            {

                saveResponse = await _certificateOfInsuranceRepository.SaveAllAsync(certificateOfInsurances);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving certificateOfInsurances");
            }

            return saveResponse;
        }
        public async Task<ValidationResult> ValidateAsync(ICertificateOfInsurance certificateOfInsurance)
        {
            return await _certificateOfInsuranceValidator.ValidateAsync(certificateOfInsurance);
        }
    }
}
