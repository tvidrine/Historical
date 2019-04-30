// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
	public interface IAddressApplicationService
	{
		Task<ICreateResponse<IAddress>> CreateAsync();
		Task<DeleteResponse> DeleteAsync(int id);
		Task<GetResponse<IReadOnlyList<IAddress>>> GetAllAsync(int entityId);
		Task<GetResponse<IAddress>> GetAsync(int id);
		Task<SaveResponse<IReadOnlyList<IAddress>>> SaveAllAsync(IReadOnlyList<IAddress> addresses);
		Task<SaveResponse<IAddress>> SaveAsync(IAddress address);
		Task<ValidationResult> ValidateAsync(IAddress address);
	}
}