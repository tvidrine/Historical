// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/12/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.WebApi.Contracts
{
    /// <summary>
    /// Definition for Web API DTO objects
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public interface IDto<out TDto>
    {
        /// <summary>
        /// Populates DTO with model data
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        TDto FromModel<TModel>(TModel model);
    }
}