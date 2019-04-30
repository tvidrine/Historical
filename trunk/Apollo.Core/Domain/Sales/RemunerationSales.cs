// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Sales
{
    public class RemunerationSales : ModelBase, IRemunerationSales
    {
        public AuditTypeEnum AuditType { get; set; }
        public string State { get; set; }
        public int ClientId { get; set; }
        public bool IncludeSales { get; set; }
        public bool IncludeReturns { get; set; }
        public bool IncludeFreightShipping { get; set; }
        public bool IncludeSalesTax { get; set; }
        public bool IncludeLotterySales { get; set; }
        public bool IncludeInterCompanySales { get; set; }
        public DateTimeOffset EffectiveStart { get; set; }
        public DateTimeOffset? EffectiveEnd { get; set; }
    }
}
