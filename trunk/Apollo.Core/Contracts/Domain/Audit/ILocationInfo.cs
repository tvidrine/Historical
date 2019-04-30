// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/05/2019
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface ILocationInfo
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}