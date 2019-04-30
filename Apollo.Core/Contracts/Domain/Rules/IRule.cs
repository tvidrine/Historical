// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Rules
{
    public interface IRule : IHaveAuditData, IHaveId
    {
        string Name { get; set; }
        string Body { get; set; }
        string GeneratedCode { get; set; }
        bool IsPublished { get; set; }
    }
}
