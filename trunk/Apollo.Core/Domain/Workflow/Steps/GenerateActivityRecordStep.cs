// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Base;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Domain.Workflow.Steps
{
    [AuditActionKey("GenerateActivityRecord")]
    public class GenerateActivityRecordStep : IAuditAction  
    {
        private readonly IActivityNoteApplicationService _activityNoteApplicationService;
        private readonly ILogManager _logManager;

        public GenerateActivityRecordStep(IActivityNoteApplicationService activityNoteApplicationService, ILogManager logManager)
        {
            _activityNoteApplicationService = activityNoteApplicationService;
            _logManager = logManager;
        }
        public string RuleSetName { get; set; }
        public IToken Execute(IToken token)
        {
            try
            {

                if (string.IsNullOrEmpty(token.ActivityNote))
                    return token;

                var response = _activityNoteApplicationService.CreateAsync().Result;

                if (response.IsSuccessful)
                {
                    var activityNote = response.Content;
                    activityNote.AuditId = token.Audit.Id;
                    activityNote.ActivityTypeId = 1;
                    activityNote.ActionTypeId = 12;
                    activityNote.ActivityDate = DateTime.Now;
                    activityNote.UserId = 6;
                    activityNote.ActivityDescription = token.ActivityNote;
                    activityNote.EmailorPhone = string.Empty;
                    activityNote.CallResultId = 0;

                    var saveResponse = _activityNoteApplicationService.SaveAsync(activityNote).Result;
                    
                    if (!saveResponse.IsSuccessful)
                    {
                        token.SetException(new InvalidOperationException(saveResponse.Message));
                    }
                }

                
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "GenerateActivityRecordStep.Execute");
                token.SetException(e);
            }

            return token;
        }
    }
}