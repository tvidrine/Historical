using System.Threading.Tasks;

namespace Apollo.Core.Contracts.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}