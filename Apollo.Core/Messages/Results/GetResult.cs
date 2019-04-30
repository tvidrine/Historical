// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Domain.Communication;

namespace Apollo.Core.Messages.Results
{
    public class GetResult<TPacket> : ICommunicationResult
    {
        public GetResult()
        {
            Errors = new List<CommunicationError>();
        }

        public TPacket Content { get; set; }
        public bool Succeeded { get; set; }
        public IList<CommunicationError> Errors { get; }
    }
}