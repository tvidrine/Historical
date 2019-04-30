// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Payroll
{
    public interface IRemunerationPayroll : IHaveAuditData, IHaveId
    {
        AuditTypeEnum AuditType { get; set; }
        string State { get; set; }
        int ClientId { get; set; }
        bool IncludeWage { get; set; }
        bool IncludeCommission { get; set; }
        bool IncludeBonus { get; set; }
        bool IncludeHoliday { get; set; }
        bool IncludeVacation { get; set; }
        bool IncludeSickPay { get; set; }
        bool IncludeTips { get; set; }
        bool IncludeOvertime { get; set; }
        bool IncludeSeverance { get; set; }
        bool IncludeSection125 { get; set; }
        DateTimeOffset EffectiveStart { get; set; }
        DateTimeOffset? EffectiveEnd { get; set; }
    }
}
