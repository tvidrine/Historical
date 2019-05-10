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
    public class WorkflowDto : DtoBase<IAuditWorkflow, IAuditWorkflow>
    {
        #region Public Properties
        public string Name { get; set; }
        public string RootStepKey { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditWorkflow model)
        {
            Id = model.Id;
            Name = model.Name;
            RootStepKey = model.RootStepKey;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAuditWorkflow ToModel()
        {
            var model = new AuditWorkflow
            {
                Id = Id,
                Name = Name,
                RootStepKey = RootStepKey,
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