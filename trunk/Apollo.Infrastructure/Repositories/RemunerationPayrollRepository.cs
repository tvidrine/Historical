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
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Payroll;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class RemunerationPayrollRepository : BaseRepository<RemunerationPayrollDto, IRemunerationPayroll, IRemunerationPayroll>, IRemunerationRepository
    {
        public RemunerationPayrollRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new RemunerationPayrollDto())
        {
        }

        public async Task<GetResponse<IRemunerationPayroll>> GetAsync(RemunerationRequest request)
        {
            var response = new GetResponse<IRemunerationPayroll>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSummarySelectStatement()} AND [State] = @state";

                    var results = await connection.QuerySingleAsync<RemunerationPayrollDto>(sql, new { State = request.State });

                    response.Content = results.ToModel();
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

        public async Task<GetResponse<IReadOnlyList<IRemunerationPayroll>>> GetAllAsync(RemunerationRequest request)
        {
            var response = new GetResponse<IReadOnlyList<IRemunerationPayroll>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSummarySelectStatement()} AND [State] = @state";

                    var results = await connection.QueryAsync<RemunerationPayrollDto>(sql, new { State = request.State });

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
                UPDATE [v2].[RemunerationPayroll] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[RemunerationPayroll] AS T
                USING (VALUES 
                        ( @id
                        , @audittypeid
                        , @state
                        , @clientid
                        , @includewage
                        , @includecommission
                        , @includebonus
                        , @includeholiday
                        , @includevacation
                        , @includesickpay
                        , @includetips
                        , @includeovertime
                        , @includeseverance
                        , @includesection125
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
                        , [IncludeWage]
                        , [IncludeCommission]
                        , [IncludeBonus]
                        , [IncludeHoliday]
                        , [IncludeVacation]
                        , [IncludeSickPay]
                        , [IncludeTips]
                        , [IncludeOvertime]
                        , [IncludeSeverance]
                        , [IncludeSection125]
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
                        , [IncludeWage]
                        , [IncludeCommission]
                        , [IncludeBonus]
                        , [IncludeHoliday]
                        , [IncludeVacation]
                        , [IncludeSickPay]
                        , [IncludeTips]
                        , [IncludeOvertime]
                        , [IncludeSeverance]
                        , [IncludeSection125]
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
                        , S.[IncludeWage]
                        , S.[IncludeCommission]
                        , S.[IncludeBonus]
                        , S.[IncludeHoliday]
                        , S.[IncludeVacation]
                        , S.[IncludeSickPay]
                        , S.[IncludeTips]
                        , S.[IncludeOvertime]
                        , S.[IncludeSeverance]
                        , S.[IncludeSection125]
                        , S.[EffectiveStart]
                        , S.[EffectiveEnd]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[IncludeWage] = S.[IncludeWage], T.[IncludeCommission] = S.[IncludeCommission], T.[IncludeBonus] = S.[IncludeBonus], T.[IncludeHoliday] = S.[IncludeHoliday], T.[IncludeVacation] = S.[IncludeVacation], T.[IncludeSickPay] = S.[IncludeSickPay], T.[IncludeTips] = S.[IncludeTips], 
                        T.[IncludeOvertime] = S.[IncludeOvertime], T.[IncludeSeverance] = S.[IncludeSeverance], T.[IncludeSection125] = S.[IncludeSection125], T.[EffectiveEnd] = S.[EffectiveEnd], 
                        T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                        
                OUTPUT inserted.*;

                UPDATE [v2].[RemunerationPayroll] SET EffectiveEnd = DATEADD(d,-1, @effectiveStart)
                WHERE [Id] IN (SELECT TOP 1 [Id] FROM [v2].[RemunerationPayroll] WHERE [AuditTypeId] = @auditTypeId AND [ClientId] = @clientId AND [State] = @state AND [EffectiveStart] < @effectiveStart AND IsDeleted = 0);";
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
                        , [IncludeWage]
                        , [IncludeCommission]
                        , [IncludeBonus]
                        , [IncludeHoliday]
                        , [IncludeVacation]
                        , [IncludeSickPay]
                        , [IncludeTips]
                        , [IncludeOvertime]
                        , [IncludeSeverance]
                        , [IncludeSection125]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[RemunerationPayroll]
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



