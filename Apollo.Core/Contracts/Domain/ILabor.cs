// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain
{
    public interface ILabor : IHaveAuditData, IHaveId
    {
        int EntityId { get; set; }
        int AuditId { get; set; }
        LaborTypes LaborType { get; set; }
        string LaborerName { get; set; }
        string LaborDescription { get; set; }
        decimal LaborAmount { get; set; }
        string LaborState { get; set; }
        bool IsLaborOnly { get; set; }
        bool IsLaborInsured { get; set; }
        int ClassCodeId { get; set; }
        decimal AggregateLimit { get; set; }
        ICertificateOfInsurance CertificateOfInsurance { get; set; }
        
    }
}
