// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/2/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Domain.Workflow;

namespace Apollo.Infrastructure.Models.Workflow
{
    public class WorkflowStepDto : DtoBase<IAuditWorkflowStep, IAuditWorkflowStep>
    {
        #region Public Properties
        public int WorkflowId { get; set; }
        public string Key { get; set; }
        public string RuleSetKey { get; set; }
        public string OnFailureStepKey { get; set; }
        public string OnSuccessStepKey { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditWorkflowStep model)
        {
            Id = model.Id;
            WorkflowId = model.WorkflowId;
            Key = model.Key;
            RuleSetKey = model.RuleSetKey;
            OnFailureStepKey = model.OnFailureStepKey;
            OnSuccessStepKey = model.OnSuccessStepKey;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAuditWorkflowStep ToModel()
        {
            var model = new AuditWorkflowStep
            {
                Id = Id,
                Key = Key,
                RuleSetKey = RuleSetKey,
                OnFailureStepKey = OnFailureStepKey,
                OnSuccessStepKey = OnSuccessStepKey,
                CreatedOn = CreatedOn,
                CreatedById = CreatedById,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById
            };

            return model;
        }

        #endregion ToModel
    }
}
