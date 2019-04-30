// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Audit
{
    public interface IAuditUpload : IHaveAuditData, IHaveId
    {
        string Title { get; set; }
        DateTime Date { get; set; }
        string Size { get; set; }
        int AttachedBy { get; set; }
        DocumentTypes DocumentType { get; set; }
        string Description { get; set; }
        int AuditId { get; set; }
        int EntityId { get; set; }
        string FilePath { get; set; }
        string Directory { get; set; }
        string OriginalFileName { get; set; }
        byte[] Data { get; set; }
    }
}
