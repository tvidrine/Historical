// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Policies
{
    public class Agent: ModelBase, IAgent
    {
       public IAddress Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
    }
}