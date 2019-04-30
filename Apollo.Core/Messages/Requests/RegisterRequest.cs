using Apollo.Core.Contracts.Domain;

namespace Apollo.Core.Messages.Requests
{
    public class RegisterRequest
    {
        public IIdentity Identity { get; set; }
       
    }
}