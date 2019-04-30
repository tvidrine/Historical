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
    public class AuditStatus : ModelBase, IAuditStatus
    {
        #region Public Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExecutionOrder { get; set; }
        public int SortOrder { get; set; }
        public bool IsSystemOnly { get; set; }
        #endregion Public Properties
    }
}
