// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain
{
    public class Labor : ModelBase, ILabor
    {
        public int EntityId { get; set; }
        public int AuditId { get; set; }
        public LaborTypes LaborType { get; set; }
        public string LaborerName { get; set; }
        public string LaborDescription { get; set; }
        public decimal LaborAmount { get; set; }
        public string LaborState { get; set; }
        public bool IsLaborOnly { get; set; }
        public bool IsLaborInsured { get; set; }
        public int ClassCodeId { get; set; }
        public decimal AggregateLimit { get; set; }
        public ICertificateOfInsurance CertificateOfInsurance { get; set; }
    }
}