using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;
using FluentValidation;

namespace Apollo.Core.DomainServices.Validators
{
    public class ClientValidator : ValidatorBase<IClient>, IClientValidator
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
		        .NotNull()
                .WithMessage(@"Client name is required.");

	        RuleFor(c => c.Address)
		        .NotNull()
	            .WithMessage("A client must have an address.");

	        RuleFor(c => c.AuditType)
		        .NotEqual(AuditTypeEnum.NotSet)
	            .WithMessage(@"The audit type must be set.");

	        RuleFor(c => c.ProcessType)
		        .NotEqual(AuditProcessTypeEnum.NotSet)
	            .WithMessage(@"The process type must be set.");

        }
        public async Task<bool> IsValidAsync(IClient client)
        {
            var validationResult = await ValidateAsync(client);
            return validationResult.IsValid;
        }
    }
}