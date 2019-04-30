// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/12/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Client;

namespace Apollo.Core.Domain.Client
{
    public class ClientInfo : IClientInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}