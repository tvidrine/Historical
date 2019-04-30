using System;
using Apollo.Core.Contracts.Reporting;

namespace Apollo.Core.Domain.Reporting
{
    public class AuditorActivityData : IAuditorActivityData
    {
        public int AuditId { get; set; }
        public string CurrentStatus { get; set; }
        public string CarrierName { get; set; }
        public string CompanyName { get; set; }
        public string InsuredName { get; set; }
        public string InsuredState { get; set; }
        public string PolicyPhone { get; set; }
        public string PolicyPeriod { get; set; }
        public int DaysWorkable { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Auditor { get; set; }
        public DateTime? FirstContact { get; set; }
        public DateTime? SecondContact { get; set; }
        public DateTime? ThirdContact { get; set; }
        public DateTime? FirstAgentContact { get; set; }
        public DateTime? SecondAgentContact { get; set; }
        public DateTime? LastContact { get; set; }
        public DateTime? RecordsReceived { get; set; }
        public string RecordsComplete { get; set; }
        public bool IsDispute { get; set; }
        public string Late { get; set; }
    }
}
