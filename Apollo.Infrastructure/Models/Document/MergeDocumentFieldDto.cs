// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Document;

namespace Apollo.Infrastructure.Models.Document
{
    public class MergeDocumentFieldDto : DtoBase<IMergeDocumentField, IMergeDocumentField>
    {
        #region Public Properties
        public int DocumentId { get; set; }
        public string Tag { get; set; }
        public MergeDocumentFieldTypes FieldType { get; set; }
        public string Field { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IMergeDocumentField model)
        {
            Id = model.Id;
            Tag = model.Tag;
            FieldType = model.FieldType;
            Field = model.Field;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IMergeDocumentField ToModel()
        {
            var model = new MergeDocumentField
            {
                Id = Id,
                Tag = Tag,
                FieldType = FieldType,
                Field = Field,
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
