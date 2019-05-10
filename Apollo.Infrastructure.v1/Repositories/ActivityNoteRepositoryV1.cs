// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class ActivityNoteRepositoryV1 : BaseRepository<ActivityNoteDto, IActivityNote, IActivityNoteInfo>, IActivityNoteRepository
    {
        public ActivityNoteRepositoryV1(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new ActivityNoteDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [dbo].[Admin_ActivityLog] SET IsDeleted = 1
                WHERE [ALID] = @alid";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[Admin_ActivityLog] AS T
                USING (VALUES 
                        ( @alid
                        , @auditid
                        , @entityid
                        , @activitytypeid
                        , @actiontypeid
                        , @activitydate
                        , @userid
                        , @activitydescription
                        , @tofromid
                        , @emailorphone
                        , @callresultid
                        , @iscompleted
                        , @createddate
                        )
                       ) AS S
                       (
                          [ALID]
                        , [AuditID]
                        , [EntityID]
                        , [ActivityTypeID]
                        , [ActionTypeID]
                        , [ActivityDate]
                        , [UserID]
                        , [ActivityDescription]
                        , [ToFromID]
                        , [EmailorPhone]
                        , [CallResultID]
                        , [IsCompleted]
                        , [CreatedDate]
                       ) ON T.[ALID] = S.[ALID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditID]
                        , [EntityID]
                        , [ActivityTypeID]
                        , [ActionTypeID]
                        , [ActivityDate]
                        , [UserID]
                        , [ActivityDescription]
                        , [ToFromID]
                        , [EmailorPhone]
                        , [CallResultID]
                        , [IsCompleted]
                        , [CreatedDate]
                           )
                    VALUES (
                          S.[AuditID]
                        , S.[EntityID]
                        , S.[ActivityTypeID]
                        , S.[ActionTypeID]
                        , S.[ActivityDate]
                        , S.[UserID]
                        , S.[ActivityDescription]
                        , S.[ToFromID]
                        , S.[EmailorPhone]
                        , S.[CallResultID]
                        , S.[IsCompleted]
                        , S.[CreatedDate]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AuditID] = S.[AuditID], T.[EntityID] = S.[EntityID], T.[ActivityTypeID] = S.[ActivityTypeID], T.[ActionTypeID] = S.[ActionTypeID], T.[ActivityDate] = S.[ActivityDate], 
                        T.[UserID] = S.[UserID], T.[ActivityDescription] = S.[ActivityDescription], T.[ToFromID] = S.[ToFromID], T.[EmailorPhone] = S.[EmailorPhone], T.[CallResultID] = S.[CallResultID], 
                        T.[IsCompleted] = S.[IsCompleted], T.[CreatedDate] = S.[CreatedDate] 
                OUTPUT inserted.*;
                
                UPDATE dbo.Audits SET HasActivity = 1 WHERE AuditId = @auditId;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [ALID]
                        , [AuditID]
                        , [EntityID]
                        , [ActivityTypeID]
                        , [ActionTypeID]
                        , [ActivityDate]
                        , [UserID]
                        , [ActivityDescription]
                        , [ToFromID]
                        , [EmailorPhone]
                        , [CallResultID]
                        , [IsCompleted]
                        , [CreatedDate]
                    FROM [dbo].[Admin_ActivityLog]
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



