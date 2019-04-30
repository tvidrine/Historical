// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Payroll
{
    public interface IPayrollLimit : IHaveAuditData, IHaveId
    {
        AuditTypeEnum AuditType { get; set; }
        int ClientId { get; set; }
        int EntityTypeId { get; set; }
        string State { get; set; }
        PayrollLimitEmployeeTypes EmployeeType { get; set; }
        decimal Min { get; set; }
        decimal Max { get; set; }
        DateTimeOffset EffectiveStart { get; set; }
        DateTimeOffset? EffectiveEnd { get; set; }
        IPayrollLimit Copy();
    }
}
