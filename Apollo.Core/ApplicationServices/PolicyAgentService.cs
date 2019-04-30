using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class PolicyAgentService : IPolicyAgentService
    {
        private readonly IPolicyRepository _policyRepository;

        public PolicyAgentService(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public PolicyAgent CreateAgent(IUser user)
        {
            return new PolicyAgent
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                MiddleInitial = user.MiddleInitial,
                LastName = user.LastName
            };
        }

        public async Task<GetResponse<bool>> IsValidPolicyAuditAsync(int auditNumber, string policyNumber)
        {
            var response = new GetResponse<bool>();

            try
            {
                response = await _policyRepository.IsValidPolicyAgentAsync(auditNumber, policyNumber);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
            }

            return response;
        }

        public async Task<SaveResponse> SaveAgentInformationAsync(PolicyAgent agent)
        {
            var response = new SaveResponse();

            try
            {
                response = await _policyRepository.SaveAgentAsync(agent);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
            }

            return response;
        }
    }
}