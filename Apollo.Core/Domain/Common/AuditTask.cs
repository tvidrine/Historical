// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Common
{
    public class AuditTask : ModelBase, IAuditTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
