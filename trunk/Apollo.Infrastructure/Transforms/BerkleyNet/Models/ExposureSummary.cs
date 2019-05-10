// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Infrastructure.Transforms.BerkleyNet.Models
{
    public class ExposureSummary
    {
        public int GrossTotal { get; set; }
        public int AdjustmentTotal { get; set; }
        public int GrandTotal { get; set; }
    }
}