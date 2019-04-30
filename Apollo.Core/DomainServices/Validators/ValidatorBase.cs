using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Apollo.Core.DomainServices.Validators
{
	public abstract class ValidatorBase<T> : AbstractValidator<T>
	{
	    public Task<ValidationResult> ValidateAsync(T instance)
	    {
	        return base.ValidateAsync(instance);
	    }
    }
}