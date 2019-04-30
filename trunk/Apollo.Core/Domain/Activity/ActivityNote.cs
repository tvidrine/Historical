// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Activity
{
    public class ActivityNote : ModelBase, IActivityNote
    {
        public int AuditId { get; set; }
        public int? EntityId { get; set; }
        public int ActivityTypeId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime ActivityDate { get; set; }
        public int UserId { get; set; }
        public string ActivityDescription { get; set; }
        public int? ToFromId { get; set; }
        public string EmailorPhone { get; set; }
        public int? CallResultId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
