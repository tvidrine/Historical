using System.Threading.Tasks;

namespace Apollo.Core.Contracts.Providers
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}