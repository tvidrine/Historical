// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/22/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Messages.Requests
{
    public struct AuditRequest
    {
        public AuditRequest(string policyNumber, string effectiveDate, string expiryDate)
        {
            PolicyNumber = policyNumber;
            EffectiveDate = DateTime.Parse(effectiveDate);
            ExpiryDate = DateTime.Parse(expiryDate);
        }

        public string PolicyNumber { get;  }
        public DateTime EffectiveDate { get;  }
        public DateTime ExpiryDate { get;  }
    }
}