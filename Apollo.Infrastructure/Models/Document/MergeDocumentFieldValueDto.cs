// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Document;

namespace Apollo.Infrastructure.Models.Document
{
    public class MergeDocumentFieldValueDto : DtoBase<IDocumentFieldValue, IDocumentFieldValue>
    {
        #region Public Properties
        public int DocumentId { get; set; }
        public int FieldId { get; set; }
        public int ClientId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Tag { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IDocumentFieldValue model)
        {
            Id = model.Id;
            DocumentId = model.DocumentId;
            FieldId = model.FieldId;
            ClientId = model.ClientId;
            Key = model.Key;
            Value = model.Value;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IDocumentFieldValue ToModel()
        {
            var model = new MergeDocumentFieldValue
            {
                Id = Id,
                DocumentId = DocumentId,
                FieldId = FieldId,
                ClientId = ClientId,
                Key = Key,
                Value = Value,
                FieldTag = Tag,
                CreatedOn = CreatedOn,
                CreatedById = CreatedById,
                LastModifiedOn = LastModifiedOn,
                LastModifiedById = LastModifiedById
            };

            return model;
        }
        #endregion ToModel
    }
}
