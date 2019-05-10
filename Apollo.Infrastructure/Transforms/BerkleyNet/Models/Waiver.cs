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
    public class Waiver
    {
        [XmlElement("WaiverHolder")]
        public string Holder { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("WaiverClass")]
        public string Class { get; set; }
        [XmlElement("EstimatedPayroll")]
        public decimal EstimatedPayroll { get; set; }
    }
}