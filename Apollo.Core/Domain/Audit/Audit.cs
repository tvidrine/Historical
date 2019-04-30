// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/16/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Extensions;

namespace Apollo.Core.Domain.Audit
{
    
    public class Audit : ModelBase, IAudit
    {
        public DateTimeOffset AssignedOn { get; set; }
        public int AssignmentNumber { get; set; }
        public string AuditPeriod { get; set; }
        public int AuditorId { get; set; }
        public AuditStatuses AuditStatus { get; set; }
        
        // Term periods not on the first of the month should be adjusted to the first day of whichever month is within 15 days.
        // Less than one year periods should use the same period.
        public DateTimeOffset AuditPeriodStartDate
        {
            get => (Policy.EffectiveEnd - Policy.EffectiveStart).Days < 365
                ? Policy.EffectiveStart
                : Policy.EffectiveStart.StartOfMonth();
            set => throw new NotImplementedException();
        }

        public DateTimeOffset AuditPeriodEndDate
        {
            get => (Policy.EffectiveEnd - Policy.EffectiveStart).Days < 365
                ? Policy.EffectiveEnd
                : Policy.EffectiveEnd.StartOfMonth().AddDays(-1);
            set => throw new NotImplementedException();
        }
        public DateTimeOffset CompletedDate { get; set; }
        public int InvoiceId { get; set; }
        public IPolicy Policy { get; set; }
        public IUser RequestedBy { get; set; }
        public DateTimeOffset RequestedOn { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public AuditTypeEnum AuditType { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public AuditMethods AuditMethod { get; set; }
        public ExposureBasisEnum AuditExposureBasis { get; set; }
        public AuditFrequencyEnum AuditFrequency { get; set; }
        public bool CheckedForQualityControl { get; set; }
        public bool ByLocation { get; set; }
        public IReadOnlyList<IClassCode> StandardExceptions { get; set; }
    }
}