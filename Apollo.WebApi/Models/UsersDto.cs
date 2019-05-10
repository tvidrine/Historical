// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain;
using Apollo.WebApi.Contracts;

namespace Apollo.WebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersDto : IDto<UsersDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<IUser> Users { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public UsersDto FromModel<TModel>(TModel model)
        {
            var models = model as IReadOnlyList<IUser>;

            Users = models;

            return this;
        }
    }
}