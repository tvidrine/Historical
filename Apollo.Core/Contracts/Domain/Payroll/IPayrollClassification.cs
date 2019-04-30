// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 03/11/2019
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Payroll
{
    public interface IPayrollClassification
    {
        int Id { get; set; }
        int AuditId { get; set; }
        int EntityId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string StateCode { get; set; }
        int ClassCodeId { get; set; }
        string ClassCodeComment { get; set; }
        
    }
}