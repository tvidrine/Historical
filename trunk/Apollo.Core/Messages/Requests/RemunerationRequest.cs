// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Messages.Requests
{
    public class RemunerationRequest
    {
        public int ClientId { get; set; }
        public string State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}