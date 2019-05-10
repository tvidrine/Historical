// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Data;
using Apollo.Core.Contracts.Services;
using Apollo.Infrastructure.Transforms.BerkleyNet;

namespace Apollo.Infrastructure.Factories
{
    public class ClientTransformFactory : IClientTransformFactory
    {
        private readonly Dictionary<Guid, IClientTransform> _transforms;

        public ClientTransformFactory(ILogManager logManager, IAuditApplicationService auditApplicationService)
        {
            _transforms = new Dictionary<Guid, IClientTransform>
            {
                {BerkleynetTransform.ClientKey, new BerkleynetTransform(logManager, auditApplicationService)}
            };
        }

        public IClientTransform GetTransform(Guid clientKey)
        {
            return _transforms[clientKey];
        }
    }
}