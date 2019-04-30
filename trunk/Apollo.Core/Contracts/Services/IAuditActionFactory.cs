// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Contracts.Services
{
    public interface IAuditActionFactory
    {
        void Add(string key, IAuditAction step);
        IAuditAction Get(string key);
    }
}