using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Identity;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IPolicyAgentService _policyAgentService;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;

        public UserApplicationService(
            ILogManager logManager,
            IUserRepository userRepository, IPolicyAgentService policyAgentService, IUserValidator userValidator)
        {
            _userRepository = userRepository;
            _policyAgentService = policyAgentService;
            _userValidator = userValidator;
            _logManager = logManager;
        }

        public async Task<ICreateResponse<IUser>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IUser>
            {
                Content = new User
                {
                    IsActive = true
                }
        });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete user");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IUser>> FindByEmailAsync(string email)
        {
            var response = new GetResponse<IUser>();

            try
            {
                response = await _userRepository.GetByEmailAsync(email);
             
            }
            catch (Exception e)
            {
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IUser>> FindByIdAsync(string userId)
        {
            var response = new GetResponse<IUser>();

            try
            {
                response = await _userRepository.GetByUserIdAsync(userId);
            }
            catch (Exception e)
            {
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<IUser>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IUser>>();
            try
            {
                getResponse = await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving users");
            }

            return getResponse;
        }

        public async Task<GetResponse<IUser>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IUser>();
            try
            {
                getResponse = await _userRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving user");
            }

            return getResponse;
        }

        public async Task<SaveResponse> RegisterUserAsync(RegisterRequest request)
        {
            var response = new SaveResponse();

            try
            {
                // 1. check if valid policy number and pin.
                var validResponse =
                    await _policyAgentService.IsValidPolicyAuditAsync(request.Identity.Pin,
                        request.Identity.PolicyNumber);

                if (validResponse.IsSuccessful && validResponse.Content)
                {
                    // 2. Save user
                    var userResponse = await _userRepository.SaveAsync(request.Identity);

                    response.Join<SaveResponse>(userResponse);

                    if (userResponse.IsSuccessful)
                    {
                        // 3. Create and save the agent
                        var agent = _policyAgentService.CreateAgent(userResponse.Content);
                        agent.PolicyNumber = request.Identity.PolicyNumber;

                        response = await _policyAgentService.SaveAgentInformationAsync(agent);
                    }
                    else
                    {
                        response.AddError($@"Unable to register user for Pin: {request.Identity.PolicyNumber}, Policy Number: {
                                request.Identity.Pin
                            }.  The Pin/Policy Number does not match our records.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
            }

            return response;
        }

        public async Task<SaveResponse> SaveAsync(IUser user)
        {
            var response = new SaveResponse();

            try
            {
                var validateResponse = await _userValidator.ValidateAsync(user);
                if(validateResponse.IsValid)
                    response = await _userRepository.SaveAsync(user);
                else
                {
                    response.AddErrors(validateResponse.Errors);
                }
            }
            catch (Exception e)
            {
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public Task<SaveResponse<IReadOnlyList<IUser>>> SaveAllAsync(IReadOnlyList<IUser> users)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResponse> SaveSessionStateAsync(IUser user)
        {
            var response = new SaveResponse();

            try
            {
                response = await _userRepository.SaveSessionStateAsync(user);
            }
            catch (Exception e)
            {
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }
    }
}