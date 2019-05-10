// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.RulesEngine;
using Apollo.RulesEngine.Contracts;
using Apollo.RulesEngine.Models;

namespace Apollo.Infrastructure.Models
{
    public class RuleGrammarTerminalDto : DtoBase<IRuleGrammarTerminal,IRuleGrammarTerminal>
    {
        #region Public Properties
        public byte TerminalType { get; set; }
        public string Keyword { get; set; }
        public string TranslateTo { get; set; }
        #endregion Public Properties

        #region FromModel
        public override IDto FromModel(IRuleGrammarTerminal model)
        {
            Id = model.Id;
            TerminalType = (byte) model.TerminalType;
            Keyword = model.Keyword;
            TranslateTo = model.TranslateTo;
            CreatedOn = model.CreatedOn;
            CreatedById = model.CreatedById;
            LastModifiedOn = model.LastModifiedOn;
            LastModifiedById = model.LastModifiedById;

            return this;
        }
        #endregion FromModel

        #region ToModel
        public override IRuleGrammarTerminal ToModel()
        {
            var model = new RuleGrammarTerminal
            {
                Id = Id,
                TerminalType = (TerminalTypes) TerminalType,
                Keyword = Keyword,
                TranslateTo = TranslateTo,
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
