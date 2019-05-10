// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Rules;

namespace Apollo.Infrastructure.Models.Rule
{
    public class RuleDto : DtoBase<IRule, IRule>
    {
        #region Public Properties
        public int RuleSetId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string GeneratedCode { get; set; }
        public bool IsPublished { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IRule model)
        {
            Id = model.Id;
            Name = model.Name;
            Body = model.Body;
            GeneratedCode = model.GeneratedCode;
            IsPublished = model.IsPublished;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IRule ToModel()
        {
            var model = new Core.Domain.Rule.Rule
            {
                Id = Id,
                RuleSetId = RuleSetId,
                Name = Name,
                Body = Body,
                GeneratedCode = GeneratedCode,
                IsPublished = IsPublished,
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
