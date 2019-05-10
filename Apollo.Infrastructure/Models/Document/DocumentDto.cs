// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models.Document
{
    public class DocumentDto : DtoBase<IDocument, IDocument>
    {
        #region Public Properties
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public byte[] Data { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IDocument model)
        {
            Id = model.Id;
            ClientId = model.ClientId;
            Name = model.Name;
            Description = model.Description;
            TypeId = (int) model.DocumentType;
            Data = model.Data;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IDocument ToModel()
        {
            var model = new Core.Domain.Document.Mocument
            {
                Id = Id,
                ClientId = ClientId,
                Name = Name,
                Description = Description,
                DocumentType = (DocumentTypes)TypeId,
                Data = Data,
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
