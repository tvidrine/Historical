// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Client;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class WpdConfigApplicationService : IWpdConfigApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IWpdConfigRepository _wpdConfigRepository;

        public WpdConfigApplicationService(ILogManager logManager, IWpdConfigRepository wpdConfigRepository)
        {
            _logManager = logManager;
            _wpdConfigRepository = wpdConfigRepository;
        }

        public async Task<ICreateResponse<IWpdConfig>> CreateAsync(int clientId)
        {
            return await Task.Run(() => new CreateResponse<IWpdConfig>
            {
                Content = new WpdConfig(clientId),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _wpdConfigRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete wpdConfig");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IWpdConfig>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IWpdConfig>();
            try
            {
                getResponse = await _wpdConfigRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving wpdConfig");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IWpdConfig>> SaveAsync(IWpdConfig wpdConfig)
        {
            var saveResponse = new SaveResponse<IWpdConfig>();
            try
            {
                saveResponse = await _wpdConfigRepository.SaveAsync(wpdConfig);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving wpdConfig");
            }

            return saveResponse;
        }

    }
}
