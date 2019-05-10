// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 1/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Client;

namespace Apollo.Infrastructure.Repositories
{
    public class ClientSettingRepository : BaseRepository<ClientSettingDto, IClientSetting, IClientSetting>, IClientSettingRepository
    {
        public ClientSettingRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new ClientSettingDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[ClientSetting] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [v2].[ClientSetting] AS T
                USING (VALUES 
                        ( @id
                        , @clientid
                        , @settingtype
                        , @settingvalue
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [ClientId]
                        , [SettingType]
                        , [SettingValue]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[ClientId] = S.[ClientId] AND T.[SettingType] = S.[SettingType] AND IsDeleted = 0
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [ClientId]
                        , [SettingType]
                        , [SettingValue]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[ClientId]
                        , S.[SettingType]
                        , S.[SettingValue]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[SettingValue] = S.[SettingValue], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectByParentIdStatement()
        {
            return $@"{GetSelectStatement()} AND ClientId = @parentId";
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [ClientId]
                        , [SettingType]
                        , [SettingValue]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[ClientSetting]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



