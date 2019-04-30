// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Core;
using Apollo.RulesEngine.Contracts;

namespace Apollo.RulesEngine.Models
{
    public class RuleGrammarTerminal : ModelBase, IRuleGrammarTerminal
    {
        public TerminalTypes TerminalType { get; set; }
        public string Keyword { get; set; }
        public string[] SupportingKeywords { get; set; }
        public string TranslateTo { get; set; }
    }
}