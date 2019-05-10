// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories.v1
{
    public class ContactRepositoryV1 : BaseRepository<IContact, IContact, ContactDto>, IContactRepository
    {
        public ContactRepositoryV1(IConnectionFactory connectionFactory, ILogManager loggerManager, ContactDto dto) 
            : base(connectionFactory, loggerManager, dto)
        {
        }
    }
}