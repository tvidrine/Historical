// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Contracts.Services;
using Apollo.Core.Domain.Enums;
using Apollo.Infrastructure.Providers;

namespace Apollo.Infrastructure.Factories
{
    public class CommunicationProviderFactory : ICommunicationProviderFactory
    {
        private readonly Dictionary<CommunicationServiceTypes, ICommunicationProvider> _providers;

        public CommunicationProviderFactory(IAuditConfiguration configuration, ILogManager logManager)
        {
            _providers = new Dictionary<CommunicationServiceTypes, ICommunicationProvider>
            {
                {CommunicationServiceTypes.Email, new EmailProvider(configuration, logManager) },
                {CommunicationServiceTypes.Ftp, new FtpProvider(configuration, logManager) }
            };
        }
        public ICommunicationProvider GetProvider(CommunicationServiceTypes serviceType)
        {
            return _providers[serviceType];
        }
    }
}