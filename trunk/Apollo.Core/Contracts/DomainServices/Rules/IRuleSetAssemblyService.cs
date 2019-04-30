// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/05/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Rules;

namespace Apollo.Core.Contracts.DomainServices.Rules
{
    public interface IRuleSetAssemblyService
    {
        byte[] CreateAssembly(IRuleSet ruleSet);
    }
}