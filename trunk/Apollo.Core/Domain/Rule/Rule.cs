// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Rule
{
    public class Rule : ModelBase, IRule
    {
        public int RuleSetId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string GeneratedCode { get; set; }
        public bool IsPublished { get; set; }
    }
}
