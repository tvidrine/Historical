// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class QcRepositoryV1 : AbstractBaseRepository, IQcRepository
    {
        public QcRepositoryV1(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager)
        {
        }

        #region Public Methods
        public async Task<DeleteResponse> DeleteQcDataByAuditIdAsync(int auditId)
        {
            var response = new DeleteResponse();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetQcDataDeleteStatement();
                    var results = await connection.ExecuteAsync(sql, new { AuditId = auditId });

                    response.Message = $@"{results} Qc Data records were deleted.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error deleting Qc Data records";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }
        #endregion

        #region QcData Sql Statements
        #region Delete Statement
        protected string GetQcDataDeleteStatement()
        {
            return @"
                DELETE FROM [dbo].[QC_Data] WHERE [AuditId] = @auditId;
                DELETE FROM [dbo].[PH_CompletedEntities] WHERE [AuditId] = @auditId;";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected string GetQcDataMergeStatement()
        {
            return @"
                MERGE [dbo].[QC_Data] AS T
                USING (VALUES 
                        ( @qcid
                        , @auditid
                        , @entityid
                        , @hasentitychange
                        , @hasofficerchange
                        , @haslocationchange
                        , @hasqmquestions
                        , @hasprvarianceversusee
                        , @has941issues
                        , @hascasuallabor
                        , @hasuisubs
                        , @has941uploadissues
                        , @iscleared
                        , @hasentitychangecleared
                        , @hasofficerchangecleared
                        , @haslocationchangecleared
                        , @hasqmquestionscleared
                        , @hasprvarianceversuseecleared
                        , @has941issuescleared
                        , @hascasuallaborcleared
                        , @hasuisubscleared
                        , @has941uploadissuescleared
                        , @wpdarchfilename
                        , @wpdarchdate
                        )
                       ) AS S
                       (
                          [QCID]
                        , [AuditID]
                        , [EntityID]
                        , [HasEntityChange]
                        , [HasOfficerChange]
                        , [HasLocationChange]
                        , [HasQMQuestions]
                        , [HasPRVarianceVersusEE]
                        , [Has941Issues]
                        , [HasCasualLabor]
                        , [HasUISubs]
                        , [Has941UploadIssues]
                        , [IsCleared]
                        , [HasEntityChangeCleared]
                        , [HasOfficerChangeCleared]
                        , [HasLocationChangeCleared]
                        , [HasQMQuestionsCleared]
                        , [HasPRVarianceVersusEECleared]
                        , [Has941IssuesCleared]
                        , [HasCasualLaborCleared]
                        , [HasUISubsCleared]
                        , [Has941UploadIssuesCleared]
                        , [WPDArchFileName]
                        , [WPDArchDate]
                       ) ON T.[QCID] = S.[QCID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditID]
                        , [EntityID]
                        , [HasEntityChange]
                        , [HasOfficerChange]
                        , [HasLocationChange]
                        , [HasQMQuestions]
                        , [HasPRVarianceVersusEE]
                        , [Has941Issues]
                        , [HasCasualLabor]
                        , [HasUISubs]
                        , [Has941UploadIssues]
                        , [IsCleared]
                        , [HasEntityChangeCleared]
                        , [HasOfficerChangeCleared]
                        , [HasLocationChangeCleared]
                        , [HasQMQuestionsCleared]
                        , [HasPRVarianceVersusEECleared]
                        , [Has941IssuesCleared]
                        , [HasCasualLaborCleared]
                        , [HasUISubsCleared]
                        , [Has941UploadIssuesCleared]
                        , [WPDArchFileName]
                        , [WPDArchDate]
                           )
                    VALUES (
                          S.[AuditID]
                        , S.[EntityID]
                        , S.[HasEntityChange]
                        , S.[HasOfficerChange]
                        , S.[HasLocationChange]
                        , S.[HasQMQuestions]
                        , S.[HasPRVarianceVersusEE]
                        , S.[Has941Issues]
                        , S.[HasCasualLabor]
                        , S.[HasUISubs]
                        , S.[Has941UploadIssues]
                        , S.[IsCleared]
                        , S.[HasEntityChangeCleared]
                        , S.[HasOfficerChangeCleared]
                        , S.[HasLocationChangeCleared]
                        , S.[HasQMQuestionsCleared]
                        , S.[HasPRVarianceVersusEECleared]
                        , S.[Has941IssuesCleared]
                        , S.[HasCasualLaborCleared]
                        , S.[HasUISubsCleared]
                        , S.[Has941UploadIssuesCleared]
                        , S.[WPDArchFileName]
                        , S.[WPDArchDate]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AuditID] = S.[AuditID], T.[EntityID] = S.[EntityID], T.[HasEntityChange] = S.[HasEntityChange], T.[HasOfficerChange] = S.[HasOfficerChange], T.[HasLocationChange] = S.[HasLocationChange], 
                        T.[HasQMQuestions] = S.[HasQMQuestions], T.[HasPRVarianceVersusEE] = S.[HasPRVarianceVersusEE], T.[Has941Issues] = S.[Has941Issues], T.[HasCasualLabor] = S.[HasCasualLabor], T.[HasUISubs] = S.[HasUISubs], 
                        T.[Has941UploadIssues] = S.[Has941UploadIssues], T.[IsCleared] = S.[IsCleared], T.[HasEntityChangeCleared] = S.[HasEntityChangeCleared], T.[HasOfficerChangeCleared] = S.[HasOfficerChangeCleared], T.[HasLocationChangeCleared] = S.[HasLocationChangeCleared], 
                        T.[HasQMQuestionsCleared] = S.[HasQMQuestionsCleared], T.[HasPRVarianceVersusEECleared] = S.[HasPRVarianceVersusEECleared], T.[Has941IssuesCleared] = S.[Has941IssuesCleared], T.[HasCasualLaborCleared] = S.[HasCasualLaborCleared], T.[HasUISubsCleared] = S.[HasUISubsCleared], 
                        T.[Has941UploadIssuesCleared] = S.[Has941UploadIssuesCleared], T.[WPDArchFileName] = S.[WPDArchFileName], T.[WPDArchDate] = S.[WPDArchDate] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected string GetQcDataSelectStatement()
        {
            return @"
                    SELECT 
                          [QCID]
                        , [AuditID]
                        , [EntityID]
                        , [HasEntityChange]
                        , [HasOfficerChange]
                        , [HasLocationChange]
                        , [HasQMQuestions]
                        , [HasPRVarianceVersusEE]
                        , [Has941Issues]
                        , [HasCasualLabor]
                        , [HasUISubs]
                        , [Has941UploadIssues]
                        , [IsCleared]
                        , [HasEntityChangeCleared]
                        , [HasOfficerChangeCleared]
                        , [HasLocationChangeCleared]
                        , [HasQMQuestionsCleared]
                        , [HasPRVarianceVersusEECleared]
                        , [Has941IssuesCleared]
                        , [HasCasualLaborCleared]
                        , [HasUISubsCleared]
                        , [Has941UploadIssuesCleared]
                        , [WPDArchFileName]
                        , [WPDArchDate]
                    FROM [dbo].[QC_Data]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}
