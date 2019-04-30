// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Apollo.Core.Contracts.Domain.Rules
{
    public interface IRuleSet : IHaveAuditData, IHaveId
    {
        string Name { get; set; }
        int CategoryId { get; set; }
        string Key { get; set; }
        string Code { get; set; }
        byte[] GeneratedAssembly { get; set; }
        IList<IRule> Rules { get; set; }
    }
}
