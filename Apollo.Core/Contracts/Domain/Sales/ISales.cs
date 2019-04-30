// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Sales
{
    public interface ISales : IHaveAuditData, IHaveId
    {
        int AuditId { get; set; }
        int EntityId { get; set; }
        int LocationId { get; set; }
        SalesPeriodType PeriodType { get; set; }
        DateTimeOffset PeriodStart { get; set; }
        DateTimeOffset PeriodEnd { get; set; }
        decimal GrossSales { get; set; }
        decimal AlcoholSales { get; set; }
        decimal Freight { get; set; }
        decimal IntercompanySales { get; set; }
        decimal LotterSales { get; set; }
        decimal Returns { get; set; }
        decimal SalesTax { get; set; }
        
    }
}
