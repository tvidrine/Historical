// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Sales
{
    public interface IRemunerationSales : IHaveAuditData, IHaveId
    {
        AuditTypeEnum AuditType { get; set; }
        string State { get; set; }
        int ClientId { get; set; }
        bool IncludeSales { get; set; }
        bool IncludeReturns { get; set; }
        bool IncludeFreightShipping { get; set; }
        bool IncludeSalesTax { get; set; }
        bool IncludeLotterySales { get; set; }
        bool IncludeInterCompanySales { get; set; }
        DateTimeOffset EffectiveStart { get; set; }
        DateTimeOffset? EffectiveEnd { get; set; }
    }
}
