// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Domain.Enums
{
    [Flags]
    public enum IntegrationActionTypes
    {
        None = 0x0,
        Export = 0x1,
        Import = 0x2,
        Both = Export | Import
    }
}