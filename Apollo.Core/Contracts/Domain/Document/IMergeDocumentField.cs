// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/12/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Document;

namespace Apollo.Core.Contracts.Domain.Document
{
    public interface IMergeDocumentField : IHaveId, IHaveAuditData
    {
        string Tag { get; set; }
        MergeDocumentFieldTypes FieldType { get; set; }
        string Field { get; set; }
        object Value { get; set; }
    }
}