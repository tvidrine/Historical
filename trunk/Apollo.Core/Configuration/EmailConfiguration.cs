// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/11/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Configuration;

namespace Apollo.Core.Configuration
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string PopPassword { get; set; }
        public int PopPort { get; set; }
        public string PopServer { get; set; }
        public string PopUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUsername { get; set; }
        public string InfoAddress { get; set; }
    }
}