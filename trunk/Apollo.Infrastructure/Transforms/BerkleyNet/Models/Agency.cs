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
    public class Agency
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Phone")]
        public string Phone { get; set; }
        [XmlElement("MailingAddress")]
        public Address MailingAddress { get; set; }
    }
}