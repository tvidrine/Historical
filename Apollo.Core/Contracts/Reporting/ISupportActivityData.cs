// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/06/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Contracts.Reporting
{
    public interface ISupportActivityData
    {
        int AuditId { get; set; } 
        string CurrentStatus { get; set; }
        string CarrierName { get; set; } 
        string CompanyName { get; set; }
        string InsuredName { get; set; }
        string InsuredState { get; set; }
        string PolicyPhone { get; set; }
        string PolicyPeriod { get; set; }
        DateTime? DueDate { get; set; }
        int DaysWorkable { get; set; }
        DateTime? AssignedDate { get; set; }
        string SupportName { get; set; }
        DateTime? LastPHAccess { get; set; }
        DateTime? FirstContact { get; set; }
        DateTime? SecondContact { get; set; }
        DateTime? ThirdContact { get; set; }
        DateTime? FirstAgentContact { get; set; }
        DateTime? SecondAgentContact { get; set; }
        DateTime? LastContact { get; set; }
        string CloseOut { get; set; }
        bool IsDispute { get; set; } 
        string Late { get; set; }
    }
}