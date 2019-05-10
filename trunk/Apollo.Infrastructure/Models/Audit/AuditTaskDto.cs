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
    public class AuditTaskDto : DtoBase<IAuditTask, IAuditTask>
    {
        #region Public Properties
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IAuditTask model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IAuditTask ToModel()
        {
            var model = new AuditTask
            {
                Id = Id,
                Name = Name,
                Description = Description,
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
