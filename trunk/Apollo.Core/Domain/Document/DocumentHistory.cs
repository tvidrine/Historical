// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/4/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Document
{
    public class DocumentHistory : ModelBase, IDocumentHistory
    {
        public int AuditId { get; set; }
        public DocumentTypes DocumentTypeId { get; set; }
        public string Filename { get; set; }
        public DateTimeOffset? PrintedOn { get; set; }
        public int PrintedBy { get; set; }
    }
}
