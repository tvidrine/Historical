// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Audit
{
    public class AuditEntity : ModelBase, IAuditEntity
    {
        public string Name { get; set; }
        public IList<ILocation> Locations { get; set; }
        public ExposureBasisEnum ExposureBasis { get; set; }
        public IList<IEntityPrincipal> Principals { get; set; }
    }
}
