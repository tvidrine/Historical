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
    public class ExposureDetail
    {
        [XmlElement("Exposure")]
        public string Exposure { get; set; }
        [XmlElement("RatingType")]
        public string RatingType { get; set; }
        [XmlElement("Rate")]
        public string Rate { get; set; }
        [XmlElement("Amount")]
        public string Amount { get; set; }
        [XmlElement("ExposureDetailNo")]
        public string ExposureDetailNumber { get; set; }
        [XmlElement("ClassCode")]
        public string ClassCode { get; set; }
        [XmlElement("Classification")]
        public string Classification { get; set; }
        [XmlElement("ClassificationType")]
        public string ClassificationType { get; set; }
        [XmlElement("ExposureEffDt")]
        public string ExposureEffectiveDate { get; set; }
        [XmlElement("ExposureExpDt")]
        public string ExposureExpirationDate { get; set; }
    }
}