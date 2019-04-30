// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/12/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.DomainServices.Validators
{
    public interface IValidator<in T> 
    {
        Task<bool> IsValidAsync(T instance);
        Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = new CancellationToken());
    }
}