// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 3/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain.Payroll;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class PayrollDto : DtoBase<IPayroll, IPayroll>
    {
        #region Public Properties
        public int PayRollID { get; set; }
        public int AuditID { get; set; }
        public int EntityID { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpSSN { get; set; }
        public decimal EmpWages { get; set; }
        public decimal EmpCommissions { get; set; }
        public decimal EmpBonus { get; set; }
        public decimal EmpHoliday { get; set; }
        public decimal EmpVacation { get; set; }
        public decimal EmpSickPay { get; set; }
        public decimal EmpTips { get; set; }
        public decimal EmpOT15 { get; set; }
        public decimal EmpOT2 { get; set; }
        public decimal EmpOT3 { get; set; }
        public decimal EmpOT4 { get; set; }
        public decimal EmpSeverance { get; set; }
        public decimal EmpSec125 { get; set; }
        public DateTime DateReported { get; set; }
        public string ClassCode { get; set; }
        public string ClassCodeDesc { get; set; }
        public string EmpState { get; set; }
        public bool IsICOL { get; set; }
        public string ClassCodeComments { get; set; }
        public int ClassCodeLookupID { get; set; }
        public string ClassCodeLookupCode { get; set; }
        public int LocationID { get; set; }
        public int EmpCount { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IPayroll model)
        {
            PayRollID = model.Id;
            //AuditID = model.AuditID;
            //EntityID = model.EntityID;
            EmpFirstName = model.EmpFirstName;
            EmpLastName = model.EmpLastName;
            EmpSSN = model.Key;
            EmpWages = model.EmpWages;
            EmpCommissions = model.EmpCommissions;
            EmpBonus = model.EmpBonus;
            EmpHoliday = model.EmpHoliday;
            EmpVacation = model.EmpVacation;
            EmpSickPay = model.EmpSickPay;
            EmpTips = model.EmpTips;
            EmpOT15 = model.EmpOT15;
            EmpOT2 = model.EmpOT2;
            EmpOT3 = model.EmpOT3;
            EmpOT4 = model.EmpOT4;
            EmpSeverance = model.EmpSeverance;
            EmpSec125 = model.EmpSec125;
            DateReported = model.DateReported;
            ClassCode = model.ClassCode;
            ClassCodeDesc = model.ClassCodeDesc;
            EmpState = model.EmpState;
            IsICOL = model.IsICOL;
            CreatedOn = model.CreatedOn;
            ClassCodeComments = model.ClassCodeComments;
            ClassCodeLookupID = model.ClassCodeLookupID;
            ClassCodeLookupCode = model.ClassCodeLookupCode;
            LocationID = model.LocationID;
            EmpCount = model.EmpCount;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IPayroll ToModel()
        {
            var model = new Payroll
            {
                Id = PayRollID,
                EmpFirstName = EmpFirstName,
                EmpLastName = EmpLastName,
                Key = EmpSSN,
                EmpWages = EmpWages,
                EmpCommissions = EmpCommissions,
                EmpBonus = EmpBonus,
                EmpHoliday = EmpHoliday,
                EmpVacation = EmpVacation,
                EmpSickPay = EmpSickPay,
                EmpTips = EmpTips,
                EmpOT15 = EmpOT15,
                EmpOT2 = EmpOT2,
                EmpOT3 = EmpOT3,
                EmpOT4 = EmpOT4,
                EmpSeverance = EmpSeverance,
                EmpSec125 = EmpSec125,
                DateReported = DateReported,
                ClassCode = ClassCode,
                ClassCodeDesc = ClassCodeDesc,
                EmpState = EmpState,
                IsICOL = IsICOL,
                CreatedOn = CreatedOn,
                ClassCodeComments = ClassCodeComments,
                ClassCodeLookupID = ClassCodeLookupID,
                ClassCodeLookupCode = ClassCodeLookupCode,
                LocationID = LocationID,
                EmpCount = EmpCount
            };

            return model;
        }
        #endregion ToModel
    }
}
