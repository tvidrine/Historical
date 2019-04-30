// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/22/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Apollo.Core.Domain.Enums
{
    public enum ClientSettingsEnum
    {
        [Description("Not Set")]
        NotSet = 0,
        [Description("Use Custom Payroll Limits")]
        UseCustomPayrollLimits = 1,
        [Description("Use Custom Payroll Remuneration")]
        UseCustomPayrollRemuneration = 2,
        [Description("Use Custom Sales Remuneration")]
        UseCustomSalesRemuneration = 3,
        [Description("Enter subcontractors as one total")]
        UseTotalForAllSubContractors = 4,
        [Description("General Aggregate Limit")]
        GeneralAggregateLimit = 5
    }
}