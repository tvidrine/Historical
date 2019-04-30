// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class SystemInfoApplicationService : ISystemInfoApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly ISystemInfoRepository _systemInfoRepository;
        
        public SystemInfoApplicationService(ILogManager logManager, ISystemInfoRepository systemInfoRepository)
        {
            _logManager = logManager;
            _systemInfoRepository = systemInfoRepository;
        }

        public async Task<GetResponse<ISystemInfo>> GetAsync()
        {
            var getResponse = new GetResponse<ISystemInfo>();
            try
            {
                getResponse = await _systemInfoRepository.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving systemInfo");
            }

            return getResponse;
        }

        public async Task<SaveResponse<ISystemInfo>> SaveAsync(ISystemInfo systemInfo)
        {
            var saveResponse = new SaveResponse<ISystemInfo>();
            try
            {
                saveResponse = await _systemInfoRepository.SaveAsync(systemInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving systemInfo");
            }

            return saveResponse;
        }

        
    }
}
