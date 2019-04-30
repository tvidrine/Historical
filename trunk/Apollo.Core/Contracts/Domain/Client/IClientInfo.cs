// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/12/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Client
{
    public interface IClientInfo
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}