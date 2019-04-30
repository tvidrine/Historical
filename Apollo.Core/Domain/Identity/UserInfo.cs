// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Domain.Identity
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
    }
}