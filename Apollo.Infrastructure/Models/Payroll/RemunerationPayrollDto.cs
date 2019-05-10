// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Domain.Payroll;

namespace Apollo.Infrastructure.Models.Payroll
{
    public class RemunerationPayrollDto : DtoBase<IRemunerationPayroll, IRemunerationPayroll>
    {
        #region Public Properties
        public int AuditTypeId { get; set; }
        public string State { get; set; }
        public int ClientId { get; set; }
        public bool IncludeWage { get; set; }
        public bool IncludeCommission { get; set; }
        public bool IncludeBonus { get; set; }
        public bool IncludeHoliday { get; set; }
        public bool IncludeVacation { get; set; }
        public bool IncludeSickPay { get; set; }
        public bool IncludeTips { get; set; }
        public bool IncludeOvertime { get; set; }
        public bool IncludeSeverance { get; set; }
        public bool IncludeSection125 { get; set; }
        public DateTimeOffset EffectiveStart { get; set; }
        public DateTimeOffset? EffectiveEnd { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IRemunerationPayroll model)
        {
            Id = model.Id;
            AuditTypeId = (int) model.AuditType;
            State = model.State;
            ClientId = model.ClientId;
            IncludeWage = model.IncludeWage;
            IncludeCommission = model.IncludeCommission;
            IncludeBonus = model.IncludeBonus;
            IncludeHoliday = model.IncludeHoliday;
            IncludeVacation = model.IncludeVacation;
            IncludeSickPay = model.IncludeSickPay;
            IncludeTips = model.IncludeTips;
            IncludeOvertime = model.IncludeOvertime;
            IncludeSeverance = model.IncludeSeverance;
            IncludeSection125 = model.IncludeSection125;
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
        public override IRemunerationPayroll ToModel()
        {
            var model = new RemunerationPayroll
            {
                Id = Id,
                AuditType = (AuditTypeEnum) AuditTypeId,
                State = State.TrimEnd(),
                ClientId = ClientId,
                IncludeWage = IncludeWage,
                IncludeCommission = IncludeCommission,
                IncludeBonus = IncludeBonus,
                IncludeHoliday = IncludeHoliday,
                IncludeVacation = IncludeVacation,
                IncludeSickPay = IncludeSickPay,
                IncludeTips = IncludeTips,
                IncludeOvertime = IncludeOvertime,
                IncludeSeverance = IncludeSeverance,
                IncludeSection125 = IncludeSection125,
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
