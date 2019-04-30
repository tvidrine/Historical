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
    public class ContactApplicationService : IContactApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IContactRepository _contactRepository;
	    private readonly IContactValidator _contactValidator;
        private readonly IAddressApplicationService _addressApplicationService;

        public ContactApplicationService(ILogManager logManager, 
	        IContactRepository contactRepository, 
	        IContactValidator contactValidator,
	        IAddressApplicationService addressApplicationService)
        {
            _logManager = logManager;
            _contactRepository = contactRepository;
	        _contactValidator = contactValidator;
            _addressApplicationService = addressApplicationService;
        }

        public async Task<ICreateResponse<IContact>> CreateAsync(int entityId, ContactTypeEnum contactType)
        {
            return await Task.Run(() => new CreateResponse<IContact>
            {
                Content = new Contact(entityId){ContactType = contactType}
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _contactRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete contact");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IContact>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IContact>();
            try
            {
                getResponse = await _contactRepository.GetByIdAsync(id);

                //if (getResponse.IsSuccessful)
                //{
                //    var addressResponse = await _addressApplicationService.GetAllAsync(id);
                //    if (addressResponse.IsSuccessful)
                //        getResponse.Content.Address = addressResponse.Content.FirstOrDefault();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving contact");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IContact>>> GetAllAsync(int entityId)
        {
            var getResponse = new GetResponse<IReadOnlyList<IContact>>();
            try
            {
                getResponse = await _contactRepository.GetAllAsync(entityId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving contacts");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IContact>> SaveAsync(IContact model)
        {
            var saveResponse = new SaveResponse<IContact>();
            try
            {
	            // Make sure contact is valid before saving
	            var validationResult = await _contactValidator.ValidateAsync(model);
	            if (validationResult.IsValid)
	            {
		            saveResponse = await _contactRepository.SaveAsync(model);
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
                _logManager.LogError(ex, "Error saving contact");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IContact>>> SaveAllAsync(IReadOnlyList<IContact> modelList)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IContact>>();
            try
            {
	            var isValid = true;
	            var messages = new List<string>();

	            foreach (var model in modelList)
	            {
		            // Make sure contacts are valid before saving
		            var validationResult = await _contactValidator.ValidateAsync(model);

		            if (!validationResult.IsValid)
		            {
			            messages.Add(string.Join<ValidationFailure>(". \n", validationResult.Errors.ToArray()));
			            isValid = false;
		            }
                    saveResponse.AddErrors(validationResult.Errors);

	            }

	            if (isValid)
		            saveResponse = await _contactRepository.SaveAllAsync(modelList);
	            else
	            {
		            saveResponse.Message = string.Join<string>(". \n", messages.ToArray());
	            }
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving contacts");
            }

            return saveResponse;
        }

	    public async Task<ValidationResult> ValidateAsync(IContact contact)
	    {
		    return await _contactValidator.ValidateAsync(contact);
	    }
    }
}
