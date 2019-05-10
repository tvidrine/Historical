// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Sales;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Sales;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class RemunerationSalesRepository : BaseRepository<RemunerationSalesDto, IRemunerationSales, IRemunerationSales>, IRemunerationSalesRepository
    {
        public RemunerationSalesRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new RemunerationSalesDto())
        {
        }

        public async Task<GetResponse<IReadOnlyList<IRemunerationSales>>> GetAllAsync(RemunerationRequest request)
        {
            var response = new GetResponse<IReadOnlyList<IRemunerationSales>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSummarySelectStatement()} AND [State] = @state";

                    var results = await connection.QueryAsync<RemunerationSalesDto>(sql, new { State = request.State });

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve remuneration records.";
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
                UPDATE [v2].[RemunerationSales] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[RemunerationSales] AS T
                USING (VALUES 
                        ( @id
                        , @audittypeid
                        , @state
                        , @clientid
                        , @includesales
                        , @includereturns
                        , @includefreightshipping
                        , @includesalestax
                        , @includelotterysales
                        , @includeintercompanysales
                        , @effectivestart
                        , @effectiveend
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [AuditTypeId]
                        , [State]
                        , [ClientId]
                        , [IncludeSales]
                        , [IncludeReturns]
                        , [IncludeFreightShipping]
                        , [IncludeSalesTax]
                        , [IncludeLotterySales]
                        , [IncludeInterCompanySales]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[AuditTypeId] = S.[AuditTypeId] AND T.[State] = S.[State] AND T.[ClientId] = S.[ClientId] AND T.[EffectiveStart] = S.[EffectiveStart]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditTypeId]
                        , [State]
                        , [ClientId]
                        , [IncludeSales]
                        , [IncludeReturns]
                        , [IncludeFreightShipping]
                        , [IncludeSalesTax]
                        , [IncludeLotterySales]
                        , [IncludeInterCompanySales]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[AuditTypeId]
                        , S.[State]
                        , S.[ClientId]
                        , S.[IncludeSales]
                        , S.[IncludeReturns]
                        , S.[IncludeFreightShipping]
                        , S.[IncludeSalesTax]
                        , S.[IncludeLotterySales]
                        , S.[IncludeInterCompanySales]
                        , S.[EffectiveStart]
                        , S.[EffectiveEnd]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[IncludeSales] = S.[IncludeSales], T.[IncludeReturns] = S.[IncludeReturns], T.[IncludeFreightShipping] = S.[IncludeFreightShipping], T.[IncludeSalesTax] = S.[IncludeSalesTax], 
                        T.[IncludeLotterySales] = S.[IncludeLotterySales], T.[IncludeInterCompanySales] = S.[IncludeInterCompanySales], T.[EffectiveEnd] = S.[EffectiveEnd], 
                        T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [AuditTypeId]
                        , [State]
                        , [ClientId]
                        , [IncludeSales]
                        , [IncludeReturns]
                        , [IncludeFreightShipping]
                        , [IncludeSalesTax]
                        , [IncludeLotterySales]
                        , [IncludeInterCompanySales]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[RemunerationSales]
                    WHERE IsDeleted = 0 
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



