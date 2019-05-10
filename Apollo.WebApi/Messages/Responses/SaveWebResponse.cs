﻿// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/13/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.WebApi.Contracts;

namespace Apollo.WebApi.Messages.Responses
{
    /// <summary>
    /// A response generated by a Save end point
    /// </summary>
    /// <inheritdoc/>
    /// <typeparam name="T"></typeparam>
    public class SaveWebResponse<T> : BaseWebResponse<T> where T : IDto<T>
    {
        
    }

    /// <summary>
    /// A response generated by a Save end point
    /// </summary>
    /// <inheritdoc/>
    public class SaveWebResponse : BaseWebResponse
    {

    }
}