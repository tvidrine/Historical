// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;

namespace Apollo.RulesEngine.Contracts
{
    public interface IRuleGrammarTerminal : IHaveAuditData, IHaveId
    {
        TerminalTypes TerminalType { get; set; }
        string Keyword { get; set; }
        string[] SupportingKeywords { get; set; }
        string TranslateTo { get; set; }
    }
}