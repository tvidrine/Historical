// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/6/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Client;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<ClientDto,IClient, IClientInfo>
    {
        public ClientRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new ClientDto())
        {
            
        }

        public Task<GetResponse<IReadOnlyList<ClientConfiguration>>> GetConfigurationsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetResponse<IReadOnlyList<IClientInfo>>> GetInfoListAsync(ClientSettingsEnum settings)
        {
            var response = new GetResponse<IReadOnlyList<IClientInfo>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetSelectInfoStatement();

                    var results = await connection.QueryAsync<ClientDto>(sql);

                    response.Content = results
                        .Select(dto => dto.ToInfo())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var messaage = $@"Unable to retrieve client records.";
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
                UPDATE [Carrier].[Client] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [Carrier].[Client] AS T
                USING (VALUES 
                        ( @id
                        , @name
                        , @audittype
                        , @clienttype
                        , @processtype
                        , @parentclientid
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [Name]
                        , [AuditType]
                        , [ClientType]
                        , [ProcessType]
                        , [ParentClientId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Id]
                        , [Name]
                        , [AuditType]
                        , [ClientType]
                        , [ProcessType]
                        , [ParentClientId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Id]
                        , S.[Name]
                        , S.[AuditType]
                        , S.[ClientType]
                        , S.[ProcessType]
                        , S.[ParentClientId]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Name] = S.[Name], T.[AuditType] = S.[AuditType], T.[ClientType] = S.[ClientType], T.[ProcessType] = S.[ProcessType], T.[ParentClientId] = S.[ParentClientId], 
                        T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                        ;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [Name]
                        , [AuditType]
                        , [ClientType]
                        , [ProcessType]
                        , [ParentClientId]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [Carrier].[Client]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #region Select Statement
        protected override string GetSummarySelectStatement()
        {
            return @"
                    SELECT
	                      [Id] = c.Id
	                    , [Name] =c.[Name]
	                    , [ParentName] = ISNULL(p.[Name],'')
	                    , [ContactName] = ISNULL(ct.[Name], '')
	                    , [ContactPhone] = ISNULL(ct.[PhoneNumber], '')
	                    , [Address] =  ISNULL(a.[Line1], '')
	                    , [City] = ISNULL(a.[City], '')
	                    , [State] = ISNULL(a.[State], '')
                    FROM [Carrier].[Client] c 
	                    LEFT JOIN [Carrier].[Client] p ON c.ParentClientId = p.Id AND p.IsDeleted = 0 
	                    LEFT JOIN [Common].[Address] a ON c.Id = a.EntityId AND a.IsDeleted = 0
	                    LEFT JOIN [Common].[Contact] ct ON c.Id = ct.EntityId AND ct.ContactType = 2 AND ct.IsDeleted = 0
                    WHERE c.IsDeleted = 0;
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
        
    }
}



