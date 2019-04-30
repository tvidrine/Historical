using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Identity
{
    public class User : ModelBase, IUser
    {
        public int AccessFailedCount { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public DateTimeOffset LastAccessedDate { get; set; }
        public DateTimeOffset LastPasswordChangedDate { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string Notes { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int Pin { get; set; }
        public string PolicyNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }

        // For now, the username is the email address.
        public string UserName => Email;
    }
}