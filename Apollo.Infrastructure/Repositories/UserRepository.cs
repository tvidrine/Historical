using System;
using System.Threading.Tasks;
using Dapper;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models;

namespace Apollo.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserDto, IUser,  IUserInfo>, IUserRepository
    {
        public UserRepository(IConnectionFactory connectionFactory, ILogManager loggerManager) :
            base(connectionFactory, loggerManager, new UserDto())
        {
        }

        public async Task<GetResponse<IUser>> GetByEmailAsync(string email)
        {
            var response = new GetResponse<IUser>();

            try
            {
                var sql = $@"{GetSelectStatement()}
                        AND [NormalizedEmail] = @email";
                var parms = new {email};

                using (var connection = ConnectionFactory.GetConnection())
                {
                    var result = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, parms);

                    if (result != null) response.Content = result.ToModel();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
                response.Message = $@"Unable to retrieve user with email address: {email}";
                LogManager.LogError(e, $@"Error retrieving user by email address: {email}");
            }

            return response;
        }

        public async Task<GetResponse<IUser>> GetByUserIdAsync(string userId)
        {
            var response = new GetResponse<IUser>();

            try
            {
                var sql = $@"{GetSelectStatement()}
                        AND [Id] = @id";
                var parms = new {id = userId};

                using (var connection = ConnectionFactory.GetConnection())
                {
                    var result = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, parms);

                    if (result != null) response.Content = result.ToModel();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
                response.Message = $@"Unable to retrieve user with user id: {userId}";
                LogManager.LogError(e, $@"Error retrieving user by user id: {userId}");
            }

            return response;
        }

        public async Task<GetResponse<IUser>> GetByUserNameAsync(string userName)
        {
            var response = new GetResponse<IUser>();

            try
            {
                var sql = $@"{GetSelectStatement()}
                        AND [Login] = @login";
                var parms = new {Login = userName};

                using (var connection = ConnectionFactory.GetConnection())
                {
                    var result = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, parms);

                    if (result != null) response.Content = result.ToModel();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
                response.Message = $@"Unable to retrieve user with user name: {userName}";
                LogManager.LogError(e, $@"Error retrieving user by username: {userName}");
            }

            return response;
        }

        public async Task<SaveResponse> SaveSessionStateAsync(IUser user)
        {
            var response = new SaveResponse();

            try
            {
                const string sql =
                    @"UPDATE [Identity].[User] SET [LastAccessedDate] = SYSDATETIMEOFFSET() WHERE [Id] = @id;
                      INSERT INTO [Identity].[UserSession](UserId, LoginDate) VALUES(@id, SYSDATETIMEOFFSET());";
                var parms = new { id = user.Id };

                using (var connection = ConnectionFactory.GetConnection())
                {
                    var x = await connection.ExecuteAsync(sql, parms);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.AddError(e);
                LogManager.LogError(e, $@"Error saving login state for user: {user.Email}.");
            }

            return response;
        }

        #region Sql Statements
        protected override string GetDeleteStatement()
        {
            return $@"
                UPDATE [Identity].[User] SET IsDeleted = 1 WHERE [Id] = @id;
                ";
        }

        protected override string GetMergeStatement()
        {
            return @"
                MERGE [Identity].[User] AS T
                USING (VALUES
                        ( @id
                        , @accessFailedCount
                        , @email
                        , @isActive
                        , @isEmailConfirmed
                        , @isLocked
                        , @isLockoutEnabled
                        , @isPhoneNumberConfirmed
                        , @isTwoFactorEnabled
                        , @lastAccessedDate
                        , @lockoutEnd
                        , @firstName
                        , @middleInitial
                        , @lastName
                        , @lastPasswordChangedDate
                        , @normalizedEmail
                        , @normalizedUserName
                        , @passwordhash
                        , @phoneNumber
                        , @userName
                        , @notes
                        , SYSDATETIMEOFFSET()
                        , @createdById
                        , SYSDATETIMEOFFSET()
                        , @lastModifiedById
                        )
                    ) AS S
                    ( 
                          [Id]
                        , [AccessFailedCount]
                        , [Email]
                        , [IsActive]
                        , [IsEmailConfirmed]
                        , [IsLocked]
                        , [IsLockoutEnabled]
                        , [IsPhoneNumberConfirmed]
                        , [IsTwoFactorEnabled]
                        , [LastAccessedDate]
                        , [LockoutEnd]
                        , [FirstName]
                        , [MiddleInitial]
                        , [LastName]
                        , [LastPasswordChangedDate]
                        , [NormalizedEmail]
                        , [NormalizedUserName] 
                        , [PasswordHash]
                        , [PhoneNumber]
                        , [UserName]
                        , [Notes]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    ) ON T.[UserName] = S.[UserName]
                WHEN NOT MATCHED THEN
                    INSERT
                        ( [Id]
                        , [AccessFailedCount]
                        , [Email]
                        , [IsActive]
                        , [IsEmailConfirmed]
                        , [IsLocked]
                        , [IsLockoutEnabled]
                        , [IsPhoneNumberConfirmed]
                        , [IsTwoFactorEnabled]
                        , [LastAccessedDate]
                        , [LockoutEnd]
                        , [FirstName]
                        , [MiddleInitial]
                        , [LastName]
                        , [LastPasswordChangedDate]
                        , [NormalizedEmail]
                        , [NormalizedUserName] 
                        , [PasswordHash]
                        , [PhoneNumber]
                        , [UserName]
                        , [Notes]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById])
                    VALUES
                        ( S.[Id]
                        , S.[AccessFailedCount]
                        , S.[Email]
                        , S.[IsActive]
                        , S.[IsEmailConfirmed]
                        , S.[IsLocked]
                        , S.[IsLockoutEnabled]
                        , S.[IsPhoneNumberConfirmed]
                        , S.[IsTwoFactorEnabled]
                        , S.[LastAccessedDate]
                        , S.[LockoutEnd]
                        , S.[FirstName]
                        , S.[MiddleInitial]
                        , S.[LastName]
                        , S.[LastPasswordChangedDate]
                        , S.[NormalizedEmail]
                        , S.[NormalizedUserName] 
                        , S.[PasswordHash]
                        , S.[PhoneNumber]
                        , S.[UserName]
                        , S.[Notes]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById])
                WHEN MATCHED THEN
                    UPDATE SET
                        T. [AccessFailedCount] = S.[AccessFailedCount], T.[Email] = S.[Email], T.[IsActive] = S.[IsActive], T.[IsEmailConfirmed] = S.[IsEmailConfirmed], T.[IsLocked] = S.[IsLocked], T.[IsLockoutEnabled] = S.[IsLockoutEnabled], 
                        T.[IsPhoneNumberConfirmed] = S.[IsPhoneNumberConfirmed], T.[IsTwoFactorEnabled] = S.[IsTwoFactorEnabled], T.[LastAccessedDate] = S.[LastAccessedDate],  T.[LockoutEnd] = S.[LockoutEnd], 
                        T.[FirstName] = S.[FirstName], T.[MiddleInitial] = S.[MiddleInitial], T.[LastName] = S.[LastName], T.[LastPasswordChangedDate] = S.[LastPasswordChangedDate], T.[NormalizedEmail] = S.[NormalizedEmail], 
                        T.[NormalizedUserName] = S.[NormalizedUserName], T.[PasswordHash] = S.[PasswordHash], T.[PhoneNumber] = S.[PhoneNumber], T.[Notes] = S.[Notes], 
                        T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById]
                OUTPUT inserted.*;";
        }

        protected override string GetSelectStatement()
        {
            return @"
                    SELECT [Id]
                        , [AccessFailedCount]
                        , [Email]
                        , [IsActive]
                        , [IsEmailConfirmed]
                        , [IsLocked]
                        , [IsLockoutEnabled]
                        , [IsPhoneNumberConfirmed]
                        , [IsTwoFactorEnabled]
                        , [LastAccessedDate]
                        , [LockoutEnd]
                        , [FirstName]
                        , [MiddleInitial]
                        , [LastName]
                        , [LastPasswordChangedDate]
                        , [NormalizedEmail]
                        , [PasswordHash]
                        , [PhoneNumber]
                        , [UserName]
                        , [Notes]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                      FROM [Identity].[User]
                      WHERE IsDeleted = 0 
                    ";
        }

        protected override string GetSummarySelectStatement()
        {
            return @"
                 SELECT [Id]
                        , [Email]
                        , [IsActive]
                        , [IsLocked]
                        , [FirstName]
                        , [MiddleInitial]
                        , [LastName]
                 FROM [Identity].[User]
                 WHERE IsDeleted = 0;
                    ";
        }
        #endregion Sql Statements
    }
}