// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/14/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Configuration
{
    public class FtpConfiguration
    {
        public string ClientFolder { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
    }
}