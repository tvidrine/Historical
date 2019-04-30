using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IUserApplicationService
    {
        Task<ICreateResponse<IUser>> CreateAsync();
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IUser>> FindByEmailAsync(string email);
        Task<GetResponse<IUser>> FindByIdAsync(string userId);
        Task<GetResponse<IReadOnlyList<IUser>>> GetAllAsync();
        Task<GetResponse<IUser>> GetAsync(int id);
        Task<SaveResponse> RegisterUserAsync(RegisterRequest request);
        Task<SaveResponse<IReadOnlyList<IUser>>> SaveAllAsync(IReadOnlyList<IUser> users);
        Task<SaveResponse> SaveAsync(IUser user);
        Task<SaveResponse> SaveSessionStateAsync(IUser user);
    }
}