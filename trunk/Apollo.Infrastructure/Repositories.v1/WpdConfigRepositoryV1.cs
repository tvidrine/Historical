// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories.v1
{
    public class WpdConfigRepositoryV1 : BaseRepository<IWpdConfig, IWpdConfig, WpdConfigDto>, IWpdConfigRepository
    {
        public WpdConfigRepositoryV1(IConnectionFactory connectionFactory, ILogManager loggerManager, WpdConfigDto dto) 
            : base(connectionFactory, loggerManager, dto)
        {
        }
    }
}