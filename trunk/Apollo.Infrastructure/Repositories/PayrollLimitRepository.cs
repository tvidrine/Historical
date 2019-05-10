// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/14/2018
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
    public class PayrollLimitRepository : BaseRepository<PayrollLimitDto, IPayrollLimit, IPayrollLimit>, IPayrollLimitRepository
    {
        public PayrollLimitRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new PayrollLimitDto())
        {
        }

        public async Task<GetResponse<IPayrollLimit>> GetAsync(PayrollLimitRequest request)
        {
            var response = new GetResponse<IPayrollLimit>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND [State] = @state AND [ClientId] = @clientId ";

                    var results = await connection.QuerySingleAsync<PayrollLimitDto>(sql, new { State = request.State, ClientId = request.ClientId });

                    response.Content = results.ToModel();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve payroll limit records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<IPayrollLimit>>> GetAllAsync(PayrollLimitRequest request)
        {
            var response = new GetResponse<IReadOnlyList<IPayrollLimit>>();

            try
            {
                using (var connection =ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSummarySelectStatement()} AND [State] = @state" ;

                    var results = await connection.QueryAsync<PayrollLimitDto>(sql, new { State = request.State });

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve payroll limit records.";
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
                UPDATE [v2].[PayrollLimit] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[PayrollLimit] AS T
                USING (VALUES 
                        ( @id
                        , @audittypeid
                        , @clientid
                        , @entitytypeid
                        , @state
                        , @employeetypeid
                        , @min
                        , @max
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
                        , [ClientId]
                        , [EntityTypeId]
                        , [State]
                        , [EmployeeTypeId]
                        , [Min]
                        , [Max]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[AuditTypeId] = S.[AuditTypeId] AND T.[ClientId] = S.[ClientId] AND T.[EntityTypeId] = S.[EntityTypeId] AND T.[State] = S.[State] AND T.[EmployeeTypeId] = S.[EmployeeTypeId] AND T.[EffectiveStart] = S.[EffectiveStart]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditTypeId]
                        , [ClientId]
                        , [EntityTypeId]
                        , [State]
                        , [EmployeeTypeId]
                        , [Min]
                        , [Max]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[AuditTypeId]
                        , S.[ClientId]
                        , S.[EntityTypeId]
                        , S.[State]
                        , S.[EmployeeTypeId]
                        , S.[Min]
                        , S.[Max]
                        , S.[EffectiveStart]
                        , S.[EffectiveEnd]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AuditTypeId] = S.[AuditTypeId], T.[ClientId] = S.[ClientId], T.[EntityTypeId] = S.[EntityTypeId], T.[State] = S.[State], T.[EmployeeTypeId] = S.[EmployeeTypeId], 
                        T.[Min] = S.[Min], T.[Max] = S.[Max], T.[EffectiveStart] = S.[EffectiveStart], T.[EffectiveEnd] = S.[EffectiveEnd], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] ;
                
                UPDATE [v2].[PayrollLimit] SET EffectiveEnd = DATEADD(d,-1, @effectiveStart)
                WHERE [Id] IN (SELECT TOP 1 [Id] FROM [v2].[PayrollLimit] WHERE [AuditTypeId] = @auditTypeId AND [ClientId] = @clientId AND [EntityTypeId] = @entityTypeId AND [State] = @state AND [EmployeeTypeId] = @employeeTypeId AND [EffectiveStart] < @effectiveStart AND IsDeleted = 0)";
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
                          [Id]
                        , [AuditTypeId]
                        , [ClientId]
                        , [EntityTypeId]
                        , [State]
                        , [EmployeeTypeId]
                        , [Min]
                        , [Max]
                        , [EffectiveStart]
                        , [EffectiveEnd]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[PayrollLimit]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



