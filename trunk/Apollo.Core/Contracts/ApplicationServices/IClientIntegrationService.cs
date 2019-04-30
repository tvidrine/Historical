// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IClientIntegrationService
    {
        Task<IntegrationResult> ExecuteAsync();
    }
}