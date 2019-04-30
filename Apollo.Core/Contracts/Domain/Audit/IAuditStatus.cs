// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface IAuditStatus : IHaveAuditData, IHaveId
    {
        string Name { get; set; }
        string Description { get; set; }
        int ExecutionOrder { get; set; }
        int SortOrder { get; set; }
        bool IsSystemOnly { get; set; }
    }
}
