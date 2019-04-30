// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Payroll
{
    public class RemunerationPayroll : ModelBase, IRemunerationPayroll
    {
        public AuditTypeEnum AuditType { get; set; }
        public string State { get; set; }
        public int ClientId { get; set; }
        public bool IncludeWage { get; set; }
        public bool IncludeCommission { get; set; }
        public bool IncludeBonus { get; set; }
        public bool IncludeHoliday { get; set; }
        public bool IncludeVacation { get; set; }
        public bool IncludeSickPay { get; set; }
        public bool IncludeTips { get; set; }
        public bool IncludeOvertime { get; set; }
        public bool IncludeSeverance { get; set; }
        public bool IncludeSection125 { get; set; }
        public DateTimeOffset EffectiveStart { get; set; }
        public DateTimeOffset? EffectiveEnd { get; set; }
    }
}
