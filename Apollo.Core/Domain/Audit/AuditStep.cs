// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Audit
{
    public class AuditStep : ModelBase, IAuditStep
    {
        public int AuditId { get; set; }
        public int EntityId { get; set; }
        public WizardPageEnum WizardPageType { get; set; }
        public int StepOrder { get; set; }
        public string State { get; set; }
        public string PreviousStepState { get; set; }
        public string WizardPage { get; set; }
        public string WizardMenuText { get; set; }
        public string WizardMenuImageUrl { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedById { get; set; }
        public DateTimeOffset? CompletedOn { get; set; }
        public override bool Equals(object obj)
        {
            return obj is AuditStep auditStep && Id.Equals(auditStep.Id);
        }

        public override int GetHashCode()
        {
            return AuditId.GetHashCode();
        }
    }
}
