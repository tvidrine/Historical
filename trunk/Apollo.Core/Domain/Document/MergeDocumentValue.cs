// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/26/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Domain.Document
{
    public class MergeDocumentValue
    {
        public string Key { get; set; }
        public MergeDocumentFieldTypes Type { get; set; }
        public object Value { get; set; }
    }
}