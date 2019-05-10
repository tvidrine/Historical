// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Audit;
using Apollo.Infrastructure.Transforms.BerkleyNet.Models;

namespace Apollo.Infrastructure.Transforms.BerkleyNet
{
    [Serializable]
    [XmlRoot("BRACAuditOrder")]
    public class BracAuditOrder
    {
        public string PolicyNumber { get; set; }
        public string DocumentDate { get; set; }
        public string AuditType { get; set; }
        public string GovState  { get; set; }
        public string PolicyEffDate { get; set; }
        public string PolicyExpDate { get; set; }
        public string AONTracking { get; set; }
        public string UserName { get; set; }
        public string OwnerType { get; set; }
        public string PlycyStatus { get; set; }
        public string EndrsDt { get; set; }
        public decimal PremiumPaidToDate { get; set; }
        public decimal NetDepositPremium { get; set; }
        public string TaxId { get; set; }
        public string AuditStatus { get; set; }
        public string AuditDueDt { get; set; }
        public string AuditVendor { get; set; }
        public string AuditTiming { get; set; }
        public Insured Insured { get; set; }
        public Agency Agency { get; set; }
        public PolicyPremiumDetails PolicyPremiumDetails { get; set; }
        public List<Principal> Principals { get; set; }
        public List<Claim> Claims { get; set; }
        public List<Waiver> Waivers { get; set; }
        public string SpecialInstructions { get; set; }

        internal Audit ToAudit(IAudit audit)
        {
            
            
            return audit as Audit;
        }
    }

}