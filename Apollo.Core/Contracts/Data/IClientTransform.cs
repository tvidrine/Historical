// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Communication;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.Contracts.Data
{
    public interface IClientTransform
    {
        TransformResult<IList<Packet>> From(IReadOnlyList<IAudit> audits, ClientConfiguration clientConfiguration);
        TransformResult<IReadOnlyList<IAudit>> To(IReadOnlyList<Packet> packets, ClientConfiguration clientConfiguration);
    }
}