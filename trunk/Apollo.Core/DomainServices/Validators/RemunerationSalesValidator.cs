// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.DomainServices.Validators;

namespace Apollo.Core.DomainServices.Validators
{
    public class RemunerationSalesValidator : ValidatorBase<IRemunerationSales>, IRemunerationSalesValidator
    {
        public async Task<bool> IsValidAsync(IRemunerationSales instance)
        {
            return await Task.FromResult(true);
        }
       
    }
}