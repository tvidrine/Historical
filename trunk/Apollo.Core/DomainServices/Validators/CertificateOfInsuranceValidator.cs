// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.DomainServices.Validators;


namespace Apollo.Core.DomainServices.Validators
{
    public class CertificateOfInsuranceValidator : ValidatorBase<ICertificateOfInsurance>, ICertificateOfInsuranceValidator
    {
        public async Task<bool> IsValidAsync(ICertificateOfInsurance instance)
        {
            var validationResult = await ValidateAsync(instance);
            return validationResult.IsValid;
        }
    }
}