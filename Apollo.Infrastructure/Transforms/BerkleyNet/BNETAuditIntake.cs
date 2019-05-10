// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Infrastructure.Transforms.BerkleyNet.Models;

namespace Apollo.Infrastructure.Transforms.BerkleyNet
{
    public class BnetAuditIntake
    {
        public string AuditDate { get; set; }
        public string InsuredName { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyEffectiveDate { get; set; }
        public string PolicyExpirationDate { get; set; }
        public string AuditPeriodStartDate { get; set; }
        public string AuditPeriodEndDate { get; set; }
        public string OrderNumber { get; set; }
        public int BilledHours { get; set; }
        public string ChargeAmount { get; set; }
        public string ProductiveAudit { get; set; }

        public List<ExposureDetail> ExposureInformation { get; set; }

        public List<PrincipalSummary> PrincipalSummary { get; set; }
        public ExposureSummary ExposureSummary { get; set; }
    }
}