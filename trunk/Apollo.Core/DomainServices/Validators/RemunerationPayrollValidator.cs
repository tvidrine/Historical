// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.DomainServices.Validators;

namespace Apollo.Core.DomainServices.Validators
{
    public class RemunerationPayrollValidator : ValidatorBase<IRemunerationPayroll>, IRemunerationPayrollValidator{
        public async Task<bool> IsValidAsync(IRemunerationPayroll instance)
        {
            return await Task.FromResult(true);
        }
    }
}