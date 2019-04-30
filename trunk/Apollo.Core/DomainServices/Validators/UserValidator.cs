// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using FluentValidation;

namespace Apollo.Core.DomainServices.Validators
{
    public class UserValidator : ValidatorBase<IUser>, IUserValidator
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull();
        }
        public async Task<bool> IsValidAsync(IUser user)
        {
            var validationResult = await ValidateAsync(user);
            return validationResult.IsValid;
        }
    }
}