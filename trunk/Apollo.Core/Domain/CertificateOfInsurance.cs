// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/27/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain
{
    public class CertificateOfInsurance : ModelBase, ICertificateOfInsurance
    {
        public int LaborId { get; set; }
        public string CarrierName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? PolicyStart { get; set; }
        public DateTime? PolicyEnd { get; set; }
        public decimal AggregateLimit { get; set; }
        public IAuditUpload File { get; set; }
    }
}
