// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IRuleSetRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IRuleSet>>> GetAllAsync();
        Task<GetResponse<IRuleSet>> GetByIdAsync(int id);
        Task<GetResponse<IRuleSet>> GetByNameAsync(string name);
        Task<SaveResponse<IRuleSet>> SaveAsync(IRuleSet ruleSet);
        Task<SaveResponse<IReadOnlyList<IRuleSet>>> SaveAllAsync(IReadOnlyList<IRuleSet> ruleSet);
    }
}
