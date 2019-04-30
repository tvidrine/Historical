// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Messages.Results
{
    public class TransformResult<T> : BaseResponse
    {
        public T Content { get; set; }
    }
}