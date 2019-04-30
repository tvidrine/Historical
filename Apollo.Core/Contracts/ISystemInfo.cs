// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/9/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;

namespace Apollo.Core.Contracts
{
    public interface ISystemInfo : IHaveAuditData, IHaveId
    {
        string AppVersion { get; set; }
        DateTimeOffset AppLastUpdated { get; set; }
        string DbSchemaVersion { get; set; }
        DateTimeOffset DbSchemaLastUpdated { get; set; }
        bool ShowMaintenanceMessage { get; set; }
        string MaintenanceMessage { get; set; }
        DateTimeOffset LastUpdated { get; set; }
        string LastUpdatedBy { get; set; }
    }
}
