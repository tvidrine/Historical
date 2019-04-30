// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class ClientApplicationService : IClientApplicationService
    {
        private readonly IAddressApplicationService _addressApplicationService;
        private readonly IClientRepository _clientRepository;
        private readonly IClientValidator _clientValidator;
        private readonly IContactApplicationService _contactApplicationService;
        private readonly ILogManager _logManager;

        public ClientApplicationService(
            ILogManager logManager,
            IClientRepository clientRepository,
            IClientValidator clientValidator,
            IAddressApplicationService addressApplicationService,
            IContactApplicationService contactApplicationService
        )
        {
            _logManager = logManager;
            _clientRepository = clientRepository;
            _clientValidator = clientValidator;
            _addressApplicationService = addressApplicationService;
            _contactApplicationService = contactApplicationService;
        }

        public async Task<CreateResponse<IClient>> CreateAsync()
        {
            var createResponse = new CreateResponse<IClient>();

            try
            {
                var client = new Client
                {
                    Name = "<New Client>",
                    ClientType = ClientTypeEnum.InsuranceCarrier,
                    ProcessType = AuditProcessTypeEnum.SharedAudit
                };

                // Create a billing contact
                var contactResponse = await _contactApplicationService.CreateAsync(client.Id, ContactTypeEnum.Billing);

                if (!contactResponse.IsSuccessful)
                    return createResponse.Join<CreateResponse<IClient>>(contactResponse);

                client.Contacts.Add(contactResponse.Content);

                // Create a client contact
                contactResponse = await _contactApplicationService.CreateAsync(client.Id, ContactTypeEnum.Client);

                if (!contactResponse.IsSuccessful)
                    return createResponse.Join<CreateResponse<IClient>>(contactResponse);

                client.Contacts.Add(contactResponse.Content);

                // Create an address
                var addressResponse = await _addressApplicationService.CreateAsync();

                if (!addressResponse.IsSuccessful)
                    return createResponse.Join<CreateResponse<IClient>>(addressResponse);

                client.Address = addressResponse.Content;

                createResponse.Content = client;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                createResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to create a new client");
            }

            return createResponse;
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();

            try
            {
                deleteResponse = await _clientRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete client");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClient>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IClient>>();

            try
            {
                getResponse = await _clientRepository.GetAllAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving clients");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IClientInfo>>();

            try
            {
                getResponse = await _clientRepository.GetInfoListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving clients");
            }

            return getResponse;
        }
        public async Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync(ClientSettingsEnum setting, object value)
        {
            var getResponse = new GetResponse<IReadOnlyList<IClientInfo>>();

            try
            {
                getResponse = await _clientRepository.GetInfoListAsync(setting, value);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving clients");
            }

            return getResponse;
        }

        public async Task<GetResponse<IClient>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IClient>();

            try
            {
                var getClientTask = _clientRepository.GetByIdAsync(id);
                var getAddressTask = _addressApplicationService.GetAllAsync(id);
                var getContactTask = _contactApplicationService.GetAllAsync(id);

                await Task.WhenAll(getAddressTask, getClientTask, getContactTask);

                getResponse = getClientTask.Result;

                if (getResponse.IsSuccessful)
                {
                    var client = getResponse.Content;

                    if (getAddressTask.Result.IsSuccessful)
                    {
                        var addresses = getAddressTask.Result.Content.ToList();
                        client.Address = addresses.FirstOrDefault();
                    }

                    if (getContactTask.Result.IsSuccessful)
                        client.Contacts = getContactTask.Result.Content.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving client");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<ClientConfiguration>>> GetConfigurationsAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<ClientConfiguration>>();

            try
            {
                getResponse = await _clientRepository.GetConfigurationsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "ClientApplicationService.GetConfigurationAsync");
            }
            return getResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IClient>>> SaveAllAsync(IReadOnlyList<IClient> clients)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IClient>>();

            try
            {
                foreach (var client in clients)
                {
                    var response = await SaveAsync(client);
                    saveResponse
                        .Join<SaveResponse<IReadOnlyList<IClient>>>(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving clients");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IClient>> SaveAsync(IClient client)
        {
            var response = new SaveResponse<IClient>();

            try
            {
                // Validate Information before saving
                response.FromValidationResult(await ValidateAsync(client));

                // 4. Begin transaction
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    // 5. Save Client
                    response = _clientRepository.SaveAsync(client).Result;

                    if (response.IsSuccessful)
                    {
                        // 6. Save Addresses
                        var addressResponse = _addressApplicationService.SaveAsync(client.Address).Result;

                        // 7. Save Contacts
                        var contactResponse = _contactApplicationService
                            .SaveAllAsync(client.Contacts as IReadOnlyList<IContact>).Result;

                        response.Join<SaveResponse>(addressResponse)
                            .Join<SaveResponse>(contactResponse);
                    }

                    if (response.IsSuccessful)
                        scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response.AddError(ex);
                _logManager.LogError(ex, "Error saving client");
            }

            return response;
        }

        public async Task<ValidationResult> ValidateAsync(IClient client)
        {
            // 1. Validate client
            var clientTask = _clientValidator.ValidateAsync(client);

            // 2. Validate Contact Information
            var tasks = client.Contacts?.Select(c => _contactApplicationService.ValidateAsync(c));
            var contactTask = tasks == null ? Task.FromResult(new ValidationResult[0]) : Task.WhenAll(tasks);

            // 3. Validate Address Information
            var addressTask = _addressApplicationService.ValidateAsync(client.Address);

            await Task.WhenAll(clientTask, contactTask, addressTask);

            // Get all errors if any
            var validationResult = clientTask.Result
                .From(contactTask.Result?.SelectMany(r => r.Errors).ToList())
                .From(addressTask.Result?.Errors);

            return validationResult;
        }
    }
}