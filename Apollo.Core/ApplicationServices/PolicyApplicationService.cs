// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/22/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class PolicyApplicationService : IPolicyApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IPolicyRepository _policyRepository;

        public PolicyApplicationService(ILogManager logManager, IPolicyRepository policyRepository)
        {
            _logManager = logManager;
            _policyRepository = policyRepository;
        }

        public async Task<ICreateResponse<IPolicy>> CreateAsync(AuditRequest request)
        {
            return await Task.Run(() => new CreateResponse<IPolicy>
            {
                Content = new Policy
                {
                    PolicyNumber = request.PolicyNumber,
                    EffectiveStart = request.EffectiveDate,
                    EffectiveEnd = request.ExpiryDate
                }
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _policyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete policy");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IPolicy>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IPolicy>();
            try
            {
                getResponse = await _policyRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving policy");
            }

            return getResponse;
        }

        public Task<GetResponse<IPolicy>> GetAsync(AuditRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetResponse<IReadOnlyList<IPolicy>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IPolicy>>();
            try
            {
                getResponse = await _policyRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving policys");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IPolicy>> SaveAsync(IPolicy policy)
        {
            var saveResponse = new SaveResponse<IPolicy>();
            try
            {
                saveResponse = await _policyRepository.SaveAsync(policy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving policy");
            }

            return saveResponse;
        }

        public Task<ValidationResult> ValidateAsync(IPolicy policy)
        {
            throw new NotImplementedException();
        }
    }
}
