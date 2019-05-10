// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/4/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;

namespace Apollo.Infrastructure.Models.Document
{
    public class DocumentHistoryDto : DtoBase<IDocumentHistory, IDocumentHistory>
    {
        #region Public Properties
        public int AuditId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Filename { get; set; }
        public DateTimeOffset? PrintedOn { get; set; }
        public int PrintedBy { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IDocumentHistory model)
        {
            Id = model.Id;
            AuditId = model.AuditId;
            DocumentTypeId = (int) model.DocumentTypeId;
            Filename = model.Filename;
            PrintedOn = model.PrintedOn;
            PrintedBy = model.PrintedBy;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IDocumentHistory ToModel()
        {
            var model = new DocumentHistory
            {
                Id = Id,
                AuditId = AuditId,
                DocumentTypeId = (DocumentTypes) DocumentTypeId,
                Filename = Filename,
                PrintedOn = PrintedOn,
                PrintedBy = PrintedBy,
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
