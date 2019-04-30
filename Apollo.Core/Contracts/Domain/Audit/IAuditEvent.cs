// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface IAuditEvent : IHaveAuditData, IHaveId
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
