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
    public class ContactRepository : BaseRepository<ContactDto, IContact, IContact>, IContactRepository
    {
        public ContactRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new ContactDto())
        {
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [Common].[Contact] SET IsDeleted = 1
                WHERE [Id] = @id";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [Common].[Contact] AS T
                USING (VALUES 
                        ( @id
                        , @entityId
                        , @contacttype
                        , @email
                        , @faxnumber
                        , @name
                        , @phonenumber
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [EntityId]
                        , [ContactType]
                        , [Email]
                        , [FaxNumber]
                        , [Name]
                        , [PhoneNumber]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[Id] = S.[Id]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Id]
                        , [EntityId]
                        , [ContactType]
                        , [Email]
                        , [FaxNumber]
                        , [Name]
                        , [PhoneNumber]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[Id]
                        , S.[EntityId]
                        , S.[ContactType]
                        , S.[Email]
                        , S.[FaxNumber]
                        , S.[Name]
                        , S.[PhoneNumber]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[ContactType] = S.[ContactType], T.[Email] = S.[Email], T.[FaxNumber] = S.[FaxNumber], T.[Name] = S.[Name], T.[PhoneNumber] = S.[PhoneNumber], 
                        T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectByParentIdStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [EntityId]
                        , [ContactType]
                        , [Email]
                        , [FaxNumber]
                        , [Name]
                        , [PhoneNumber]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [Common].[Contact]
                    WHERE IsDeleted = 0 
                        AND EntityId = @parentId;
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



