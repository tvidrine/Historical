// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/02/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.Contracts
{
    public interface IBusinessRuleParser
    {
        TranslateResult Translate(IRule rule);
    }
}