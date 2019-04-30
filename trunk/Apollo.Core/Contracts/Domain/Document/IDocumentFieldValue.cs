// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/21/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain.Document
{
    public interface IDocumentFieldValue : IHaveAuditData, IHaveId
    {
        int DocumentId { get; set; }
        int FieldId { get; set; }
        int ClientId { get; set; }
        string Key { get; set; }
        string Value { get; set; }
        string FieldTag { get; set; }
    }
}
