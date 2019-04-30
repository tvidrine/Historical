// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 3/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Contracts.Domain.Payroll
{
    public interface IPayroll : IHaveAuditData, IHaveId
    {
        string EmpFirstName { get; set; }
        string EmpLastName { get; set; }
        string Key { get; set; }
        decimal EmpWages { get; set; }
        decimal EmpCommissions { get; set; }
        decimal EmpBonus { get; set; }
        decimal EmpHoliday { get; set; }
        decimal EmpVacation { get; set; }
        decimal EmpSickPay { get; set; }
        decimal EmpTips { get; set; }
        decimal EmpOT15 { get; set; }
        decimal EmpOT2 { get; set; }
        decimal EmpOT3 { get; set; }
        decimal EmpOT4 { get; set; }
        decimal EmpSeverance { get; set; }
        decimal EmpSec125 { get; set; }
        DateTime DateReported { get; set; }
        string ClassCode { get; set; }
        string ClassCodeDesc { get; set; }
        string EmpState { get; set; }
        bool IsICOL { get; set; }
        string ClassCodeComments { get; set; }
        int ClassCodeLookupID { get; set; }
        string ClassCodeLookupCode { get; set; }
        int LocationID { get; set; }
        int EmpCount { get; set; }
    }
}
