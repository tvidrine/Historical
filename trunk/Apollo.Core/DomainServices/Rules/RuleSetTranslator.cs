// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/05/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Core.Base;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Contracts.DomainServices.Rules;
using Apollo.Core.Extensions;

namespace Apollo.Core.DomainServices.Rules
{
    public class RuleSetTranslator : IRuleSetTranslator
    {
        private readonly IBusinessRuleParser _businessRuleParser;

        public RuleSetTranslator(IBusinessRuleParser businessRuleParser)
        {
            _businessRuleParser = businessRuleParser;
        }
        public string Translate(IRuleSet ruleSet, bool forTesting = false)
        {
            var rules = forTesting ? ruleSet.Rules : ruleSet.Rules.Where(r => r.IsPublished);
            var className = GetClassName(ruleSet);

            // Create class based on rule set name
            var code = $@"{GetUsingStatements()}
                    namespace {Constants.RuleNamespace}
                    {{
                        public class {className}
                        {{
                            private readonly Dictionary<string, Func<IToken, Exception>> _actions;

                            public {className}(Dictionary<string, Func<IToken, Exception>> actions)
                            {{
                                _actions = actions;
                            }}

                            {GetMethods(rules)}
                        }}
                    }}";

            return code;
        }

        private string GetMethods(IEnumerable<IRule> rules)
        {
            var sb = new StringBuilder();

            foreach (var rule in rules)
            {
                var response = _businessRuleParser.Translate(rule);
                if (response.IsSuccessful)
                    sb.Append(response.Code);
            }
            return sb.ToString();
        }

        #region Private Methods
        private string GetClassName(IRuleSet ruleSet)
        {
            return ruleSet.Name.RemoveWhitespace();
        }
        private string GetUsingStatements()
        {
            return @"using System;
                     using System.Collections.Generic;
                     using Apollo.Core.Contracts.Workflow;
                     using Apollo.Core.Domain.Audit;";
        }
        #endregion Private Methods
    }
}