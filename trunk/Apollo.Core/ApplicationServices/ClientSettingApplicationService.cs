// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Client;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class ClientSettingApplicationService : IClientSettingApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IClientSettingRepository _clientSettingRepository;
        private readonly IClientSettingValidator _validator;

        public ClientSettingApplicationService(ILogManager logManager, IClientSettingRepository clientSettingRepository, IClientSettingValidator validator)
        {
            _logManager = logManager;
            _clientSettingRepository = clientSettingRepository;
            _validator = validator;
        }

        public async Task<ICreateResponse<IClientSetting>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IClientSetting>
            {
                Content = new ClientSetting(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _clientSettingRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete clientSetting");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IClientSetting>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IClientSetting>();
            try
            {
                getResponse = await _clientSettingRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving clientSetting");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClientSetting>>> GetAllAsync(int clientId)
        {
            var getResponse = new GetResponse<IReadOnlyList<IClientSetting>>();
            try
            {
                getResponse = await _clientSettingRepository.GetAllAsync(clientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving clientSettings");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IClientSetting>> SaveAsync(IClientSetting clientSetting)
        {
            var saveResponse = new SaveResponse<IClientSetting>();
            try
            {
                saveResponse = await _clientSettingRepository.SaveAsync(clientSetting);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving clientSetting");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IClientSetting>>> SaveAllAsync(IReadOnlyList<IClientSetting> clientSettings)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IClientSetting>>();
            try
            {
                saveResponse = await _clientSettingRepository.SaveAllAsync(clientSettings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving clientSettings");
            }

            return saveResponse;
        }
        public async Task<ValidationResult> ValidateAsync(IClientSetting clientSetting)
        {
            return await _validator.ValidateAsync(clientSetting);
        }
    }
}
