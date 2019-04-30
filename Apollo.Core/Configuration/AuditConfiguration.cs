using Apollo.Core.Contracts.Configuration;

namespace Apollo.Core.Configuration
{
    public class  AuditConfiguration : IAuditConfiguration
    {
        public string AuditDataConnection { get; set; }
        public bool ShowMaintenanceBanner { get; set; }
        public string MaintenanceBannerMessage { get; set; }
        public EmailConfiguration EmailConfiguration { get; set; }
        public FtpConfiguration FtpConfiguration { get; set; }
        public LegacyConfiguration LegacyConfiguration { get; set; }
        public ShareFileConfiguration ShareFileConfiguration { get; set; }
    }
}