// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface IAuditEntity: IHaveId, IHaveAuditData
    {
        string Name { get; set; }
        IList<ILocation> Locations { get; set; }
        ExposureBasisEnum ExposureBasis { get; set; }

        IList<IEntityPrincipal> Principals { get; set; }
        
    }
}