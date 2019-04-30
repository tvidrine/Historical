// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/7/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.Domain.Payroll;

namespace Apollo.Core.Contracts.Domain.Policies
{
    public interface ILocation : IHaveAuditData, IHaveId
    {
        int EntityId { get; set; }
        string Name { get; set; }
        IAddress MainAddress { get; set; }
        string Phone { get; set; }
        bool IncludeAllEmployeesForExposure { get; set; }
        IList<IClassCode> ClassCodes { get; set; }
        IList<IPayroll> Payroll { get; set; }
    }
}
