// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/9/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain
{
    public class SystemInfo : ModelBase, ISystemInfo
    {
        public string AppVersion { get; set; }
        public DateTimeOffset AppLastUpdated { get; set; }
        public string DbSchemaVersion { get; set; }
        public DateTimeOffset DbSchemaLastUpdated { get; set; }
        public bool ShowMaintenanceMessage { get; set; }
        public string MaintenanceMessage { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
