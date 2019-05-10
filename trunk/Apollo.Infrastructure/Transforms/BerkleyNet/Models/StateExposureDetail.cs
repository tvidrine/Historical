// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace Apollo.Infrastructure.Transforms.BerkleyNet.Models
{

    [Serializable]
    public class StateExposureDetail
    {
        [XmlElement("STATECODE")]
        public string StateCode { get; set; }
        [XmlElement("PremiumPeriodDetails")]
        public PremiumPeriod[] PremiumPeriodDetails { get; set; }
    }
}