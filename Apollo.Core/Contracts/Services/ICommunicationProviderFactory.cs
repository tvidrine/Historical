// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Providers;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Services
{
    public interface ICommunicationProviderFactory
    {
        ICommunicationProvider GetProvider(CommunicationServiceTypes serviceType);
    }
}