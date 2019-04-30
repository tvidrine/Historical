// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Common;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class AddressApplicationService : IAddressApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressValidator _addressValidator;

        public AddressApplicationService(
            ILogManager logManager, 
            IAddressRepository addressRepository,
            IAddressValidator addressValidator)
        {
            _logManager = logManager;
            _addressRepository = addressRepository;
            _addressValidator = addressValidator;
        }

        public async Task<ICreateResponse<IAddress>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IAddress>
            {
                Content = new Address(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _addressRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete address");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IAddress>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAddress>();
            try
            {
                getResponse = await _addressRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving address");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAddress>>> GetAllAsync(int entityId)
        {
            var getResponse = new GetResponse<IReadOnlyList<IAddress>>();
            try
            {
                getResponse = await _addressRepository.GetAllAsync(entityId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving addresss");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IAddress>> SaveAsync(IAddress model)
        {
            var saveResponse = new SaveResponse<IAddress>();
            try
            {
                // Make sure address is valid before saving
                var validationResult = await _addressValidator.ValidateAsync(model);

                if (validationResult.IsValid)
                {
                    saveResponse = await _addressRepository.SaveAsync(model);
                }
                else
                {
                    saveResponse.AddErrors(validationResult.Errors);
	                saveResponse.Message = string.Join<ValidationFailure>(". \n", validationResult.Errors.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving address");
            }

            return saveResponse;
        }

	    public async Task<ValidationResult> ValidateAsync(IAddress address)
	    {
		    return await _addressValidator.ValidateAsync(address);
	    }

	    public async Task<SaveResponse<IReadOnlyList<IAddress>>> SaveAllAsync(IReadOnlyList<IAddress> modelList)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IAddress>>();
            try
            {
	            var isValid = true;
	            var messages = new List<string>();
	            foreach (var model in modelList)
	            {
					// Make sure addresses are valid before saving
		            var validationResult = await _addressValidator.ValidateAsync(model);

		            if (!validationResult.IsValid)
		            {
                        saveResponse.AddErrors(validationResult.Errors);
			            messages.Add(string.Join<ValidationFailure>(". \n", validationResult.Errors.ToArray()));
			            isValid = false;
		            }

				}
	            
				if(isValid)
					saveResponse = await _addressRepository.SaveAllAsync(modelList);
				else
				{
					saveResponse.Message = string.Join<string>(". \n", messages.ToArray());
				}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving addresss");
            }

            return saveResponse;
        }
	}
}
