// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Identity;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories.v1
{
    public class UserRepositoryV1 : BaseRepository<IUser, UserInfo, UserDto>, IUserRepository
    {
        public UserRepositoryV1(IConnectionFactory connectionFactory, ILogManager loggerManager, UserDto dto) 
            : base(connectionFactory, loggerManager, dto)
        {
        }

        public Task<GetResponse<IUser>> GetByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetResponse<IUser>> GetByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetResponse<IUser>> GetByUserNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResponse> SaveSessionStateAsync(IUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}