// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models.Audit
{
    public class AuditStepDto : DtoBase<IAuditStep, IAuditStep>
    {
        #region Public Properties
        public int Auditid { get; set; }
        public int EntityId { get; set; }
        public int WizardPageId { get; set; }
        public int StepOrder { get; set; }
        public string State { get; set; }
        public string PreviousStepState { get; set; }
        public string WizardPage { get; set; }
        public string WizardMenuText { get; set; }
        public string WizardMenuImageUrl { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedById { get; set; }
        public DateTimeOffset? CompletedOn { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditStep model)
        {
            Id = model.Id;
            Auditid = model.AuditId;
            EntityId = model.EntityId;
            WizardPageId = (int) model.WizardPageType;
            StepOrder = model.StepOrder;
            State = model.State;
            IsCompleted = model.IsCompleted;
            CompletedById = model.CompletedById;
            CompletedOn = model.CompletedOn;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAuditStep ToModel()
        {
            var model = new AuditStep
            {
                Id = Id,
                AuditId = Auditid,
                EntityId = EntityId,
                WizardPageType = (WizardPageEnum)WizardPageId,
                StepOrder = StepOrder,
                State = State,
                PreviousStepState = PreviousStepState,
                WizardPage = WizardPage,
                WizardMenuImageUrl = WizardMenuImageUrl,
                WizardMenuText = WizardMenuText,
                IsCompleted = IsCompleted,
                CompletedById = CompletedById,
                CompletedOn = CompletedOn,
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
