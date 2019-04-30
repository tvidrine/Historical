// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/14/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Document
{
    public class MergeDocumentField : ModelBase, IMergeDocumentField
    {
        public string Tag { get; set; }
        public MergeDocumentFieldTypes FieldType { get; set; }
        public string Field { get; set; }
        public object Value { get; set; }
    }
}