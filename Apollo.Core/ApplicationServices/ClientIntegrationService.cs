// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.DomainServices.Integration;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.ApplicationServices
{
    public class ClientIntegrationService : IClientIntegrationService
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly ILogManager _logManager;
        private readonly IAuditImportService _importService;
        private readonly IAuditExportService _exportService;
        private readonly IAuditReportService _reportService;

        public ClientIntegrationService(
            ILogManager logManager, 
            IClientApplicationService clientApplicationService, 
            IAuditExportService exportService, 
            IAuditImportService importService,
            IAuditReportService resportService)
        {
            _logManager = logManager;
            _clientApplicationService = clientApplicationService;
            _importService = importService;
            _exportService = exportService;
            _reportService = resportService;
        }
        public async Task<IntegrationResult> ExecuteAsync()
        {
            var result = new IntegrationResult();
            try
            {
                // 1. Get client configurations
                var response = await _clientApplicationService.GetConfigurationsAsync();

                if (!response.IsSuccessful)
                {
                    return result.Join<IntegrationResult>(response);
                }

                var clientConfigurations = response.Content;
                
                var clientsToExecute = clientConfigurations
                    .Where(c => c.ActionToPerform != IntegrationActionTypes.None);

                foreach (var clientConfig in clientsToExecute)
                {
                    // 2. Import Records
                    result = await _importService.ImportRecordsAsync(clientConfig);

                    // TODO : Save data stream
                    // 3. Save data stream
                    
                    // 4. Export Records
                    result.Join<IntegrationResult>(await _exportService.ExportRecordsAsync(clientConfig));

                    // 5. Report Results
                    result.Join<IntegrationResult>(await _reportService.ReportResults(clientConfig));
                }
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "ClientIntegrationService.ExecuteAsync");
                result.AddError(e);
            }

            return result;
        }
    }
}