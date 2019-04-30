// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/03/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Common;

namespace Apollo.Core.Contracts.Domain.Client
{
    public interface IWpdConfig : IHaveId, IHaveAuditData
    {
        int ClientId { get; }

        WpdDeliveryMethods DeliveryMethod { get; set; }
        DeliverToOptions DeliverTo { get; set; }
        DocumentTypesToChange DocumentTypes { get; set; }
        FileFormats FileFormats { get; set; }
    }
}