// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/13/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.WebApi.Contracts;

namespace Apollo.WebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientsDto : IDto<ClientsDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<IClient> Clients { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ClientsDto FromModel<TModel>(TModel model)
        {
            var models = model as IReadOnlyList<IClient>;

            Clients = models;

            return this;
        }
    }
}