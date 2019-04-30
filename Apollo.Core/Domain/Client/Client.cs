// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/06/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Client
{
    public class Client : ModelBase, IClient
    {
        public Client()
        {
            Contacts = new List<IContact>();
        }

        public IAddress Address { get; set; }
        public AuditTypeEnum AuditType { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public IList<IContact> Contacts { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public AuditProcessTypeEnum ProcessType { get; set; }
        public IClientSettings Settings { get; set; }
    }
}