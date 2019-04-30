using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IUser>>> GetAllAsync();
        Task<GetResponse<IUser>> GetByEmailAsync(string email);
        Task<GetResponse<IUser>> GetByIdAsync(int id);
        Task<GetResponse<IUser>> GetByUserIdAsync(string userId);
        Task<GetResponse<IUser>> GetByUserNameAsync(string userName);
        Task<SaveResponse<IUser>> SaveAsync(IUser user);
        Task<SaveResponse> SaveSessionStateAsync(IUser user);
    }
}