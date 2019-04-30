// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Policies
{
    public class Policy : ModelBase, IPolicy
    {
        public IList<IAudit> Audits { get; set; }
        public IAgent Agent { get; set; }
        public IAddress Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InsuredName { get; set; }
        public string CompanyName { get; set; }
        public IClient Client { get; set; }
        public DateTimeOffset EffectiveEnd { get; set; }
        public DateTimeOffset EffectiveStart { get; set; }
        public IList<IAuditEntity> Entities { get; set; }
        public int PolicyAgentId { get; set; }
        public int PolicyHolderId { get; set; }
        public string PolicyNumber { get; set; }

    }
}