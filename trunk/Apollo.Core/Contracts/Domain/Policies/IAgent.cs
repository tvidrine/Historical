// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/30/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Policies
{
    public interface IAgent: IHaveAuditData, IHaveId
    {
        IAddress Address { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string Company { get; set; }
    }
}