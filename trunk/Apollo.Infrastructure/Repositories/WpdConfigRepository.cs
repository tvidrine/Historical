// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/3/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories
{
    public class WpdConfigRepository : BaseRepository<WpdConfigDto, IWpdConfig, IWpdConfig>, IWpdConfigRepository
    {
        public WpdConfigRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new WpdConfigDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [Carrier].[WpdConfig] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [Carrier].[WpdConfig] AS T
                USING (VALUES 
                        ( @id
                        , @clientid
                        , @deliverymethod
                        , @options
                        , @deliverto
                        , @fileformats
                        , @documenttypes
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [ClientId]
                        , [DeliveryMethod]
                        , [Options]
                        , [DeliverTo]
                        , [FileFormats]
                        , [DocumentTypes]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Id]
                        , [ClientId]
                        , [DeliveryMethod]
                        , [Options]
                        , [DeliverTo]
                        , [FileFormats]
                        , [DocumentTypes]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Id]
                        , S.[ClientId]
                        , S.[DeliveryMethod]
                        , S.[Options]
                        , S.[DeliverTo]
                        , S.[FileFormats]
                        , S.[DocumentTypes]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[ClientId] = S.[ClientId], T.[DeliveryMethod] = S.[DeliveryMethod], T.[Options] = S.[Options], T.[DeliverTo] = S.[DeliverTo], T.[FileFormats] = S.[FileFormats], 
                        T.[DocumentTypes] = S.[DocumentTypes], T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], 
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
                        , [ClientId]
                        , [DeliveryMethod]
                        , [Options]
                        , [DeliverTo]
                        , [FileFormats]
                        , [DocumentTypes]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [Carrier].[WpdConfig]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



