// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Document
{
    public interface IDocument : IHaveAuditData, IHaveId
    {
        int ClientId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DocumentTypes DocumentType { get; set; }
        byte[] Data { get; set; }

        IList<IMergeDocumentField> Fields { get; set; }
    }
}
