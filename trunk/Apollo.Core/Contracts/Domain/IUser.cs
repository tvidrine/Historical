// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Contracts.Domain
{
    public interface IUser : IHaveId, IHaveAuditData
    {
        int AccessFailedCount { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        bool IsActive { get; set; }
        bool IsLocked { get; set; }
        DateTimeOffset LastAccessedDate { get; set; }
        DateTimeOffset LastPasswordChangedDate { get; set; }
        bool LockoutEnabled { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        string FirstName { get; set; }
        string MiddleInitial { get; set; }
        string LastName { get; set; }
        string NormalizedEmail { get; set; }
        string NormalizedUserName { get; set; }
        string Notes { get; set; }
        string PasswordHash { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        int Pin { get; set; }
        string PolicyNumber { get; set; }
        bool TwoFactorEnabled { get; set; }
        // For now, username is the email address
        string UserName { get; }
    }
}