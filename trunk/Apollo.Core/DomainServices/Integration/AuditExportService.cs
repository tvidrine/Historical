// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.DomainServices.Integration;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.DomainServices.Integration
{
    public class AuditExportService : IAuditExportService
    {
        public Task<IntegrationResult> ExportRecordsAsync(ClientConfiguration client)
        {
            throw new System.NotImplementedException();
        }
    }
}