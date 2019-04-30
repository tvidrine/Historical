// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/22/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Client;

namespace Apollo.Core.Contracts.Domain.Policies
{
    public interface IPolicy : IHaveAuditData, IHaveId
    {
        IClient Client { get; set; }
        DateTimeOffset EffectiveEnd { get; set; }
        DateTimeOffset EffectiveStart { get; set; }
        IList<IAuditEntity> Entities { get; set; }
        int PolicyHolderId { get; set; }
        string PolicyNumber { get; set; }
        IList<IAudit> Audits { get; set; }
        IAgent Agent { get; set; }
        IAddress Address { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string InsuredName { get; set; }
        string CompanyName { get; set; }
    }
}