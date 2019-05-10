// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/1/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Rule;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class RuleSetRepository : BaseRepository<RuleSetDto, IRuleSet, IRuleSet>, IRuleSetRepository
    {
        public RuleSetRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new RuleSetDto())
        {
        }

        public override async Task<DeleteResponse> DeleteAsync(int id)
        {
            var response = new DeleteResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetRuleDeleteStatement()};
                                 {GetDeleteStatement()}";

                    var trans = connection.BeginTransaction();
                    var results = await connection.ExecuteAsync(sql, new { id }, trans);

                    trans.Commit();
                    response.Message = $@"{results} {typeof(IRuleSet).Name} records were deleted.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error deleting {typeof(IRuleSet).Name}";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public override async Task<GetResponse<IRuleSet>> GetByIdAsync(int id)
        {
            var response = new GetResponse<IRuleSet>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND rs.Id = @id;
                                 {GetRuleSelectStatement()} AND rs.Id = @id; ";

                    var results = await connection.QueryMultipleAsync(sql, new { Id = id });

                    var ruleSet = results.Read<RuleSetDto>()
                        .First()
                        .ToModel();

                    ruleSet.Rules = results.Read<RuleDto>()
                        .Select(dto => dto.ToModel())
                        .AsList();

                    response.Content = ruleSet;
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve {typeof(IRuleSet).Name} records.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IRuleSet>> GetByNameAsync(string name)
        {
            var response = new GetResponse<IRuleSet>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND rs.Name = @name;
                                 {GetRuleSelectStatement()} AND rs.Name = @name; ";

                    var results = await connection.QueryMultipleAsync(sql, new { Name = name });

                    var ruleSet = results.Read<RuleSetDto>()
                        .First()
                        .ToModel();

                    ruleSet.Rules = results.Read<RuleDto>()
                        .Select(dto => dto.ToModel())
                        .AsList();

                    response.Content = ruleSet;
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve {typeof(IRuleSet).Name} records.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public override async Task<SaveResponse<IRuleSet>> SaveAsync(IRuleSet item)
        {
            var response = new SaveResponse<IRuleSet>();

            try
            {
                var sql = GetMergeStatement();
                using (var connection = ConnectionFactory.GetConnection())
                {
                    using (var trans = connection.BeginTransaction())
                    {
                        // Save rule set
                        var ruleSetDto = await connection.QuerySingleAsync<RuleSetDto>(sql, Dto.FromModel(item), trans);
                        
                        // Save rules
                        var ruleDtos = item.Rules
                            .Select(r =>
                            {
                                var dto = (RuleDto) new RuleDto()
                                    .FromModel(r);
                                dto.RuleSetId = ruleSetDto.Id;

                                return dto;
                            });

                        await connection.ExecuteAsync(GetRuleMergeStatement(), ruleDtos, trans);
                        trans.Commit();

                        sql = $@"{GetRuleSelectStatement()} AND RuleSetId = @ruleSetId;";
                        var results = await connection.QueryAsync<RuleDto>(sql, new { RuleSetId = ruleSetDto.Id} );

                        var ruleSet = ruleSetDto.ToModel();
                        ruleSet.Rules = results
                            .Select(dto => dto.ToModel())
                            .AsList();
                        
                        response.Content = ruleSet;
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
                UPDATE [v2].[RuleSet] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        string GetRuleDeleteStatement()
        {
            return @"
                UPDATE [v2].[Rule] SET IsDeleted = 1
                WHERE [RuleSetId] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[RuleSet] AS T
                USING (VALUES 
                        ( @id
                        , @name
                        , @key
                        , @code
                        , @generatedassembly
                        , @categoryId
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [Name]
                        , [Key]
                        , [Code]
                        , [GeneratedAssembly]
                        , [CategoryId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Name]
                        , [Key]
                        , [Code]
                        , [GeneratedAssembly]
                        , [CategoryId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Name]
                        , S.[Key]
                        , S.[Code]
                        , S.[GeneratedAssembly]
                        , S.[CategoryId]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Name] = S.[Name], T.[Key] = S.[Key], T.[Code] = S.[Code], T.[GeneratedAssembly] = S.[GeneratedAssembly], T.[CategoryId] = S.[CategoryId], T.[CreatedOn] = S.[CreatedOn], 
                        T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        private string GetRuleMergeStatement()
        {
            return @"
                MERGE [v2].[Rule] AS T
                USING (VALUES 
                        ( @id
                        , @rulesetid
                        , @name
                        , @body
                        , @generatedcode
                        , @ispublished
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [RuleSetId]
                        , [Name]
                        , [Body]
                        , [GeneratedCode]
                        , [IsPublished]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [RuleSetId]
                        , [Name]
                        , [Body]
                        , [GeneratedCode]
                        , [IsPublished]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[RuleSetId]
                        , S.[Name]
                        , S.[Body]
                        , S.[GeneratedCode]
                        , S.[IsPublished]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[RuleSetId] = S.[RuleSetId], T.[Name] = S.[Name], T.[Body] = S.[Body], T.[GeneratedCode] = S.[GeneratedCode], T.[IsPublished] = S.[IsPublished], T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSummarySelectStatement()
        {
            return GetSelectStatement();
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          rs.[Id]
                        , rs.[Name]
                        , rs.[Key]
                        , rs.[Code]
                        , rs.[GeneratedAssembly]
                        , rs.[CategoryId]
                        , rs.[CreatedOn]
                        , rs.[CreatedById]
                        , rs.[LastModifiedOn]
                        , rs.[LastModifiedById]
                    FROM [v2].[RuleSet] rs
                    WHERE IsDeleted = 0 
                    ";
        }
        private string GetRuleSelectStatement()
        {
            return @"
                    SELECT 
                          r.[Id]
                        , r.[RuleSetId]
                        , r.[Name]
                        , r.[Body]
                        , r.[GeneratedCode]
                        , r.[IsPublished]
                        , r.[CreatedOn]
                        , r.[CreatedById]
                        , r.[LastModifiedOn]
                        , r.[LastModifiedById]
                    FROM [v2].[Rule] r INNER JOIN [v2].[RuleSet] rs ON r.RuleSetId = rs.Id
                    WHERE r.IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements


    }
}



