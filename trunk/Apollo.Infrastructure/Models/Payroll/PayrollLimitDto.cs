// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Domain.Payroll;

namespace Apollo.Infrastructure.Models.Payroll
{
    public class PayrollLimitDto : DtoBase<IPayrollLimit, IPayrollLimit>
    {
        #region Public Properties
        public int AuditTypeId { get; set; }
        public int ClientId { get; set; }
        public int EntityTypeId { get; set; }
        public string State { get; set; }
        public int EmployeeTypeId { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public DateTimeOffset EffectiveStart { get; set; }
        public DateTimeOffset? EffectiveEnd { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IPayrollLimit model)
        {
            Id = model.Id;
            AuditTypeId = (int) model.AuditType;
            ClientId = model.ClientId;
            EntityTypeId = model.EntityTypeId;
            State = model.State;
            EmployeeTypeId = (int) model.EmployeeType;
            Min = model.Min;
            Max = model.Max;
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
        public override IPayrollLimit ToModel()
        {
            var model = new PayrollLimit
            {
                Id = Id,
                AuditType = (AuditTypeEnum) AuditTypeId,
                ClientId = ClientId,
                EntityTypeId = EntityTypeId,
                State = State,
                EmployeeType = (PayrollLimitEmployeeTypes) EmployeeTypeId,
                Min = Min,
                Max = Max,
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
