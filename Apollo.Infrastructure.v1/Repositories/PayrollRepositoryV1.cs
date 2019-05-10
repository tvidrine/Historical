// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 3/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Payroll;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class PayrollRepositoryV1 : BaseRepository<PayrollDto, IPayroll, IPayroll>, IPayrollRepository
    {
        public PayrollRepositoryV1(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new PayrollDto())
        {
        }

        public async Task<GetResponse<IReadOnlyList<IPayroll>>> GetAllPayrollAsync(int locationId)
        {
            var response = new GetResponse<IReadOnlyList<IPayroll>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} AND LocationId = @locationId;";

                    var results = await connection.QueryAsync<PayrollDto>(sql, new { LocationId = locationId });

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve payroll records.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public Task<GetResponse<IPayroll>> GetPayrollByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResponse<IPayroll>> SavePayrollAsync(IPayroll payroll)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResponse<IReadOnlyList<IPayroll>>> SaveAllPayrollAsync(IReadOnlyList<IPayroll> payroll)
        {
            throw new System.NotImplementedException();
        }
         
        public async Task<GetResponse<IReadOnlyList<IPayrollClassification>>> GetAllPayrollClassificationsAsync(IAudit audit)
        {
            var response = new GetResponse<IReadOnlyList<IPayrollClassification>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetPayrollClassificationSelectStatement();

                    var results = await connection.QueryAsync<PayrollClassificationDto>(sql, new { AuditId = audit.Id, AuditTypeId = (int)audit.AuditType});
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
        public async Task<SaveResponse<IReadOnlyList<IPayrollClassification>>> SaveAllPayrollClassification(IReadOnlyList<IPayrollClassification> items)
        {
            var response = new SaveResponse<IReadOnlyList<IPayrollClassification>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetPayrollClassificationUpdateSqlStatement();

                    var results = await connection.ExecuteAsync(sql, items.Select(m =>
                    {
                        var dto = new PayrollClassificationDto();

                        return dto.FromModel(m);
                    }));

                    response.Content = items;
                    response.Message = $@"{results} payroll classification record were inserted/updated.";
                }
            }
            catch (Exception ex)
            {
                var messaage = $@"Unable to save payroll classification records.";
                response.AddError(ex);
                LogManager.LogError(ex, messaage);
                Console.WriteLine(ex);
            }
            return response;

        }

        private string GetPayrollClassificationUpdateSqlStatement()
        {
            return @"
                   UPDATE [dbo].[PH_PayrollData] SET [ClassCodeLookupID] = @classCodeLookupID, [ClassCodeComments] = @classCodeComments, [EmpState] = @empState  WHERE [PayrollId] = @payrollId;
                    ";
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [dbo].[PH_PayRollData] SET IsDeleted = 1
                WHERE [PayRollID] = @payrollid";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[PH_PayRollData] AS T
                USING (VALUES 
                        ( @payrollid
                        , @auditid
                        , @entityid
                        , @empfirstname
                        , @emplastname
                        , @empssn
                        , @empwages
                        , @empcommissions
                        , @empbonus
                        , @empholiday
                        , @empvacation
                        , @empsickpay
                        , @emptips
                        , @empot15
                        , @empot2
                        , @empot3
                        , @empot4
                        , @empseverance
                        , @empsec125
                        , @datereported
                        , @classcode
                        , @classcodedesc
                        , @empstate
                        , @isicol
                        , @createdon
                        , @classcodecomments
                        , @classcodelookupid
                        , @classcodelookupcode
                        , @locationid
                        , @empcount
                        )
                       ) AS S
                       (
                          [PayRollID]
                        , [AuditID]
                        , [EntityID]
                        , [EmpFirstName]
                        , [EmpLastName]
                        , [EmpSSN]
                        , [EmpWages]
                        , [EmpCommissions]
                        , [EmpBonus]
                        , [EmpHoliday]
                        , [EmpVacation]
                        , [EmpSickPay]
                        , [EmpTips]
                        , [EmpOT15]
                        , [EmpOT2]
                        , [EmpOT3]
                        , [EmpOT4]
                        , [EmpSeverance]
                        , [EmpSec125]
                        , [DateReported]
                        , [ClassCode]
                        , [ClassCodeDesc]
                        , [EmpState]
                        , [IsICOL]
                        , [CreatedOn]
                        , [ClassCodeComments]
                        , [ClassCodeLookupID]
                        , [ClassCodeLookupCode]
                        , [LocationID]
                        , [EmpCount]
                       ) ON T.[PayRollID] = S.[PayRollID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditID]
                        , [EntityID]
                        , [EmpFirstName]
                        , [EmpLastName]
                        , [EmpSSN]
                        , [EmpWages]
                        , [EmpCommissions]
                        , [EmpBonus]
                        , [EmpHoliday]
                        , [EmpVacation]
                        , [EmpSickPay]
                        , [EmpTips]
                        , [EmpOT15]
                        , [EmpOT2]
                        , [EmpOT3]
                        , [EmpOT4]
                        , [EmpSeverance]
                        , [EmpSec125]
                        , [DateReported]
                        , [ClassCode]
                        , [ClassCodeDesc]
                        , [EmpState]
                        , [IsICOL]
                        , [CreatedOn]
                        , [ClassCodeComments]
                        , [ClassCodeLookupID]
                        , [ClassCodeLookupCode]
                        , [LocationID]
                        , [EmpCount]
                           )
                    VALUES (
                          S.[AuditID]
                        , S.[EntityID]
                        , S.[EmpFirstName]
                        , S.[EmpLastName]
                        , S.[EmpSSN]
                        , S.[EmpWages]
                        , S.[EmpCommissions]
                        , S.[EmpBonus]
                        , S.[EmpHoliday]
                        , S.[EmpVacation]
                        , S.[EmpSickPay]
                        , S.[EmpTips]
                        , S.[EmpOT15]
                        , S.[EmpOT2]
                        , S.[EmpOT3]
                        , S.[EmpOT4]
                        , S.[EmpSeverance]
                        , S.[EmpSec125]
                        , S.[DateReported]
                        , S.[ClassCode]
                        , S.[ClassCodeDesc]
                        , S.[EmpState]
                        , S.[IsICOL]
                        , S.[CreatedOn]
                        , S.[ClassCodeComments]
                        , S.[ClassCodeLookupID]
                        , S.[ClassCodeLookupCode]
                        , S.[LocationID]
                        , S.[EmpCount]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AuditID] = S.[AuditID], T.[EntityID] = S.[EntityID], T.[EmpFirstName] = S.[EmpFirstName], T.[EmpLastName] = S.[EmpLastName], T.[EmpSSN] = S.[EmpSSN], 
                        T.[EmpWages] = S.[EmpWages], T.[EmpCommissions] = S.[EmpCommissions], T.[EmpBonus] = S.[EmpBonus], T.[EmpHoliday] = S.[EmpHoliday], T.[EmpVacation] = S.[EmpVacation], 
                        T.[EmpSickPay] = S.[EmpSickPay], T.[EmpTips] = S.[EmpTips], T.[EmpOT15] = S.[EmpOT15], T.[EmpOT2] = S.[EmpOT2], T.[EmpOT3] = S.[EmpOT3], 
                        T.[EmpOT4] = S.[EmpOT4], T.[EmpSeverance] = S.[EmpSeverance], T.[EmpSec125] = S.[EmpSec125], T.[DateReported] = S.[DateReported], T.[ClassCode] = S.[ClassCode], 
                        T.[ClassCodeDesc] = S.[ClassCodeDesc], T.[EmpState] = S.[EmpState], T.[IsICOL] = S.[IsICOL], T.[CreatedOn] = S.[CreatedOn], 
                        T.[ClassCodeComments] = S.[ClassCodeComments], T.[ClassCodeLookupID] = S.[ClassCodeLookupID], T.[ClassCodeLookupCode] = S.[ClassCodeLookupCode], T.[LocationID] = S.[LocationID], T.[EmpCount] = S.[EmpCount] 
                        
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [PayRollID]
                        , [AuditID]
                        , [EntityID]
                        , [EmpFirstName]
                        , [EmpLastName]
                        , [EmpSSN]
                        , [EmpWages]
                        , [EmpCommissions]
                        , [EmpBonus]
                        , [EmpHoliday]
                        , [EmpVacation]
                        , [EmpSickPay]
                        , [EmpTips]
                        , [EmpOT15]
                        , [EmpOT2]
                        , [EmpOT3]
                        , [EmpOT4]
                        , [EmpSeverance]
                        , [EmpSec125]
                        , [DateReported]
                        , [ClassCode]
                        , [ClassCodeDesc]
                        , [EmpState]
                        , [IsICOL]
                        , [CreatedOn]
                        , [ClassCodeComments]
                        , [ClassCodeLookupID]
                        , [ClassCodeLookupCode]
                        , [LocationID]
                        , [EmpCount]
                    FROM [dbo].[PH_PayRollData]
                    WHERE IsDeleted = 0 
                    ";
        }

        private string GetPayrollClassificationSelectStatement()
        {
            return @"
                    SELECT 
                          p.[PayRollID]
                        , p.[AuditID]
                        , p.[EntityID]
                        , p.[EmpFirstName]
                        , p.[EmpLastName]
                        , p.[EmpState]
                        , ClassCodeLookupID = CASE WHEN EndorsementType = 1 THEN 1 ELSE
                            ISNULL(p.[ClassCodeLookupID], 
                                dbo.fnGetClassCodeId(@auditTypeId, p.EmpState, p.ClassCode, SUBSTRING(ClassCodeDesc, CHARINDEX('-',p.ClassCodeDesc) + 1, LEN(p.ClassCodeDesc) - CHARINDEX('-', p.ClassCodeDesc)) )) END
                        , [ClassCodeComments]
                    FROM [dbo].[PH_PayRollData] p LEFT JOIN [dbo].[Endorsements] e ON p.EmpSSN = e.SSN
                    WHERE AuditId = @auditId AND IsDeleted = 0
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements


    }
}



