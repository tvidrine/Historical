// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Apollo.Core.Contracts.Workflow
{
    public interface IAuditEngine
    {
        void Begin(string workflowName);
        Task BeginAsync(string workflowName);
    }
}