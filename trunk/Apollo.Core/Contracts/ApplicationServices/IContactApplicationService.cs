// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Common;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IContactApplicationService
    {
        Task<ICreateResponse<IContact>> CreateAsync(int entityId, ContactTypeEnum contactType);
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IContact>> GetAsync(int id);
        Task<GetResponse<IReadOnlyList<IContact>>> GetAllAsync(int entityId);
        Task<SaveResponse<IContact>> SaveAsync(IContact contact);
        Task<SaveResponse<IReadOnlyList<IContact>>> SaveAllAsync(IReadOnlyList<IContact> contacts);
	    Task<ValidationResult> ValidateAsync(IContact contact);
	}
}
