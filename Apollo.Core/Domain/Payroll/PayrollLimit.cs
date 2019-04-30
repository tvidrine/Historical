// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Payroll
{
    public class PayrollLimit : ModelBase, IPayrollLimit
    {
        public AuditTypeEnum AuditType { get; set; }
        public int ClientId { get; set; }
        public int EntityTypeId { get; set; }
        public string State { get; set; }
        public PayrollLimitEmployeeTypes EmployeeType { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public DateTimeOffset EffectiveStart { get; set; }
        public DateTimeOffset? EffectiveEnd { get; set; }
        public IPayrollLimit Copy()
        {
            return new PayrollLimit
            {
                ClientId = ClientId,
                EntityTypeId = EntityTypeId,
                State = State,
                EmployeeType = EmployeeType,
                Min = Min,
                Max = Max,
                EffectiveStart = EffectiveStart,
                EffectiveEnd = EffectiveEnd
            };
        }
    }
}
