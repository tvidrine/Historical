using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IPolicyRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IPolicy>>> GetAllAsync();
        Task<GetResponse<IPolicy>> GetByIdAsync(int id);
        Task<GetResponse<bool>> IsValidPolicyAgentAsync(int assignmentNumber, string policyNumber);
        Task<SaveResponse> SaveAgentAsync(PolicyAgent agent);
        Task<SaveResponse<IPolicy>> SaveAsync(IPolicy policy);
    }
}