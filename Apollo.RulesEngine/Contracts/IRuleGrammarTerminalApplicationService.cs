// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.RulesEngine.Contracts
{
    public interface IRuleGrammarTerminalApplicationService
    {
        Task<ICreateResponse<IRuleGrammarTerminal>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IRuleGrammarTerminal>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IRuleGrammarTerminal>>> GetAllAsync();
        Task<SaveResponse<IRuleGrammarTerminal>> SaveAsync(IRuleGrammarTerminal ruleGrammarTerminal);
        Task<SaveResponse<IReadOnlyList<IRuleGrammarTerminal>>> SaveAllAsync(IReadOnlyList<IRuleGrammarTerminal> ruleGrammarTerminals);

    }
}
