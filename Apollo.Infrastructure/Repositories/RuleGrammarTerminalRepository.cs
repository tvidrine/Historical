// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;
using Apollo.RulesEngine.Contracts;

namespace Apollo.Infrastructure.Repositories
{
    public class RuleGrammarTerminalRepository : BaseRepository<RuleGrammarTerminalDto, IRuleGrammarTerminal, IRuleGrammarTerminal>, IRuleGrammarTerminalRepository
    {
        public RuleGrammarTerminalRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new RuleGrammarTerminalDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[RuleGrammarTerminal] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[RuleGrammarTerminal] AS T
                USING (VALUES 
                        ( @id
                        , @terminaltype
                        , @keyword
                        , @translateto
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [TerminalType]
                        , [Keyword]
                        , [TranslateTo]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[TerminalType] = S.[TerminalType] AND T.[Keyword] = S.[Keyword]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [TerminalType]
                        , [Keyword]
                        , [TranslateTo]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[TerminalType]
                        , S.[Keyword]
                        , S.[TranslateTo]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[TerminalType] = S.[TerminalType], T.[Keyword] = S.[Keyword], T.[TranslateTo] = S.[TranslateTo], T.[CreatedOn] = S.[CreatedOn], 
                        T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [TerminalType]
                        , [Keyword]
                        , [TranslateTo]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[RuleGrammarTerminal]
                    WHERE IsDeleted = 0 
                    ";
        }

        protected override string GetSummarySelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [TerminalType]
                        , [Keyword]
                        , [TranslateTo]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[RuleGrammarTerminal]
                    WHERE IsDeleted = 0;";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



