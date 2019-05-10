// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/23/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Policies;

namespace Apollo.Infrastructure.Models
{
    public class PolicyDto : DtoBase<IPolicy, IPolicy>
    {
        #region Public Properties
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IPolicy model)
        {
            Id = model.Id;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IPolicy ToModel()
        {
            var model = new Policy
            {
                Id = Id,
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
