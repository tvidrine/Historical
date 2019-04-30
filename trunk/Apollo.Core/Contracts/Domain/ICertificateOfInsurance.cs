// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;

namespace Apollo.Core.Contracts.Domain
{
    public interface ICertificateOfInsurance : IHaveAuditData, IHaveId
    {
        int LaborId { get; set; }
        string CarrierName { get; set; }
        string PolicyNumber { get; set; }
        DateTime? PolicyStart { get; set; }
        DateTime? PolicyEnd { get; set; }
        decimal AggregateLimit { get; set; }
        IAuditUpload File { get; set; }
        
    }
}
