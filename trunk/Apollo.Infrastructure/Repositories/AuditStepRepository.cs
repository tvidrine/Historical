// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Audit;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class AuditStepRepository : BaseRepository<AuditStepDto, IAuditStep, IAuditStep>, IAuditStepRepository
    {
        public AuditStepRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new AuditStepDto())
        {
        }

        public async Task<GetResponse<IAuditStep>> GetByAuditIdAsync(int auditId, int entityId, WizardPageEnum page)
        {
            var response = new GetResponse<IAuditStep>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()}
								 AND a.AuditId = @auditId AND a.EntityId = @entityId AND w.Id = @pageId;";

                    var result = await connection.QuerySingleAsync<AuditStepDto>(sql, new { AuditId = auditId, EntityId = entityId, PageId = (int) page });

                    response.Content = result
                        .ToModel();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve audit steps.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IAuditStep>> GetCurrentStepAsync(int auditId)
        {
            var response = new GetResponse<IAuditStep>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()}
								 AND a.AuditId = @auditId;";

                    var result = await connection.QueryAsync<AuditStepDto>(sql, new { AuditId = auditId});

                    // Do not count Requirements page for now
                    response.Content = result
                        .Where(s => s.IsCompleted == false && s.WizardPageId > 1)
                        .Select(dto => dto.ToModel())
                        .OrderBy(s => s.StepOrder)
                        .FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve audit steps.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[AuditStep] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[AuditStep] AS T
                USING (VALUES 
                        ( @id
                        , @auditid
                        , @entityId
                        , @wizardPageId
                        , @steporder
                        , @state
                        , @iscompleted
                        , @completedbyid
                        , @completedon
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [Auditid]
                        , [EntityId]
                        , [WizardPageId]
                        , [StepOrder]
                        , [State]
                        , [IsCompleted]
                        , [CompletedById]
                        , [CompletedOn]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[AuditId] = S.[AuditId] AND T.[EntityId] = S.[EntityId] AND T.[WizardPageId] = S.[WizardPageId]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Auditid]
                        , [EntityId]
                        , [WizardPageId]
                        , [StepOrder]
                        , [State]
                        , [IsCompleted]
                        , [CompletedById]
                        , [CompletedOn]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Auditid]
                        , S.[EntityId]
                        , S.[WizardPageId]
                        , S.[StepOrder]
                        , S.[State]
                        , S.[IsCompleted]
                        , S.[CompletedById]
                        , S.[CompletedOn]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[StepOrder] = S.[StepOrder], T.[State] = S.[State], T.[IsCompleted] = S.[IsCompleted], 
                        T.[CompletedById] = S.[CompletedById], T.[CompletedOn] = S.[CompletedOn], T.[LastModifiedOn] = S.[LastModifiedOn], 
                        T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectByParentIdStatement()
        {
            return $@"{GetSelectStatement()} AND a.AuditId = @parentId";
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          a.[Id]
                        , a.[Auditid]
                        , a.[EntityId]
                        , a.[WizardPageId]
                        , a.[StepOrder]
                        , a.[State]
                        , PreviousStepState = p.[State]
                        , [WizardPage] = w.[Page]
                        , [WizardMenuText] = w.[MenuText]
	                    , [WizardMenuImageUrl] = w.[MenuImageUrl]
                        , a.[IsCompleted]
                        , a.[CompletedById]
                        , a.[CompletedOn]
                        , a.[CreatedOn]
                        , a.[CreatedById]
                        , a.[LastModifiedOn]
                        , a.[LastModifiedById]
                    FROM [v2].[AuditStep] a INNER JOIN [v2].[WizardPage] w ON a.WizardPageId = w.Id
                        LEFT JOIN [v2].[AuditStep] p ON a.Auditid = p.Auditid AND a.EntityId = p.EntityId AND p.StepOrder = a.StepOrder - 1
                    WHERE a.IsDeleted = 0 
                    ";
        }

        protected override string GetSummarySelectStatement()
        {
            return GetSelectStatement();
        }
        #endregion Select Statement
        #endregion Sql Statements
        
    }
}



