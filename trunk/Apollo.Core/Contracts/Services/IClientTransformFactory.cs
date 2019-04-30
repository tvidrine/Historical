// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Data;

namespace Apollo.Core.Contracts.Services
{
    public interface IClientTransformFactory
    {
        IClientTransform GetTransform(Guid clientKey);
    }
}