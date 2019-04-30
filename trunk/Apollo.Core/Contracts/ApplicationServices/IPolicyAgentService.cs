using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IPolicyAgentService
    {
        PolicyAgent CreateAgent(IUser user);
        Task<GetResponse<bool>> IsValidPolicyAuditAsync(int auditNumber, string policyNumber);
        Task<SaveResponse> SaveAgentInformationAsync(PolicyAgent agent);
    }
}