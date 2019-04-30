using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Domain.Common;
using FluentValidation;

namespace Apollo.Core.DomainServices.Validators
{
    public class ContactValidator : ValidatorBase<IContact>, IContactValidator
    {
        public ContactValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(@"Contact name is required.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$"); // Valid US phone numbers

            RuleFor(c => c.ContactType)
                .NotEqual(ContactTypeEnum.NotSet);
        }

        public async Task<bool> IsValidAsync(IContact contact)
        {
            var validationResult = await ValidateAsync(contact);

            return validationResult.IsValid;
        }
    }
}