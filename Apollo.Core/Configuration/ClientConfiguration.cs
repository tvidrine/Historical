// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/11/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Configuration
{
    public class ClientConfiguration
    {
        public IntegrationActionTypes ActionToPerform { get; set; }
        public Guid ClientKey { get; set; }
        public string ClientName { get; set; }
        public CommunicationServiceTypes CommunicateUsing { get; set; }
        public ClientDataFormat DataFormat { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public List<string> EmailNotificationRecipients { get; set; }
    }
}