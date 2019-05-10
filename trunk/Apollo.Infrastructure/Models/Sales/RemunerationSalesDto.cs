// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Domain.Sales;

namespace Apollo.Infrastructure.Models.Sales
{
    public class RemunerationSalesDto : DtoBase<IRemunerationSales, IRemunerationSales>
    {
        #region Public Properties
        public int AuditTypeId { get; set; }
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
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IRemunerationSales model)
        {
            Id = model.Id;
            AuditTypeId = (int) model.AuditType;
            State = model.State;
            ClientId = model.ClientId;
            IncludeSales = model.IncludeSales;
            IncludeReturns = model.IncludeReturns;
            IncludeFreightShipping = model.IncludeFreightShipping;
            IncludeSalesTax = model.IncludeSalesTax;
            IncludeLotterySales = model.IncludeLotterySales;
            IncludeInterCompanySales = model.IncludeInterCompanySales;
            EffectiveStart = model.EffectiveStart;
            EffectiveEnd = model.EffectiveEnd;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IRemunerationSales ToModel()
        {
            var model = new RemunerationSales
            {
                Id = Id,
                AuditType = (AuditTypeEnum) AuditTypeId,
                State = State.TrimEnd(),
                ClientId = ClientId,
                IncludeSales = IncludeSales,
                IncludeReturns = IncludeReturns,
                IncludeFreightShipping = IncludeFreightShipping,
                IncludeSalesTax = IncludeSalesTax,
                IncludeLotterySales = IncludeLotterySales,
                IncludeInterCompanySales = IncludeInterCompanySales,
                EffectiveStart = EffectiveStart,
                EffectiveEnd = EffectiveEnd,
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
