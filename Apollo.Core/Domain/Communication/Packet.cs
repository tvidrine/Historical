// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/11/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Domain.Communication
{
    public class Packet
    {
        public byte[] Data { get; set; }
       
        public string Filename { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public string Topic { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}