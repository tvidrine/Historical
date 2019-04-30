// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.DomainServices.Validators;

namespace Apollo.Core.DomainServices.Validators
{
    public class AuditStepValidator : ValidatorBase<IAuditStep>, IAuditStepValidator
    {
        public Task<bool> IsValidAsync(IAuditStep instance)
        {
            throw new System.NotImplementedException();
        }
    }
}