// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/01/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Services;
using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Domain.Workflow
{
    public class AuditActionFactory : Dictionary<string, IAuditAction>, IAuditActionFactory
    {
       
        public IAuditAction Get(string key)
        {
            return this[key];
        }
    }
}