// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/11/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Configuration
{
    public interface IEmailConfiguration
    {
        string PopPassword { get; }
        int PopPort { get; }
        string PopServer { get; }
        string PopUsername { get; }
        string SmtpPassword { get; set; }
        int SmtpPort { get; }
        string SmtpServer { get; }
        string SmtpUsername { get; set; }
    }
}