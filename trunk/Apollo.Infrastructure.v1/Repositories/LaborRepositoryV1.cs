// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/21/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class LaborRepositoryV1 : BaseRepository<LaborDto, ILabor, ILabor>, ILaborRepository
    {
        public LaborRepositoryV1(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new LaborDto())
        {
        }

        public async Task<GetResponse<IReadOnlyList<ILabor>>> GetAllAsync(int auditId, int entityId)
        {
            var response = new GetResponse<IReadOnlyList<ILabor>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND AuditId = @auditId AND EntityId = @entityId;" ;

                    var results = await connection.QueryAsync<LaborDto>(sql, new {AuditId = auditId, EntityId = entityId});

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve labor records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public override async Task<GetResponse<ILabor>> GetByIdAsync(int id)
        {
            var response = new GetResponse<ILabor>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()}
								 AND LaborId = @id;";

                    var result = await connection.QuerySingleAsync<LaborDto>(sql, new { Id = id });

                    response.Content = result
                        .ToModel();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve labor record.";
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
                UPDATE [dbo].[PH_Labor] SET IsDeleted = 1
                WHERE [LaborID] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[PH_Labor] AS T
                USING (VALUES 
                        ( @laborid
                        , @entityid
                        , @auditid
                        , @labortype
                        , @laborinvoicetype
                        , @laborname
                        , @labordescription
                        , @laborclasscode
                        , @laborclasscodedesc
                        , @laboramount
                        , @laborstate
                        , @isLaborOnly
                        , @laborinsured
                        , @classcodelookupid
                        , @classcodelookupcode
                        , @laborinsuredlimit
                        )
                       ) AS S
                       (
                          [LaborID]
                        , [EntityID]
                        , [AuditID]
                        , [LaborType]
                        , [LaborInvoiceType]
                        , [LaborName]
                        , [LaborDescription]
                        , [LaborClassCode]
                        , [LaborClassCodeDesc]
                        , [LaborAmount]
                        , [LaborState]
                        , [IsLaborOnly]
                        , [LaborInsured]
                        , [ClassCodeLookupID]
                        , [ClassCodeLookupCode]
                        , [LaborInsuredLimit]
                       ) ON T.[LaborID] = S.[LaborID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [EntityID]
                        , [AuditID]
                        , [LaborType]
                        , [LaborInvoiceType]
                        , [LaborName]
                        , [LaborDescription]
                        , [LaborClassCode]
                        , [LaborClassCodeDesc]
                        , [LaborAmount]
                        , [LaborState]
                        , [IsLaborOnly]
                        , [LaborInsured]
                        , [ClassCodeLookupID]
                        , [ClassCodeLookupCode]
                        , [LaborInsuredLimit]
                           )
                    VALUES (
                          S.[EntityID]
                        , S.[AuditID]
                        , S.[LaborType]
                        , S.[LaborInvoiceType]
                        , S.[LaborName]
                        , S.[LaborDescription]
                        , S.[LaborClassCode]
                        , S.[LaborClassCodeDesc]
                        , S.[LaborAmount]
                        , S.[LaborState]
                        , S.[IsLaborOnly]
                        , S.[LaborInsured]
                        , S.[ClassCodeLookupID]
                        , S.[ClassCodeLookupCode]
                        , S.[LaborInsuredLimit]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[EntityID] = S.[EntityID], T.[AuditID] = S.[AuditID], T.[LaborType] = S.[LaborType], T.[LaborInvoiceType] = S.[LaborInvoiceType], T.[LaborName] = S.[LaborName], 
                        T.[LaborDescription] = S.[LaborDescription], T.[LaborClassCode] = S.[LaborClassCode], T.[LaborClassCodeDesc] = S.[LaborClassCodeDesc], T.[LaborAmount] = S.[LaborAmount], 
                        T.[LaborState] = S.[LaborState], T.[IsLaborOnly] = S.[IsLaborOnly], T.[LaborInsured] = S.[LaborInsured], T.[ClassCodeLookupID] = S.[ClassCodeLookupID], 
                        T.[ClassCodeLookupCode] = S.[ClassCodeLookupCode], T.[LaborInsuredLimit] = S.[LaborInsuredLimit] 
                        
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [LaborID]
                        , [EntityID]
                        , [AuditID]
                        , [LaborType]
                        , [LaborInvoiceType]
                        , [LaborName]
                        , [LaborDescription]
                        , [LaborClassCode]
                        , [LaborClassCodeDesc]
                        , [LaborAmount]
                        , [LaborState]
                        , [LaborInsured]
                        , [IsLaborOnly]
                        , [ClassCodeLookupID]
                        , [ClassCodeLookupCode]
                        , [LaborInsuredLimit]
                    FROM [dbo].[PH_Labor]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements

        
    }
}



