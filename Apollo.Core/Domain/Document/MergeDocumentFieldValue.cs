// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Core;

namespace Apollo.Core.Domain.Document
{
    public class MergeDocumentFieldValue : ModelBase, IDocumentFieldValue
    {
        public int DocumentId { get; set; }
        public int FieldId { get; set; }
        public int ClientId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string FieldTag { get; set; }
    }
}
