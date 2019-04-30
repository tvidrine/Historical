// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Domain.Common;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface ICommonDataApplicationService
    {
        IList<StateInfo> GetStates(bool includeAll = false);

    }
}