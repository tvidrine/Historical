// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/11/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using FluentValidation.Results;

namespace Apollo.Core.Base
{
    public class Failure
    {
        public Failure(string message)
        {
            Message = message;
        }

        public Failure(Exception exception)
            :this(exception.Message)
        {
            
        }

       
        public Failure(ValidationFailure failure)
            :this(failure.ErrorMessage)
        {
            
        }

        public string Message { get; }
    }
}