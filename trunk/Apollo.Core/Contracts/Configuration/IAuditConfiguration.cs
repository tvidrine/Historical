using Apollo.Core.Configuration;

namespace Apollo.Core.Contracts.Configuration
{
    public interface IAuditConfiguration
    {
        string AuditDataConnection { get; set; }
        
        EmailConfiguration EmailConfiguration { get; set; }
        FtpConfiguration FtpConfiguration { get; set; }
        LegacyConfiguration LegacyConfiguration { get; set; }
        ShareFileConfiguration ShareFileConfiguration { get; set; }
    }
}