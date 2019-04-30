// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IClientApplicationService
    {
        Task<CreateResponse<IClient>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IClient>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IClient>>> GetAllAsync();
        Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync();
        Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync(ClientSettingsEnum setting, object value);
        Task<SaveResponse<IClient>> SaveAsync(IClient client);
        Task<SaveResponse<IReadOnlyList<IClient>>> SaveAllAsync(IReadOnlyList<IClient> clients);
	    Task<ValidationResult> ValidateAsync(IClient client);
        Task<GetResponse<IReadOnlyList<ClientConfiguration>>> GetConfigurationsAsync();
    }
}
