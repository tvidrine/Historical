// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/27/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Audit;

namespace Apollo.Infrastructure.Repositories
{
    public class AuditStatusRepository : BaseRepository<AuditStatusDto,IAuditStatus, IAuditStatus>, IAuditStatusRepository
    {
        public AuditStatusRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new AuditStatusDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[AuditStatus] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[AuditStatus] AS T
                USING (VALUES 
                        ( @id
                        , @name
                        , @description
                        , @executionorder
                        , @sortorder
                        , @issystemonly
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [Name]
                        , [Description]
                        , [ExecutionOrder]
                        , [SortOrder]
                        , [IsSystemOnly]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Name] = S.[Name] AND T.[IsDeleted] = 0
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Name]
                        , [Description]
                        , [ExecutionOrder]
                        , [SortOrder]
                        , [IsSystemOnly]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Name]
                        , S.[Description]
                        , S.[ExecutionOrder]
                        , S.[SortOrder]
                        , S.[IsSystemOnly]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Name] = S.[Name], T.[Description] = S.[Description], T.[ExecutionOrder] = S.[ExecutionOrder], T.[SortOrder] = S.[SortOrder], T.[IsSystemOnly] = S.[IsSystemOnly], 
                        T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                        
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT
                          [Id]
                        , [Name]
                        , [Description]
                        , [ExecutionOrder]
                        , [SortOrder]
                        , [IsSystemOnly]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[AuditStatus]
                    WHERE IsDeleted = 0 
                    ";
        }

        protected override string GetSummarySelectStatement()
        {
            return @"
                    SELECT
                          [Id]
                        , [Name]
                        , [Description]
                      FROM [v2].[AuditStatus]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



