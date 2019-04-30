// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.Contracts.DomainServices.Integration
{
    public interface IAuditImportService
    {
        Task<IntegrationResult> ImportRecordsAsync(ClientConfiguration client);
    }
}