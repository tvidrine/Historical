using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.DomainServices.Validators;

namespace Apollo.Core.DomainServices.Validators
{
    public class SalesValidator : ValidatorBase<ISales>, ISalesValidator
    {
        public Task<bool> IsValidAsync(ISales instance)
        {
            throw new NotImplementedException();
        }
    }
}