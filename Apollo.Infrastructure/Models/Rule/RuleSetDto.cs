// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Domain.Rule;

namespace Apollo.Infrastructure.Models.Rule
{
    public class RuleSetDto : DtoBase<IRuleSet, IRuleSet>
    {
        #region Public Properties
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Key { get; set; }
        public string Code { get; set; }
        public byte[] GeneratedAssembly { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IRuleSet model)
        {
            Id = model.Id;
            CategoryId = model.CategoryId;
            Name = model.Name;
            Key = model.Key;
            Code = model.Code;
            GeneratedAssembly = model.GeneratedAssembly;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IRuleSet ToModel()
        {
            var model = new RuleSet
            {
                Id = Id,
                Name = Name,
                CategoryId = CategoryId,
                Key = Key,
                Code = Code,
                GeneratedAssembly = GeneratedAssembly,
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
