using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Identity;

namespace Apollo.Infrastructure.Models
{
    public class UserDto :DtoBase<IUser, IUserInfo>
    {
        public int AccessFailedCount { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsLocked { get; set; }
        public bool IsLockoutEnabled { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public DateTimeOffset LastAccessedDate { get; set; }
        public DateTimeOffset LastPasswordChangedDate { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string Notes { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        public override IDto FromModel (IUser user)
        {
            Id = user.Id;
            AccessFailedCount = user.AccessFailedCount;
            Email = user.Email;
            IsActive = user.IsActive;
            IsEmailConfirmed = user.EmailConfirmed;
            IsLocked = user.IsLocked;
            IsLockoutEnabled = user.LockoutEnabled;
            IsPhoneNumberConfirmed = user.PhoneNumberConfirmed;
            IsTwoFactorEnabled = user.TwoFactorEnabled;
            LastAccessedDate = user.LastAccessedDate;
            LastPasswordChangedDate = user.LastPasswordChangedDate;
            LockoutEnd = user.LockoutEnd;
            FirstName = user.FirstName;
            MiddleInitial = user.MiddleInitial;
            LastName = user.LastName;
            NormalizedEmail = string.IsNullOrEmpty(user.Email) ? string.Empty : user.Email.ToUpper();
            NormalizedUserName = string.IsNullOrEmpty(user.Email) ? string.Empty : user.Email.ToUpper();
            PasswordHash = user.PasswordHash;
            PhoneNumber = user.PhoneNumber;
            UserName = user.UserName;
            Notes = user.Notes;
            CreatedById = user.CreatedById;
            CreatedOn = user.CreatedOn; // This is being set in the merge statement, but we will go ahead and set it here.
            LastModifiedById = user.LastModifiedById;
            LastModifiedOn = user.LastModifiedOn; // This is being set in the merge statement, but we will go ahead and set it here.

            return this;
        }

        public override IUser ToModel()
        {
            var user = new User
            {
                AccessFailedCount = AccessFailedCount,
                Email = Email,
                EmailConfirmed = IsEmailConfirmed,
                IsActive = IsActive,
                IsLocked = IsLocked,
                LastAccessedDate = LastAccessedDate,
                LockoutEnd = LockoutEnd,
                FirstName = FirstName,
                MiddleInitial = MiddleInitial,
                LastName = LastName,
                LastPasswordChangedDate = LastPasswordChangedDate,
                NormalizedEmail = NormalizedEmail,
                NormalizedUserName = NormalizedUserName,
                PasswordHash = PasswordHash,
                PhoneNumber = PhoneNumber,
                PhoneNumberConfirmed = IsPhoneNumberConfirmed,
                TwoFactorEnabled = IsTwoFactorEnabled,
                Notes = Notes,
                Id = Id,
                CreatedById = CreatedById,
                CreatedOn = CreatedOn,
                LastModifiedById = LastModifiedById,
                LastModifiedOn = LastModifiedOn
            };


            return user;
        }
    }
}