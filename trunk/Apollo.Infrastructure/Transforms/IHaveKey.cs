// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Infrastructure.Transforms
{
    internal interface IHaveKey
    {
        Guid Key { get; set; }
    }
}