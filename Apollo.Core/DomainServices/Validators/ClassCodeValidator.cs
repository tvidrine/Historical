// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;
using FluentValidation;

namespace Apollo.Core.DomainServices.Validators
{
    public class ClassCodeValidator : ValidatorBase<IClassCode>, IClassCodeValidator
    {
        public ClassCodeValidator()
        {
            RuleFor(c => c.AuditType)
                .NotEqual(AuditTypeEnum.NotSet)
                .WithMessage("Audit Type is required.");

            RuleFor(c => c.Code)
                .NotEmpty()
                .WithMessage(@"Class Code is required.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage(@"Description is required.");
        }
        public async Task<bool> IsValidAsync(IClassCode instance)
        {
            var validationResult = await ValidateAsync(instance);
            return validationResult.IsValid;
        }

        
    }
}