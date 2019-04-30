// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface IAudit : IHaveAuditData, IHaveId
    {
        DateTimeOffset AssignedOn { get; set; }
        int AssignmentNumber { get; set; }
        string AuditPeriod { get; set; }
        int AuditorId { get; set; }
        AuditStatuses AuditStatus { get; set; }
        DateTimeOffset AuditPeriodStartDate { get; set; }
        DateTimeOffset AuditPeriodEndDate { get; set; }
        DateTimeOffset CompletedDate { get; set; }
        int InvoiceId { get; set; }
        IPolicy Policy { get; set; }
        IUser RequestedBy { get; set; }
        DateTimeOffset RequestedOn { get; set; }
        DateTimeOffset StartDate { get; set; }
        AuditTypeEnum AuditType { get; set; }
        DateTimeOffset DueDate { get; set; }
        AuditMethods AuditMethod { get; set; }
        ExposureBasisEnum AuditExposureBasis { get; set; }
        AuditFrequencyEnum AuditFrequency { get; set; }
        bool CheckedForQualityControl { get; set; }
        bool ByLocation { get; set; }
        IReadOnlyList<IClassCode> StandardExceptions { get; set; }
    }

}