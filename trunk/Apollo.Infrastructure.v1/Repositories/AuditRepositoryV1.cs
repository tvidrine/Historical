// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class AuditRepositoryV1 : BaseRepository<AuditsDto, IAudit, IAudit>, IAuditRepository
    {
        private readonly IAuditEntityRepository _entityRepository;

        public AuditRepositoryV1(IConnectionFactory connectionFactory, ILogManager loggerManager, IAuditEntityRepository entityRepository, AuditsDto dto)
            : base(connectionFactory, loggerManager, dto)
        {
            _entityRepository = entityRepository;
        }

        public override async Task<GetResponse<IAudit>> GetByIdAsync(int id)
        {
            var response = new GetResponse<IAudit>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()} WHERE AuditId = @id;";

                    var result = await connection.QuerySingleAsync<AuditsDto>(sql, new { Id = id });

                    // Get entities
                    var entityResult = await _entityRepository.GetAllAsync(id);

                    var audit = result
                        .ToModel();

                    audit.Policy.Entities = entityResult.Content.ToList();

                    response.Content = audit;
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve audit.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<GetResponse<IReadOnlyList<IAudit>>> GetAllAsync(bool activeOnly = false)
        {
            var response = new GetResponse<IReadOnlyList<IAudit>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = !activeOnly
                        ? GetSummarySelectStatement()
                        : $@"{GetSummarySelectStatement()}
                           WHERE AuditStatus NOT IN ({(int)AuditStatuses.Cancelled}, {(int)AuditStatuses.Completed}, {(int)AuditStatuses.NonProductive}, {(int)AuditStatuses.MissingData})";

                    var results = await connection.QueryAsync<AuditsDto>(sql);

                    response.Content = results
                        .Select(x => x.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve active audits.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }
        public Task<GetResponse<IAudit>> GetAsync(AuditRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResponse> UpdateAuditStatus(int auditId, AuditStatuses status, int userId)
        {
            var response = new SaveResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {

                    var sql = @"UPDATE [dbo].[Audits] SET AuditStatus = @auditStatus WHERE AuditId = @Id;
                                INSERT INTO AuditsStatusHistory	(AuditID,AuditStatusID,ChangedDateTime,ChangedByID) VALUES (@Id,@auditStatus,GetDate(),@userId)";
                    var results = await connection.ExecuteAsync(sql, new { id = auditId, auditStatus = (int)status, userId = userId });

                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to audit status.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<SaveResponse> NotifyPolicyHolder(int auditId, int notificationType, DateTime sentDate)
        {
            var response = new SaveResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetPolicyHolderNotificationInsertStatement();

                    var results = await connection.ExecuteAsync(sql, new { auditId, notificationType, sentDate });

                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to audit status.";
                response.AddError(e);
                LogManager.LogError(e, messaage);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        private string GetPolicyHolderNotificationInsertStatement()
        {
            return @"INSERT INTO [dbo].[PH_Notifications] (AuditId, NotificationType, NotificationSent) VALUES (@auditid, @notificationType, @sentDate);";
        }

        #region  Insert Statements

        #endregion Insert Statements

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[Audits] AS T
                USING (VALUES 
                        ( @auditid
                        , @auditstatus
                        , @auditPeriod
                        , @isqcchecked
                        , @qcTimeStamp
                       )
                       ) AS S
                       (
                          [AuditID]
                        , [AuditStatus]
                        , [AuditPeriod]
                        , [IsQCChecked]
                        , [QcTimeStamp]
                       ) ON T.[AuditID] = S.[AuditID]
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AuditStatus] = S.[AuditStatus], T.[IsQCChecked] = S.[IsQCChecked], T.[QcTimeStamp] = S.[QcTimeStamp] 
                        
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement


        #region Select Statements
        protected override string GetSummarySelectStatement()
        {
            return GetSelectStatement();
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [AuditID]
                        , [CarrierID]
                        , [RequestedByUser]
                        , [RequestedBy]
                        , [RequestDate]
                        , [InsuredName]
                        , [CompanyName]
                        , [PolicyAddress]
                        , [PolicyAddress2]
                        , [PolicyCity]
                        , [PolicyState]
                        , [PolicyZip]
                        , [PolicyPhone]
                        , [PolicyEmail]
                        , [DueDate]
                        , [AuditType]
                        , [AuditFreq]
                        , [AuditPeriod]
                        , [PolicyNumber]
                        , [PolicyStart]
                        , [PolicyEnd]
                        , [Split]
                        , [AgentName]
                        , [AgentPhone]
                        , [AgentCompany]
                        , [AgentEmail]
                        , [AgentAddress]
                        , [AgentAddress2]
                        , [AgentCity]
                        , [AgentState]
                        , [AgentZip]
                        , [AuditStatus]
                        , [PercentComplete]
                        , [InvoiceID]
                        , [CompleteDate]
                        , [PHPercentComplete]
                        , [PHCompletedBy]
                        , [PHCompletedByPhone]
                        , [PHComments]
                        , [IsQcChecked]
                        , [QCTimeStamp]
                        , [IsQCComplete]
                        , [IsLastChild]
                        , [PHStartDate]
                        , [QCCompleteDate]
                        , [TotalHoursWorked]
                        , [OrderType]
                        , [AssignedToID]
                        , [HasActivity]
                        , [TotalHoursPaid]
                        , [IsBillable]
                        , [AssignedDate]
                        , [RecordsReceived]
                        , [RecordsComplete]
                        , [SpecialInstructions]
                        , [IsReorder]
                        , [IsReopen]
                        , [IsDispute]
                        , [OriginalAuditID]
                        , [ReorderedToAuditID]
                        , [PHBestTimeToContact]
                        , [PHDateOfBirth]
                        , [CarrierOrderNumber]
                        , [QCReviewerID]
                        , [ByLocation]
                        , [ExposureBasis] = 
                            (SELECT Stuff((SELECT N', ' + ClassCodeBasisID 
			                               FROM
				                                (SELECT DISTINCT AuditId, ClassCodeBasisID
				                                 FROM dbo.ClassCodes2Audit
				                                 WHERE AuditId = a.AuditId) C
			                               FOR XML PATH(''),TYPE)
	                                .value('text()[1]','nvarchar(max)'),1,2,N'')
                            )	
                    FROM [dbo].[Audits] a
                    
                    ";
        }
        #endregion Select Statements
        #endregion Sql Statementw
    }
}