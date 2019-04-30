// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IClientSettingApplicationService
    {
        Task<ICreateResponse<IClientSetting>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IClientSetting>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IClientSetting>>> GetAllAsync(int clientId);
        Task<SaveResponse<IClientSetting>> SaveAsync(IClientSetting clientSetting);
        Task<SaveResponse<IReadOnlyList<IClientSetting>>> SaveAllAsync(IReadOnlyList<IClientSetting> clientSettings);
        Task<ValidationResult> ValidateAsync(IClientSetting clientSetting);
    }
}
