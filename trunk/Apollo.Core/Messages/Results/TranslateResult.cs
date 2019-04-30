// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Messages.Results
{
    public class TranslateResult
    {
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
        public string Result => IsSuccessful ? Code : ErrorMessage;
        public bool IsSuccessful => string.IsNullOrEmpty(ErrorMessage);

        public void Join(TranslateResult result)
        {
            if (!result.IsSuccessful)
            {
                ErrorMessage = IsSuccessful ? 
                    result.ErrorMessage : 
                    $@"{ErrorMessage}{Environment.NewLine}{result.ErrorMessage}";
            }
        }
    }
}