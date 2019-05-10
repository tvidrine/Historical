// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Client;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;
using Newtonsoft.Json;

namespace Apollo.Infrastructure.Repositories.v1
{
    public class ClientRepositoryV1 : BaseRepository<IClient, ClientInfo, ClientDto>, IClientRepository
    {
        public ClientRepositoryV1(IConnectionFactory connectionFactory, ILogManager loggerManager, ClientDto dto) 
            : base(connectionFactory, loggerManager, dto)
        {
        }

        public async Task<GetResponse<IReadOnlyList<ClientConfiguration>>> GetConfigurationsAsync()
        {
            // For v1, we will use a Json file to contain the configurations
            return await Task.Run(() =>
            {
                var response = new GetResponse<IReadOnlyList<ClientConfiguration>>();

                try
                {
                    var filename = Path.Combine(AppContext.BaseDirectory, "client_configuration.json");
                    

                    using (var file = File.OpenText(filename))
                    {
                        var serializer = new JsonSerializer();
                        response.Content = (List<ClientConfiguration>)serializer.Deserialize(file, typeof(List<ClientConfiguration>));
                    }

                }
                catch (Exception e)
                {
                    response.AddError(e);
                    LogManager.LogError(e, "ClientRepositoryV1.GetConfigurationsAsync");
                }
                return response;
            });
            
           
        }
    }
}