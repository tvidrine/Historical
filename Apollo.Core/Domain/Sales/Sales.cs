// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Sales
{
    public class Sales : ModelBase, ISales
    {
        public int AuditId { get; set; }
        public int EntityId { get; set; }
        public int LocationId { get; set; }
        public SalesPeriodType PeriodType { get; set; }
        public DateTimeOffset PeriodStart { get; set; }
        public DateTimeOffset PeriodEnd { get; set; }
        public decimal GrossSales { get; set; }
        public decimal GrossSalesVerified { get; set; }
        public decimal AlcoholSales { get; set; }
        public decimal Freight { get; set; }
        public decimal IntercompanySales { get; set; }
        public decimal LotterSales { get; set; }
        public decimal Returns { get; set; }
        public decimal SalesTax { get; set; }
    }
}
