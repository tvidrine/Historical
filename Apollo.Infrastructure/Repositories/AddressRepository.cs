// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/9/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<AddressDto, IAddress, IAddress>, IAddressRepository
    {
        public AddressRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new AddressDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [Common].[Address] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [Common].[Address] AS T
                USING (VALUES 
                        ( @entityId
                        , @line1
                        , @line2
                        , @city
                        , @state
                        , @zipcode
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (  [EntityId]
                        , [Line1]
                        , [Line2]
                        , [City]
                        , [State]
                        , [Zipcode]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [EntityId]
                        , [Line1]
                        , [Line2]
                        , [City]
                        , [State]
                        , [Zipcode]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[EntityId]
                        , S.[Line1]
                        , S.[Line2]
                        , S.[City]
                        , S.[State]
                        , S.[Zipcode]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Line1] = S.[Line1], T.[Line2] = S.[Line2], T.[City] = S.[City], T.[State] = S.[State], T.[Zipcode] = S.[Zipcode], 
                        T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectByParentIdStatement()
        {
            return $@"
                    SELECT 
                          [Id]
                        , [EntityId]
                        , [Line1]
                        , [Line2]
                        , [City]
                        , [State]
                        , [Zipcode]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [Common].[Address]
                    WHERE IsDeleted = 0
                        AND EntityId = @parentId;
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



