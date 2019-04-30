// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/06/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Contracts.Reporting
{
    public interface IAuditorActivityData
    {
        int AuditId { get; set; }
        string CurrentStatus { get; set; }
        string CarrierName { get; set; }
        string CompanyName { get; set; }
        string InsuredName { get; set; }
        string InsuredState { get; set; }
        string PolicyPhone { get; set; }
        string PolicyPeriod { get; set; }
        int DaysWorkable { get; set; }
        DateTime? AssignedDate { get; set; }
        DateTime? DueDate { get; set; }
        string Auditor { get; set; }
        DateTime? FirstContact { get; set; }
        DateTime? SecondContact { get; set; }
        DateTime? ThirdContact { get; set; }
        DateTime? FirstAgentContact { get; set; }
        DateTime? SecondAgentContact { get; set; }
        DateTime? LastContact { get; set; }
        DateTime? RecordsReceived { get; set; }
        string RecordsComplete { get; set; }
        bool IsDispute { get; set; }
        string Late { get; set; }
    }
}