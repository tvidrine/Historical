// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Audit;

namespace Apollo.Infrastructure.Repositories
{
    public class AuditRepository : BaseRepository<AuditDto, IAudit, IAudit>
    {
        public AuditRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new AuditDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [Policy].[Audit] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [Policy].[Audit] AS T
                USING (VALUES 
                        ( @id
                        , @assignedon
                        , @assignmentnumber
                        , @auditorid
                        , @auditdate
                        , @completeddate
                        , @invoiceid
                        , @policyid
                        , @requestedbyid
                        , @requestedon
                        , @startdate
                        , @auditstatusid
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [AssignedOn]
                        , [AssignmentNumber]
                        , [AuditorId]
                        , [AuditDate]
                        , [CompletedDate]
                        , [InvoiceId]
                        , [PolicyId]
                        , [RequestedById]
                        , [RequestedOn]
                        , [StartDate]
                        , [AuditStatusId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Id]
                        , [AssignedOn]
                        , [AssignmentNumber]
                        , [AuditorId]
                        , [AuditDate]
                        , [CompletedDate]
                        , [InvoiceId]
                        , [PolicyId]
                        , [RequestedById]
                        , [RequestedOn]
                        , [StartDate]
                        , [AuditStatusId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Id]
                        , S.[AssignedOn]
                        , S.[AssignmentNumber]
                        , S.[AuditorId]
                        , S.[AuditDate]
                        , S.[CompletedDate]
                        , S.[InvoiceId]
                        , S.[PolicyId]
                        , S.[RequestedById]
                        , S.[RequestedOn]
                        , S.[StartDate]
                        , S.[AuditStatusId]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AssignedOn] = S.[AssignedOn], T.[AssignmentNumber] = S.[AssignmentNumber], T.[AuditorId] = S.[AuditorId], T.[AuditDate] = S.[AuditDate], T.[CompletedDate] = S.[CompletedDate], 
                        T.[InvoiceId] = S.[InvoiceId], T.[PolicyId] = S.[PolicyId], T.[RequestedById] = S.[RequestedById], T.[RequestedOn] = S.[RequestedOn], T.[StartDate] = S.[StartDate], 
                        T.[AuditStatusId] = S.[AuditStatusId], T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], 
                        T.[LastModifiedById] = S.[LastModifiedById]
                OUTPUT inserted.*;"; 
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [AssignedOn]
                        , [AssignmentNumber]
                        , [AuditorId]
                        , [AuditDate]
                        , [CompletedDate]
                        , [InvoiceId]
                        , [PolicyId]
                        , [RequestedById]
                        , [RequestedOn]
                        , [StartDate]
                        , [AuditStatusId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [Policy].[Audit]
                    WHERE IsDeleted = 0 
                    ";
        }

        protected override string GetSummarySelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [AssignedOn]
                        , [AssignmentNumber]
                        , [AuditorId]
                        , [AuditDate]
                        , [CompletedDate]
                        , [InvoiceId]
                        , [PolicyId]
                        , [RequestedById]
                        , [RequestedOn]
                        , [StartDate]
                        , [AuditStatusId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [Policy].[Audit]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements

        public Task<GetResponse<IAudit>> GetAsync(AuditRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}



