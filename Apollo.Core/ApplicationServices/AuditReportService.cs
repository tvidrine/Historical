// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.ApplicationServices
{
    public class AuditReportService : IAuditReportService
    {
        public Task<IntegrationResult> ReportResults(ClientConfiguration client)
        {
            throw new System.NotImplementedException();
        }
    }
}