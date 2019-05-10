// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Identity;
using Apollo.WebApi.Contracts;

namespace Apollo.WebApi.Models
{
    /// <summary>
    /// A DTO for a user domain model
    /// </summary>
    public class UserDto : IDto<UserDto>
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int CreatedById { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public int Id { get; set; }
        public int LastModifiedById { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserDto FromModel<TModel>(TModel model)
        {
            if (model is IUser user)
            {
                FirstName = user.FirstName;
                MiddleInitial = user.MiddleInitial;
                LastName = user.LastName;
                Email = user.Email;
                IsActive = user.IsActive;
                IsLocked = user.IsLocked;
                Id = user.Id;
                Notes = user.Notes;
                CreatedById = user.CreatedById;
                CreatedOn = user.CreatedOn;
                LastModifiedById = user.LastModifiedById;
                LastModifiedOn = user.LastModifiedOn;
            }

            return this;
        }

        public IUser ToModel()
        {
            var user = new User
            {
                Email = Email,
                FirstName = FirstName,
                MiddleInitial = MiddleInitial,
                LastName = LastName,
                IsActive = IsActive,
                IsLocked = IsLocked,
                Id = Id,
                Notes = Notes,
                CreatedById = CreatedById,
                CreatedOn = CreatedOn,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById
            };

            return user;
        }
    }
}