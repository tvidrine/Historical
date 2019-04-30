// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.RulesEngine.Contracts
{
    public interface IRuleGrammarTerminalRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IRuleGrammarTerminal>>> GetAllAsync();
        Task<GetResponse<IRuleGrammarTerminal>> GetByIdAsync(int id);
        Task<SaveResponse<IRuleGrammarTerminal>> SaveAsync(IRuleGrammarTerminal ruleGrammarTerminal);
        Task<SaveResponse<IReadOnlyList<IRuleGrammarTerminal>>> SaveAllAsync(IReadOnlyList<IRuleGrammarTerminal> ruleGrammarTerminal);
    }
}