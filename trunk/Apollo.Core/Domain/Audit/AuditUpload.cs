// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Audit
{
    public class AuditUpload : ModelBase, IAuditUpload
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Size { get; set; }
        public int AttachedBy { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public string Description { get; set; }
        public int AuditId { get; set; }
        public int EntityId { get; set; }
        public string FilePath { get; set; }
        public string Directory { get; set; }
        public string OriginalFileName { get; set; }
        public byte[] Data { get; set; }
    }
}