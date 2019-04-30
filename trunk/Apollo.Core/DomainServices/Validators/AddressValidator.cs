using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;
using FluentValidation;

namespace Apollo.Core.DomainServices.Validators
{
	public class AddressValidator : ValidatorBase<IAddress>, IAddressValidator
	{
		public AddressValidator()
		{
			RuleFor(a => a.Line1)
				.NotEmpty()
				.NotNull()
			    .WithMessage(@"The address is invalid");

			RuleFor(a => a.City)
				.NotEmpty()
				.NotNull()
			    .WithMessage(@"City is required.");

			RuleFor(a => a.State)
				.NotEmpty()
				.NotNull()
			    .WithMessage(@"State is required");

			RuleFor(a => a.Zipcode)
				.NotEmpty()
				.NotNull()
				.Matches("^[0-9]{5}(?:-[0-9]{4})?$")
			    .WithMessage(@"Invalid zip code."); // Regex for valid zipcodes.

		}

		public async Task<bool> IsValidAsync(IAddress address)
		{
			var validationResult = await ValidateAsync(address);
			return validationResult.IsValid;
		}
    }
}