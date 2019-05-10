// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Repositories;
using Apollo.Infrastructure.v1.Models;
using Dapper;

namespace Apollo.Infrastructure.v1.Repositories
{
    public class AuditUploadRepository : BaseRepository<FileUploadDto, IAuditUpload, IAuditUpload>, IAuditUploadRepository
    {
        public AuditUploadRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new FileUploadDto())
        {
        }

        public override async Task<GetResponse<IAuditUpload>> GetByIdAsync(int id)
        {
            var response = new GetResponse<IAuditUpload>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetSelectStatement()}
								 AND AuditUploadsID = @id;";

                    var result = await connection.QuerySingleAsync<FileUploadDto>(sql, new { Id = id });

                    response.Content = result
                        .ToModel();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve file upload records";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [dbo].[AuditUploads] SET IsDeleted = 1
                WHERE [AuditUploadsID] = @audituploadsid";
        }
        #endregion Delete Statement

        #region Merge Statement
        protected override string GetMergeStatement()
        {
            return @"
                MERGE [dbo].[AuditUploads] AS T
                USING (VALUES 
                        ( @audituploadsid
                        , @title
                        , @date
                        , @size
                        , @attachedby
                        , @type
                        , @description
                        , @auditid
                        , @entityid
                        , @filepath
                        , @directory
                        , @originalfilename
                        , @filedata
                        )
                       ) AS S
                       (
                          [AuditUploadsID]
                        , [Title]
                        , [Date]
                        , [Size]
                        , [AttachedBy]
                        , [Type]
                        , [Description]
                        , [AuditID]
                        , [EntityID]
                        , [FilePath]
                        , [Directory]
                        , [OriginalFileName]
                        , [FileData]
                       ) ON T.[AuditUploadsID] = S.[AuditUploadsID]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [Title]
                        , [Date]
                        , [Size]
                        , [AttachedBy]
                        , [Type]
                        , [Description]
                        , [AuditID]
                        , [EntityID]
                        , [FilePath]
                        , [Directory]
                        , [OriginalFileName]
                        , [FileData]
                           )
                    VALUES (
                          S.[Title]
                        , S.[Date]
                        , S.[Size]
                        , S.[AttachedBy]
                        , S.[Type]
                        , S.[Description]
                        , S.[AuditID]
                        , S.[EntityID]
                        , S.[FilePath]
                        , S.[Directory]
                        , S.[OriginalFileName]
                        , S.[FileData]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Title] = S.[Title], T.[Date] = S.[Date], T.[Size] = S.[Size], T.[AttachedBy] = S.[AttachedBy], T.[Type] = S.[Type], 
                        T.[Description] = S.[Description], T.[AuditID] = S.[AuditID], T.[EntityID] = S.[EntityID], T.[FilePath] = S.[FilePath], 
                        T.[Directory] = S.[Directory], T.[OriginalFileName] = S.[OriginalFileName], T.[FileData] = S.[FileData]
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        protected override string GetSelectStatement()
        {
            return @"
                    SELECT 
                          [AuditUploadsID]
                        , [Title]
                        , [Date]
                        , [Size]
                        , [AttachedBy]
                        , [Type]
                        , [Description]
                        , [AuditID]
                        , [EntityID]
                        , [FilePath]
                        , [Directory]
                        , [OriginalFileName]
                        , [FileData]
                    FROM [dbo].[AuditUploads]
                    WHERE IsDeleted = 0 
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements
    }
}



