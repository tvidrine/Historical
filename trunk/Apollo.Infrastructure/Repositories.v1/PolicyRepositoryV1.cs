// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Responses;

namespace Apollo.Infrastructure.Repositories.v1
{
    public class PolicyRepositoryV1  : IPolicyRepository
    {
        public Task<DeleteResponse> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetResponse<IReadOnlyList<PolicyInfo>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<GetResponse<IPolicy>> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetResponse<bool>> IsValidPolicyAgentAsync(int assignmentNumber, string policyNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResponse> SaveAgentAsync(PolicyAgent agent)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResponse<IPolicy>> SaveAsync(IPolicy policy)
        {
            throw new System.NotImplementedException();
        }
    }
}