// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Common;

namespace Apollo.Infrastructure.Models.Audit
{
    public class AuditStatusDto : DtoBase<IAuditStatus, IAuditStatus>
    {
        #region Public Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExecutionOrder { get; set; }
        public int SortOrder { get; set; }
        public bool IsSystemOnly { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditStatus model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            ExecutionOrder = model.ExecutionOrder;
            SortOrder = model.SortOrder;
            IsSystemOnly = model.IsSystemOnly;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAuditStatus ToModel()
        {
            var model = new AuditStatus
            {
                Id = Id,
                Name = Name,
                Description = Description,
                ExecutionOrder = ExecutionOrder,
                SortOrder = SortOrder,
                IsSystemOnly = IsSystemOnly,
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
