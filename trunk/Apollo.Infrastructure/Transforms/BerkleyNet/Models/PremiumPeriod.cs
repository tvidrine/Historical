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
    public class PremiumPeriod
    {
        [XmlElement("PeriodperiodBeginDate")]
        public string BeginDate { get; set; }
        [XmlElement("PeriodperiodEndDate")]
        public string EndDate { get; set; }
        [XmlElement("Locations")]
        public Location[] Locations { get; set; }
    }
}