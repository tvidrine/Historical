// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/05/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Audit;

namespace Apollo.Core.Domain.Audit
{
    public class LocationInfo : ILocationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}