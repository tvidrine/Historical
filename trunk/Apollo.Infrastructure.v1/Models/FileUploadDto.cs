// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Extensions;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.v1.Models
{
    public class FileUploadDto : DtoBase<IAuditUpload, IAuditUpload>
    {
        #region Public Properties
        public int AuditUploadsId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Size { get; set; }
        public int AttachedBy { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int AuditId { get; set; }
        public int EntityId { get; set; }
        public string FilePath { get; set; }
        public string Directory { get; set; }
        public string OriginalFileName { get; set; }
        public byte[] FileData { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditUpload model)
        {
            AuditUploadsId = model.Id;
            Title = model.Title;
            Date = model.Date;
            Size = model.Size;
            AttachedBy = model.AttachedBy;
            Type = model.DocumentType.ToCategory();
            Description = model.Description;
            AuditId = model.AuditId;
            EntityId = model.EntityId;
            FilePath = model.FilePath;
            Directory = model.Directory;
            OriginalFileName = model.OriginalFileName;
            FileData = model.Data;

            return this;
        }

       
        #endregion FromModel

        #region ToModel
        public override IAuditUpload ToModel()
        {
            var model = new AuditUpload
            {
                Id = AuditUploadsId,
                Title = Title,
                Date = Date,
                Data = FileData,
                Size = Size,
                AttachedBy = AttachedBy,
                DocumentType = DocumentTypes.NotSet,
                Description = Description,
                AuditId = AuditId,
                EntityId = EntityId,
                FilePath = FilePath,
                Directory = Directory,
                OriginalFileName = OriginalFileName
            };

            return model;
        }
        #endregion ToModel
    }
}
