// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Policies
{
    public class Claim : ModelBase
    {
        public string InjuredName { get; set; } // Change later
        public DateTimeOffset OccuredOn { get; set; }

    }
}