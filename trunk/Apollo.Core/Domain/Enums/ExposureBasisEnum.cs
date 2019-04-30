// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Domain.Enums
{
    [Flags]
    public enum ExposureBasisEnum
    {
        NotSet = 0x0,
        Admissions = 0x1,
        Area = 0x2,
        Costs = 0x4,
        Gallons = 0x8,
        Other = 0x10,
        Payroll = 0x20,
        Sales = 0x40,
        Units = 0x80
    }
}