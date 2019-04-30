// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/7/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class LocationApplicationService : ILocationApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly ILocationRepository _locationRepository;

        public LocationApplicationService(ILogManager logManager, ILocationRepository locationRepository)
        {
            _logManager = logManager;
            _locationRepository = locationRepository;
        }

        public async Task<ICreateResponse<ILocation>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<ILocation>
            {
                Content = new Location(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _locationRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete location");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<ILocation>> GetAsync(int id)
        {
            var getResponse = new GetResponse<ILocation>();
            try
            {
                getResponse = await _locationRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving location");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<ILocation>>> GetAllAsync(int entityId)
        {
            var getResponse = new GetResponse<IReadOnlyList<ILocation>>();
            try
            {
                getResponse = await _locationRepository.GetAllAsync(entityId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving locations");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<ILocationInfo>>> GetInfoListAsync(int entityId)
        {
            var getResponse = new GetResponse<IReadOnlyList<ILocationInfo>>();
            try
            {
                getResponse = await _locationRepository.GetInfoListAsync(entityId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving locations");
            }

            return getResponse;
        }

        public async Task<SaveResponse<ILocation>> SaveAsync(ILocation location)
        {
            var saveResponse = new SaveResponse<ILocation>();
            try
            {
                saveResponse = await _locationRepository.SaveAsync(location);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving location");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<ILocation>>> SaveAllAsync(IReadOnlyList<ILocation> locations)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<ILocation>>();
            try
            {
                saveResponse = await _locationRepository.SaveAllAsync(locations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving locations");
            }

            return saveResponse;
        }
        public Task<ValidationResult> ValidateAsync(ILocation location)
        {
            throw new NotImplementedException();
        }
    }
}
