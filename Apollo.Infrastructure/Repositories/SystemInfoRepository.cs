// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/9/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;
using Dapper;

namespace Apollo.Infrastructure.Repositories
{
    public class SystemInfoRepository : BaseRepository<SystemInfoDto, ISystemInfo, ISystemInfo>, ISystemInfoRepository
    {
        public SystemInfoRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new SystemInfoDto())
        {
        }
        public async Task<GetResponse<ISystemInfo>> GetAsync()
        {
            var response = new GetResponse<ISystemInfo>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetSelectStatement();

                    var result = await connection.QuerySingleAsync<SystemInfoDto>(sql);

                    response.Content = result
                        .ToModel();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve System Information record";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[SystemInfo] AS T
                USING (VALUES 
                        ( @id
                        , @appversion
                        , @applastupdated
                        , @dbschemaversion
                        , @dbschemalastupdated
                        , @showmaintenancemessage
                        , @maintenancemessage
                        , @lastupdated
                        , @lastupdatedby
                        )
                       ) AS S
                       (
                          [Id]
                        , [AppVersion]
                        , [AppLastUpdated]
                        , [DbSchemaVersion]
                        , [DbSchemaLastUpdated]
                        , [ShowMaintenanceMessage]
                        , [MaintenanceMessage]
                        , [LastUpdated]
                        , [LastUpdatedBy]
                       ) ON EXISTS(SELECT * FROM [v2].[SystemInfo])
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AppVersion]
                        , [AppLastUpdated]
                        , [DbSchemaVersion]
                        , [DbSchemaLastUpdated]
                        , [ShowMaintenanceMessage]
                        , [MaintenanceMessage]
                        , [LastUpdated]
                        , [LastUpdatedBy]
                           )
                    VALUES (
                          S.[AppVersion]
                        , S.[AppLastUpdated]
                        , S.[DbSchemaVersion]
                        , S.[DbSchemaLastUpdated]
                        , S.[ShowMaintenanceMessage]
                        , S.[MaintenanceMessage]
                        , S.[LastUpdated]
                        , S.[LastUpdatedBy]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[AppVersion] = S.[AppVersion], T.[AppLastUpdated] = S.[AppLastUpdated], T.[DbSchemaVersion] = S.[DbSchemaVersion], 
                        T.[DbSchemaLastUpdated] = S.[DbSchemaLastUpdated], T.[ShowMaintenanceMessage] = S.[ShowMaintenanceMessage], 
                        T.[MaintenanceMessage] = S.[MaintenanceMessage],T.[LastUpdated] = S.[LastUpdated], 
                        T.[LastUpdatedBy] = S.[LastUpdatedBy] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [AppVersion]
                        , [AppLastUpdated]
                        , [DbSchemaVersion]
                        , [DbSchemaLastUpdated]
                        , [ShowMaintenanceMessage]
                        , [MaintenanceMessage]
                        , [LastUpdated]
                        , [LastUpdatedBy]
                    FROM [v2].[SystemInfo]
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements

        
    }
}



