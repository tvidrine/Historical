namespace Apollo.Core.Contracts.DomainServices
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}