// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class LaborApplicationService : ILaborApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly ILaborRepository _laborRepository;
        private readonly ICertificateOfInsuranceApplicationService _certificateOfInsuranceApplicationService;
        
        public LaborApplicationService(ILogManager logManager
            , ILaborRepository laborRepository
            , ICertificateOfInsuranceApplicationService certificateOfInsuranceApplicationService)
        {
            _logManager = logManager;
            _laborRepository = laborRepository;
            _certificateOfInsuranceApplicationService = certificateOfInsuranceApplicationService;
        }

        public async Task<ICreateResponse<ILabor>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<ILabor>
            {
                Content = new Labor(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _laborRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete labor");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<ILabor>> GetAsync(int id)
        {
            var getResponse = new GetResponse<ILabor>();
            try
            {
                getResponse = await _laborRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving labor");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<ILabor>>> GetAllAsync(int auditId, int entityId)
        {
            var getResponse = new GetResponse<IReadOnlyList<ILabor>>();
            try
            {
                getResponse = await _laborRepository.GetAllAsync(auditId, entityId);

                if (getResponse.IsSuccessful)
                {
                    foreach (var labor in getResponse.Content)
                    {
                        // Get certificate information

                        var cerficateGetResponse =
                            await _certificateOfInsuranceApplicationService.GetForLaborAsync(labor.Id);

                        if (cerficateGetResponse.IsSuccessful)
                            labor.CertificateOfInsurance = cerficateGetResponse.Content;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving labors");
            }

            return getResponse;
        }

        public async Task<SaveResponse<ILabor>> SaveAsync(ILabor labor)
        {
            var saveResponse = new SaveResponse<ILabor>();
            try
            {
                
                saveResponse = await _laborRepository.SaveAsync(labor);

                if (saveResponse.IsSuccessful)
                {
                    // Save certificate of insurance
                    var savedLabor = saveResponse.Content;
                    
                    if (labor.CertificateOfInsurance != null)
                    {
                        savedLabor.CertificateOfInsurance = labor.CertificateOfInsurance;
                        var certificateResponse = await
                            _certificateOfInsuranceApplicationService.SaveAsync(savedLabor.CertificateOfInsurance);

                        if (certificateResponse.IsSuccessful)
                            savedLabor.CertificateOfInsurance = certificateResponse.Content;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving labor");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<ILabor>>> SaveAllAsync(IReadOnlyList<ILabor> labors)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<ILabor>>();
            try
            {
                saveResponse = await _laborRepository.SaveAllAsync(labors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving labors");
            }

            return saveResponse;
        }
        public Task<ValidationResult> ValidateAsync(ILabor labor)
        {
            throw new NotImplementedException();
        }
    }
}
