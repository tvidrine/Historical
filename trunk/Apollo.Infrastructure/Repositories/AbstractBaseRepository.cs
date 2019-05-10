// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Infrastructure.Factories;

namespace Apollo.Infrastructure.Repositories
{
    public abstract class AbstractBaseRepository
    {
        protected readonly IConnectionFactory ConnectionFactory;
        protected readonly ILogManager LogManager;

        protected AbstractBaseRepository(IConnectionFactory connectionFactory, ILogManager logManager)
        {
            ConnectionFactory = connectionFactory;
            LogManager = logManager;
        }
    }
}