// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/4/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Document
{
    public interface IDocumentHistory : IHaveAuditData, IHaveId
    {
        int AuditId { get; set; }
        DocumentTypes DocumentTypeId { get; set; }
        string Filename { get; set; }
        DateTimeOffset? PrintedOn { get; set; }
        int PrintedBy { get; set; }
    }
}
