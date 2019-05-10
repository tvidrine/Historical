// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/2/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Contracts.Workflow;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Workflow;

namespace Apollo.Infrastructure.Repositories
{
    public class AuditWorkflowRepository : BaseRepository<WorkflowDto, IAuditWorkflow, IAuditWorkflow>, IAuditWorkflowRepository
    {
        public AuditWorkflowRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new WorkflowDto())
        {
        }

        public override async Task<DeleteResponse> DeleteAsync(int id)
        {
            var response = new DeleteResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetWorkflowStepDeleteStatement()};
                                 {GetDeleteStatement()}";

                    var trans = connection.BeginTransaction();
                    var results = await connection.ExecuteAsync(sql, new { id }, trans);

                    trans.Commit();
                    response.Message = $@"{results} Workflow records were deleted.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error deleting Workflow with id: {id}";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }
        public override async Task<GetResponse<IAuditWorkflow>> GetByIdAsync(int id)
        {
            var response = new GetResponse<IAuditWorkflow>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND Id = @id;
                                 {GetSelectStatmentWorkflowStep()} AND w.Id = @id; ";

                    var results = await connection.QueryMultipleAsync(sql, new { Id = id });
                   
                    // Get the workflow model
                    var workflow = results.Read<WorkflowDto>().Single()
                        .ToModel();

                    // Get the workflow steps
                    workflow.Steps  = results.Read<WorkflowStepDto>()
                        .ToDictionary(dto => dto.Key, dto => dto.ToModel());

                    response.Content = workflow;
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve Workflow records.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }
        public async Task<GetResponse<IAuditWorkflow>> GetWorkflowByKeyAsync(string workflowKey)
        {
            var response = new GetResponse<IAuditWorkflow>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND [Name] = @name;
                                 {GetSelectStatmentWorkflowStep()} AND w.[Name] = @name";

                    var results = await connection.QueryMultipleAsync(sql, new { Name = workflowKey});

                    // Get the workflow model
                    var workflow = results.Read<WorkflowDto>().Single()
                        .ToModel();

                    // Get the workflow step models as a dictionary for lookups
                    workflow.Steps = results.Read<WorkflowStepDto>()
                        .ToDictionary(dto => dto.Key, dto => dto.ToModel());

                    response.Content = workflow;
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve the workflow.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }
        public override async Task<SaveResponse<IAuditWorkflow>> SaveAsync(IAuditWorkflow item)
        {
            var response = new SaveResponse<IAuditWorkflow>();

            try
            {
                var sql = GetMergeStatement();
                using (var connection = ConnectionFactory.GetConnection())
                {
                    using (var trans = connection.BeginTransaction())
                    {
                        // Save workflow
                        var workflowDto = await connection.QuerySingleAsync<WorkflowDto>(sql, Dto.FromModel(item), trans);

                        // Save workflow steps
                        var workflowStepDtos = item.Steps.Values
                            .Select(r =>
                            {
                                var dto = (WorkflowStepDto)new WorkflowStepDto()
                                    .FromModel(r);
                                dto.WorkflowId = workflowDto.Id;

                                return dto;
                            });

                        await connection.ExecuteAsync(GetWorkflowStepMergeStatement(), workflowStepDtos, trans);
                        trans.Commit();

                        // Get the workflow now that the IDs have been created
                        var getResponse = await GetByIdAsync(workflowDto.Id);

                        response.Join<SaveResponse<IAuditWorkflow>>(getResponse);

                        response.Content = getResponse.Content;
                        response.Message = $@"RuleSet: {item.Name} record was inserted/updated.";
                    }

                }
            }
            catch (Exception e)
            {
                var message = $@"Error saving RuleSet information.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[Workflow] SET IsDeleted = 1
                WHERE [Id] = @id";
        }

        private string GetWorkflowStepDeleteStatement()
        {
            return @"
                UPDATE [v2].[WorkflowStep] SET IsDeleted = 1
                WHERE [WorkFlowId] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[Workflow] AS T
                USING (VALUES 
                        ( @id
                        , @name
                        , @rootStepKey
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [Name]
                        , [RootStepKey]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Name] = S.[Name]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Name]
                        , [RootStepKey]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Name]
                        , S.[RootStepKey]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Name] = S.[Name], T.[RootStepKey] = S.[RootStepKey], T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], 
                        T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }

        private string GetWorkflowStepMergeStatement()
        {
            return @"
                MERGE [v2].[WorkflowStep] AS T
                USING (VALUES 
                        ( @id
                        , @workflowid
                        , @key
                        , @rulesetkey
                        , @onFailureStepKey
                        , @onSuccessStepKey
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [WorkflowId]
                        , [Key]
                        , [RuleSetKey]
                        , [OnFailureStepKey]
                        , [OnSuccessStepKey]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Key] = S.[Key]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [WorkflowId]
                        , [Key]
                        , [RuleSetKey]
                        , [OnFailureStepKey]
                        , [OnSuccessStepKey]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[WorkflowId]
                        , S.[Key]
                        , S.[RuleSetKey]
                        , S.[OnFailureStepKey]
                        , S.[OnSuccessStepKey]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[WorkflowId] = S.[WorkflowId], T.[Key] = S.[Key], T.[RuleSetKey] = S.[RuleSetKey], T.[OnFailureStepKey] = S.[OnFailureStepKey], T.[OnSuccessStepKey] = S.[OnSuccessStepKey], 
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
                        , [RootStepKey]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[Workflow]
                    WHERE IsDeleted = 0 
                    ";
        }

        private string GetSelectStatmentWorkflowStep()
        {
            return @"
                    SELECT 
                          ws.[Id]
                        , ws.[WorkflowId]
                        , ws.[Key]
                        , ws.[RuleSetKey]
                        , ws.[OnFailureStepKey]
                        , ws.[OnSuccessStepKey]
                        , ws.[CreatedOn]
                        , ws.[CreatedById]
                        , ws.[LastModifiedOn]
                        , ws.[LastModifiedById]
                    FROM [v2].[WorkflowStep] ws INNER JOIN [v2].[Workflow] w ON ws.WorkflowId = w.Id
                    WHERE WS.IsDeleted = 0 ";
        }
        #endregion Select Statement
        #endregion Sql Statements

    }
}



