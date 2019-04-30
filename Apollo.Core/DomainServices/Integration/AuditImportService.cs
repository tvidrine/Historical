// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.DomainServices.Integration;
using Apollo.Core.Contracts.Services;
using Apollo.Core.Messages.Responses;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.DomainServices.Integration
{
    public class AuditImportService : IAuditImportService
    {
        private readonly ILogManager _logManager;
        private readonly ICommunicationProviderFactory _communicationProviderFactory;
        private readonly IAuditApplicationService _auditApplicationService;
        private readonly IClientTransformFactory _clientTransformFactory;

        public AuditImportService(
            ILogManager logManager,
            ICommunicationProviderFactory communicationProviderFactory, 
            IAuditApplicationService auditApplicationService,
            IClientTransformFactory clientTransformFactory)
        {
            _logManager = logManager;
            _communicationProviderFactory = communicationProviderFactory;
            _auditApplicationService = auditApplicationService;
            _clientTransformFactory = clientTransformFactory;
        }
        public async Task<IntegrationResult> ImportRecordsAsync(ClientConfiguration clientConfiguration)
        {
            var response = new IntegrationResult();
            try
            {
                // 1. Get the documents for the client
                var result =await _communicationProviderFactory
                   .GetProvider(clientConfiguration.CommunicateUsing)
                   .GetPacketsAsync(clientConfiguration);

                // 2. Transform data
                var transformResult = _clientTransformFactory
                    .GetTransform(clientConfiguration.ClientKey)
                    .To(result.Content, clientConfiguration);

                response.Join<IntegrationResult>(transformResult);

                if (response.IsSuccessful)
                {
                    // 3. Save data
                    response.Join<IntegrationResult>(
                        await _auditApplicationService.SaveAllAsync(transformResult.Content));
                }
            }
            catch (Exception ex)
            {
               response.AddError(ex);
                _logManager.LogError(ex, "AuditImportService.ImportRecordsAsync");
            }

            return response;
        }
    }
}