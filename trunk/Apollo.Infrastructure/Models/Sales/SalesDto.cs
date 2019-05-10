// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/14/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models.Sales
{
    public class SalesDto : DtoBase<ISales, ISales>
    {
        #region Public Properties
        public int AuditId { get; set; }
        public int EntityId { get; set; }
        public int LocationId { get; set; }
        public int PeriodType { get; set; }
        public DateTimeOffset PeriodStart { get; set; }
        public DateTimeOffset PeriodEnd { get; set; }
        public decimal GrossSales { get; set; }
        public decimal AlcoholSales { get; set; }
        public decimal Freight { get; set; }
        public decimal IntercompanySales { get; set; }
        public decimal LotterSales { get; set; }
        public decimal Returns { get; set; }
        public decimal SalesTax { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(ISales model)
        {
            Id = model.Id;
            AuditId = model.AuditId;
            EntityId = model.EntityId;
            LocationId = model.LocationId;
            PeriodType = (int) model.PeriodType;
            PeriodStart = model.PeriodStart;
            PeriodEnd = model.PeriodEnd;
            GrossSales = model.GrossSales;
            AlcoholSales = model.AlcoholSales;
            Freight = model.Freight;
            IntercompanySales = model.IntercompanySales;
            LotterSales = model.LotterSales;
            Returns = model.Returns;
            SalesTax = model.SalesTax;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override ISales ToModel()
        {
            var model = new Core.Domain.Sales.Sales
            {
                Id = Id,
                AuditId = AuditId,
                EntityId = EntityId,
                LocationId = LocationId,
                PeriodType = (SalesPeriodType) PeriodType,
                PeriodStart = PeriodStart,
                PeriodEnd = PeriodEnd,
                GrossSales = GrossSales,
                AlcoholSales = AlcoholSales,
                Freight = Freight,
                IntercompanySales = IntercompanySales,
                LotterSales = LotterSales,
                Returns = Returns,
                SalesTax = SalesTax,
                CreatedOn = CreatedOn,
                CreatedById = CreatedById,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById
            };

            return model;
        }
        #endregion ToModel
    }
}
