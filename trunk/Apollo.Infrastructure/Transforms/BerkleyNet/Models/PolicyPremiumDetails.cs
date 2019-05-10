// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace Apollo.Infrastructure.Transforms.BerkleyNet.Models
{
    [Serializable]
    public class PolicyPremiumDetails
    {
        [XmlArray("ARDDates")]
        [XmlArrayItem("ARDDate")]
        public string[] ArdDates { get; set; }
        [XmlElement("StateExposureDetails")]
        public StateExposureDetail[] StateExposureDetails { get; set; }
    }
}