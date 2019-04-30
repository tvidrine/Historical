// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Activity;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class ActivityNoteApplicationService : IActivityNoteApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IActivityNoteRepository _activityNoteRepository;

        public ActivityNoteApplicationService(ILogManager logManager, IActivityNoteRepository activityNoteRepository)
        {
            _logManager = logManager;
            _activityNoteRepository = activityNoteRepository;
        }

        public async Task<ICreateResponse<IActivityNote>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IActivityNote>
            {
                Content = new ActivityNote
                {
                    CreatedOn =  DateTimeOffset.Now
                }
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _activityNoteRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete activityNote");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IActivityNote>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IActivityNote>();
            try
            {
                getResponse = await _activityNoteRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving activityNote");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IActivityNote>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IActivityNote>>();
            try
            {
                getResponse = await _activityNoteRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving activityNotes");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IActivityNote>> SaveAsync(IActivityNote activityNote)
        {
            var saveResponse = new SaveResponse<IActivityNote>();
            try
            {
                saveResponse = await _activityNoteRepository.SaveAsync(activityNote);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving activityNote");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IActivityNote>>> SaveAllAsync(IReadOnlyList<IActivityNote> activityNotes)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IActivityNote>>();
            try
            {
                saveResponse = await _activityNoteRepository.SaveAllAsync(activityNotes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving activityNotes");
            }

            return saveResponse;
        }

        public Task<ValidationResult> ValidateAsync(IActivityNote activityNote)
        {
            throw new NotImplementedException();
        }
    }
}
