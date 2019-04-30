// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.DomainServices.Validators;
using FluentValidation;

namespace Apollo.Core.DomainServices.Validators
{
    public class PayrollLimitValidator : ValidatorBase<IPayrollLimit>, IPayrollLimitValidator
    {
        public PayrollLimitValidator()
        {
            // Effective Start must be less than Effective end
            RuleFor(p => p.EffectiveStart)
                .LessThan(x => x.EffectiveEnd)
                .When(x => x.EffectiveEnd.HasValue)
                .WithMessage("Effect start date must be less than effective end date");

            RuleFor(p => p.Min)
                .LessThanOrEqualTo(p => p.Max)
                .WithMessage("Mininum value must be less than Max value");
        }
        public async Task<bool> IsValidAsync(IPayrollLimit instance)
        {
            var validationResult = await ValidateAsync(instance);
            return validationResult.IsValid;
        }
    }
}