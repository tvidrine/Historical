// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/11/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Domain.Communication;

namespace Apollo.Core.Messages.Results
{
    public interface ICommunicationResult
    {
        bool Succeeded { get; set; }
        IList<CommunicationError> Errors { get;  }
    }
}