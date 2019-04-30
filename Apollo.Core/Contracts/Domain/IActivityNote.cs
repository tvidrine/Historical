// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Contracts.Domain
{
    public interface IActivityNote : IHaveAuditData, IHaveId
    {
        int AuditId { get; set; }
        int? EntityId { get; set; }
        int ActivityTypeId { get; set; }
        int ActionTypeId { get; set; }
        DateTime ActivityDate { get; set; }
        int UserId { get; set; }
        string ActivityDescription { get; set; }
        int? ToFromId { get; set; }
        string EmailorPhone { get; set; }
        int? CallResultId { get; set; }
        bool IsCompleted { get; set; }
        
    }
}
