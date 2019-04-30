// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/12/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using FluentValidation.Results;

namespace Apollo.Core.Contracts.DomainServices.Validators
{
    public static class ValidationResultExt
    {
        public static ValidationResult From(this ValidationResult result, IList<ValidationFailure> errors)
        {
            return new ValidationResult(errors);
        }
    }
}