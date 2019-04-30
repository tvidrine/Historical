// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IRuleApplicationService
    {
        Task<ICreateResponse<IRuleSet>> CreateAsync();
        GetResponse<IRuleSet> CreateAssembly(IRuleSet ruleSet, bool forTesting = false);
        Task<DeleteResponse> DeleteAsync(int id);
        object ExecuteRule(IRuleSet ruleSet, string ruleName);
        Task<GetResponse<IRuleSet>> GetByIdAsync(int id);
        Task<GetResponse<IRuleSet>> GetByNameAsync(string name);
        Task<SaveResponse<IRuleSet>> SaveAsync(IRuleSet ruleSet);
        Task<SaveResponse<IReadOnlyList<IRuleSet>>> SaveAllAsync(IReadOnlyList<IRuleSet> ruleSets);
    }
}