// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/21/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Infrastructure.Transforms.BerkleyNet.Models
{
    public class Principal
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string CoveredType { get; set; }
        public string Title { get; set; }
        public string Effective { get; set; }
        public string Expiration { get; set; }
    }
}