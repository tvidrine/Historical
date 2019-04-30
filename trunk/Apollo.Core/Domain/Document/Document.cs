// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Document
{
    public class Mocument : ModelBase, IDocument
    {
        public Mocument()
        {
            Fields = new List<IMergeDocumentField>();
        }

        public byte[] Data { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public IList<IMergeDocumentField> Fields { get; set; }
    }
}