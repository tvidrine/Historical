// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface IAuditStep : IHaveAuditData, IHaveId
    {
        int AuditId { get; set; }
        int EntityId { get; set; }
        WizardPageEnum WizardPageType { get; set; }
        int StepOrder { get; set; }
        string State { get; set; }
        string PreviousStepState { get; set; }
        string WizardPage { get; set; }
        string WizardMenuText { get; set; }
        string WizardMenuImageUrl { get; set; }
        bool IsCompleted { get; set; }
        int CompletedById { get; set; }
        DateTimeOffset? CompletedOn { get; set; }
    }
}
