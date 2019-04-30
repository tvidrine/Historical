// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 03/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Payroll
{
    public class Payroll : ModelBase, IPayroll
    {
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string Key { get; set; }
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
    }
}