// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Rule
{
    public class RuleSet : ModelBase, IRuleSet
    {
        public RuleSet()
        {
            Rules = new List<IRule>();
        }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Key { get; set; }
        public string Code { get; set; }
        public byte[] GeneratedAssembly { get; set; }
        public IList<IRule> Rules { get; set; }
    }
}
