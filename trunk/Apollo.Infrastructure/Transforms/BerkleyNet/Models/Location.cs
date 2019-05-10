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
    public class Location
    {
        [XmlElement("LocationNo")]
        public string Number { get; set; }
        [XmlElement("LocationName")]
        public string Name { get; set; }
        [XmlElement("RiskLocation")]
        public string RiskLocation { get; set; }
        [XmlElement("ExposureDetails")]
        public ExposureDetail[] ExposureDetails { get; set; }
    }
}