// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 03/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Payroll;

namespace Apollo.Core.Domain
{
    public class PayrollClassification : IPayrollClassification
    {
        public int Id { get; set; }
        public int AuditId { get; set; }
        public int EntityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StateCode { get; set; }
        public int ClassCodeId { get; set; }
        public string ClassCodeComment { get; set; }
    }
}