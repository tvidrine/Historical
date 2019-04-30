// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/03/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Common;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Client
{
    public class WpdConfig : ModelBase, IWpdConfig
    {
        public WpdConfig(int clientId):base()
        {
            ClientId = clientId;
        }
        public int ClientId { get; }
        public WpdDeliveryMethods DeliveryMethod { get; set; }
        public DeliverToOptions DeliverTo { get; set; }
        public DocumentTypesToChange DocumentTypes { get; set; }
        public FileFormats FileFormats { get; set; }
    }

    public enum WpdDeliveryMethods
    {
        Email,
        System
    }

    public enum WpdDeliveryOptions
    {
        Bundle,
        
    }

    public enum DeliverToOptions
    {
        Requestor,
        Underwriter
    }

   
    
}