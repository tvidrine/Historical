// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class ClassCodeRepository : BaseRepository<ClassCodeDto,IClassCode, IClassCode>, IClassCodeRepository
    {
        public ClassCodeRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new ClassCodeDto())
        {
        }
        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllByAuditType(AuditTypeEnum auditType)
        {
            {
                var response = new GetResponse<IReadOnlyList<IClassCode>>();

                try
                {
                    using (var connection = ConnectionFactory.GetConnection())
                    {
                        var results = await connection.QueryAsync<ClassCodeDto>("spsClassCodesForAuditType", new { AuditTypeId = auditType }, commandType: CommandType.StoredProcedure);

                        response.Content = results
                            .Select(dto => dto.ToModel())
                            .AsList();
                    }
                }
                catch (Exception e)
                {
                    var messaage = $@"Unable to retrieve class codes for audit type {auditType}.";
                    response.AddError(e);
                    LogManager.LogError(e, messaage);
                    Console.WriteLine(e);
                }

                return response;
            }
        }
        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForEntity(int entityId)
        {
            var response = new GetResponse<IReadOnlyList<IClassCode>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetClassCodesForEntitySelectStatement();

                    var results = await connection.QueryAsync<ClassCodeDto>(sql, new { EntityId = entityId });

                   
                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve entity records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }
        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForAuditAsync(int auditId)
        {
            {
                var response = new GetResponse<IReadOnlyList<IClassCode>>();

                try
                {
                    using (var connection = ConnectionFactory.GetConnection())
                    {
                        var results = await connection.QueryAsync<ClassCodeDto>("spsClassCodesForAudit", new {AuditId = auditId}, commandType: CommandType.StoredProcedure);

                        response.Content = results
                            .Select(dto => dto.ToModel())
                            .AsList();
                    }
                }
                catch (Exception e)
                {
                    var messaage = $@"Unable to retrieve class codes for assignment {auditId}.";
                    response.AddError(e);
                    LogManager.LogError(e, messaage);
                    Console.WriteLine(e);
                }

                return response;
            }
        }
        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForStatesAsync(IAudit audit)
        {
            var response = new GetResponse<IReadOnlyList<IClassCode>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetClassCodesForStatesSelectStatement();

                    var results = await connection.QueryAsync<ClassCodeDto>(sql, new { AuditTypeId = audit.AuditType });


                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve entity records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetUnknownClassCodeAsync(int id)
        {
            var response = new GetResponse<IReadOnlyList<IClassCode>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetUnknownClassCodeSelectStatement();

                    var results = await connection.QueryAsync<ClassCodeDto>(sql, new {Id = id });


                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve the unknown class code.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        

        public async Task<GetResponse<IReadOnlyList<IExposureBasis>>> GetExposureBasisListAsync()
        {
            var response = new GetResponse<IReadOnlyList<IExposureBasis>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = "spsClassCodeBases";

                    var results = await connection.QueryAsync<ExposureBasisDto>(sql, commandType:CommandType.StoredProcedure);


                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve entity records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }
        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetStandardExceptionsForAudit(IAudit audit)
        {
            var response = new GetResponse<IReadOnlyList<IClassCode>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetStandardExceptionsForAuditSelectStatement();

                    var results = await connection.QueryAsync<ClassCodeDto>(sql, new { AuditTypeId = audit.AuditType, EffectiveDate = audit.Policy.EffectiveStart });


                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve entity records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[ClassCode] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[ClassCode] AS T
                USING (VALUES 
                        ( @id
                        , @code
                        , @description
                        , @isGeneralInclusion
                        , @clientid
                        , @state
                        , @auditTypeId
                        , @exposurebasis
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [Code]
                        , [Description]
                        , [IsGeneralInclusion]
                        , [ClientId]
                        , [State]
                        , [AuditTypeId]
                        , [ExposureBasis]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Code] = S.[Code] AND T.[ClientId] = S.[Clientid] AND T.[AuditTypeId] = S.[AuditTypeId] AND T.[State] = S.[State]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Code]
                        , [Description]
                        , [IsGeneralInclusion]
                        , [ClientId]
                        , [State]
                        , [AuditTypeId]
                        , [ExposureBasis]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Code]
                        , S.[Description]
                        , S.[IsGeneralInclusion]
                        , S.[ClientId]
                        , S.[State]
                        , S.[AuditTypeId]
                        , S.[ExposureBasis]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Description] = S.[Description], T.[IsGeneralInclusion] = S.[IsGeneralInclusion], T.[ExposureBasis] = S.[ExposureBasis], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                        
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        private string GetClassCodesForEntitySelectStatement()
        {
            throw new NotImplementedException();
        }
        private string GetClassCodesForStatesSelectStatement()
        {
            return @"
                 SELECT 
	                  Id = ClassCodeLookUpId
	                , Code = ClassCodeNumber
	                , Description = ClassCodeDescription
	                , State = ClassCodeState
                FROM dbo.ClassCodeLookup
                WHERE ClassCodeState IS NOT NULL 
                    AND AuditTypeID = @auditTypeId 
                    AND ClassCodeNumber <> '0'
                UNION
                SELECT 
	                  Id = ClassCodeLookUpId
	                , Code = ClassCodeNumber
	                , Description = ClassCodeDescription 
	                , State = ClassCodeState
                FROM dbo.ClassCodeLookup 
                WHERE ClassCodeState IS NULL 
	                AND CarrierId IS NULL
	                AND AuditTypeID = @auditTypeId
                    AND ClassCodeNumber <> '0'
                    AND ClassCodeNumber  NOT IN (
		                SELECT ClassCodeNumber
		                FROM dbo.ClassCodeLookup
		                WHERE AuditTypeID = @auditTypeId 
                            AND  ClassCodeState IS NOT NULL
                            AND ClassCodeNumber <> '0')
                ORDER BY ClassCodeNumber, ClassCodeDescription;
                ";
        }
        private string GetStandardExceptionsForAuditSelectStatement()
        {
            return @"
                SELECT
	                  Id = cl.ClassCodeLookUpId
	                , Code = cl.ClassCodeNumber
	                , Description = cl.ClassCodeDescription
                    , cl.AuditTypeId
                FROM dbo.ClassCodeLookup cl INNER JOIN dbo.StandardExceptions se ON cl.ClassCodeLookupID = se.ClassCodeId
                WHERE se.AuditTypeID = @auditTypeId 
                    AND se.EffectiveDate <= @effectiveDate 
                    AND se.IsDeleted = 0;";
        }
        protected override string GetSummarySelectStatement()
        {
            return GetSelectStatement();
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [Code]
                        , [Description]
                        , [IsGeneralInclusion]
                        , [ClientId]
                        , [State]
                        , [AuditTypeId]
                        , [ExposureBasis]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[ClassCode]
                    WHERE IsDeleted = 0 
                    ";
        }

        // TODO THis needs to be removed later when all the class codes are in the same table.
        private string GetUnknownClassCodeSelectStatement()
        {
            return @"
                 SELECT 
	                  Id = ClassCodeLookUpId
	                , Code = ClassCodeNumber
	                , Description = ClassCodeDescription
	                , State = ClassCodeState
                FROM dbo.ClassCodeLookup
                WHERE ClassCodeLookUpId = @id;";
        }
        #endregion Select Statement
        #endregion Sql Statements


    }
}



