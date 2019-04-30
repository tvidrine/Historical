using Apollo.Core.Domain.Identity;

namespace Apollo.Core.Messages.Responses
{
    public class LoginResponse :BaseResponse
    {
        public bool IsValidUser { get; set; }
        public User User { get; set; }
    }
}