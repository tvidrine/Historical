// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Apollo.Infrastructure.Factories;
using Apollo.Infrastructure.Models.Document;

namespace Apollo.Infrastructure.Repositories
{
    public class DocumentRepository : BaseRepository<DocumentDto,IDocument, IDocument>, IDocumentRepository
    {
        public DocumentRepository(IConnectionFactory connectionFactory, ILogManager logManager)
            : base(connectionFactory, logManager, new DocumentDto())
        {
        }

        public override async Task<GetResponse<IReadOnlyList<IDocument>>> GetAllAsync()
        {
            var response = new GetResponse<IReadOnlyList<IDocument>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetDocumentSelectStatement()};
                                 {GetDocumentFieldSelectStatement()}";

                    var results = await connection.QueryMultipleAsync(sql);

                    var documents = results.Read<DocumentDto>()
                        .Select(dto => dto.ToModel())
                        .AsList();

                    var fields = results.Read<MergeDocumentFieldDto>()
                        .AsList();

                    foreach (var document in documents)
                    {
                        document.Fields = fields.Where(f => f.DocumentId == document.Id)
                            .Select(dto => dto.ToModel())
                            .AsList();
                    }

                    response.Content = documents;

                    if (response.Content == null)
                    {
                        response.AddError($@"Unable to locate any merge documents.");
                    }

                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to locate any merge documents.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public override async Task<GetResponse<IDocument>> GetByIdAsync(int id)
        {
            var response = new GetResponse<IDocument>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetDocumentSelectStatement()} AND Id = @id;
                                 {GetDocumentFieldSelectStatement()} AND d.Id = @id";

                    var results = await connection.QueryMultipleAsync(sql, new { Id = id });
                    response = GetResponse(results);

                    if (response.Content == null)
                    {
                        response.AddError($@"Unable to locate a merge document with Id: {id}");
                    }

                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve merge document with Id: {id}.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }
        public async Task<GetResponse<IDocument>> GetByTypeAsync(int clientId, DocumentTypes documentType)
        {
            var response = new GetResponse<IDocument>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetDocumentSelectStatement()} AND [TypeId] = @typeId AND ([ClientId] = @clientId OR [ClientId] = 0) ORDER BY ClientID DESC;
                                 {GetDocumentFieldSelectStatement()} AND d.TypeId = @typeId AND (d.[ClientId] = @clientId OR d.[ClientId] = 0)";

                    var results = await connection.QueryMultipleAsync(sql, new { TypeId = (int)documentType, ClientId = clientId });
                    response = GetResponse(results);

                    if (response.Content == null)
                    {
                        response.AddError($@"Unable to locate a document with Type: {documentType} for client with ID: {clientId}");
                    }

                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve a document with Type: {documentType} for client with ID: {clientId}.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public GetResponse<IList<IDocumentHistory>> GetDocumentHistory(DocumentHistoryRequest request)
        {
            return GetDocumentHistoryAsync(request).Result;
        }
        public async Task<GetResponse<IList<IDocumentHistory>>> GetDocumentHistoryAsync(DocumentHistoryRequest request)
        {
            var response = new GetResponse<IList<IDocumentHistory>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var conditions = new List<string>();

                    if(request.PeriodStart.HasValue)
                        conditions.Add($@"CreatedOn BETWEEN {request.PeriodStart:d} AND {request.PeriodEnd:d};");

                    if(request.AuditId > 0)
                        conditions.Add($@"AuditId = {request.AuditId}");

                    var condition = string.Join(" AND ", conditions);

                    var sql = string.IsNullOrEmpty(condition)
                        ? $@"{GetDocumentHistorySelectStatement()}"
                        : $@"{GetDocumentHistorySelectStatement()} AND {condition};";
                       

                    

                    var results = await connection.QueryAsync<DocumentHistoryDto>(sql);
                    response.Content = results
                        .Select(d => d.ToModel())
                        .AsList();
                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve document records for request: {request}";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }
            return response;
        }

        public async Task<GetResponse<IList<IDocumentFieldValue>>> GetFieldValuesAsync(int clientId, DocumentTypes documentType)
        {
            var response = new GetResponse<IList<IDocumentFieldValue>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = $@"{GetDocumentFieldValueSelectStatement()}";

                    var results = await connection.QueryAsync<MergeDocumentFieldValueDto>(sql, new { ClientId = clientId, TypeId = (int)documentType });

                    response.Content = results
                        .Select(dto => dto.ToModel())
                        .AsList();

                }
            }
            catch (Exception e)
            {
                var message = $@"Unable to retrieve merge document values with Type: {documentType} for client with id: {clientId}.";
                response.AddError(e);
                LogManager.LogError(e, message);
                Console.WriteLine(e);
            }

            return response;
        }

        public override async Task<SaveResponse<IDocument>> SaveAsync(IDocument item)
        {
            var response = new SaveResponse<IDocument>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var trans = connection.BeginTransaction();
                    var sql = GetDocumentMergeStatement();
                    var mergeDocumentDto = await connection.QuerySingleAsync<DocumentDto>(sql, Dto.FromModel(item), trans);
                    var document = mergeDocumentDto.ToModel();

                    var fieldDtos = item.Fields.Select(f =>
                        {
                            var fieldDto = new MergeDocumentFieldDto
                            {
                                DocumentId = document.Id
                            };
                            return (MergeDocumentFieldDto) fieldDto.FromModel(f);
                        }).AsList();

                    foreach (var fieldDto in fieldDtos)
                    {
                        document.Fields.Add(
                            connection.QuerySingle<MergeDocumentFieldDto>(GetFieldMergeStatement(), fieldDto, trans).ToModel());
                    }

                    trans.Commit();

                    
                    document.Fields = fieldDtos
                        .Select(dto => dto.ToModel())
                        .AsList();

                    response.Content = document;
                    response.Message = $@"Merge document {document.Name} was inserted/updated successfully.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error saving merge document.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<SaveResponse<IDocumentHistory>> SaveDocumentHistoryAsync(IDocumentHistory item)
        {
            var response = new SaveResponse<IDocumentHistory>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetDocumentHistoryMergeStatement();
                    var dto = new DocumentHistoryDto();
                    var result = await connection.QuerySingleAsync<DocumentHistoryDto>(sql, dto.FromModel(item));
                    
                    response.Content = result.ToModel();
                    response.Message = $@"Document history was inserted/updated successfully.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error saving document history.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        public async Task<SaveResponse<IList<IDocumentHistory>>> SaveAllDocumentHistoryAsync(IList<IDocumentHistory> items)
        {
            var response = new SaveResponse<IList<IDocumentHistory>>();

            try
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var sql = GetDocumentHistoryMergeStatement();
                        var dtos = items.Select(i =>
                        {
                            var dto = new DocumentHistoryDto();

                            return dto.FromModel(i);
                        }).ToList();

                    var trans = connection.BeginTransaction();
                    var result = await connection.ExecuteAsync(sql, dtos, trans);
                    trans.Commit();

                    response.Content = items;
                    response.Message = $@"Document history was inserted/updated successfully.";
                }
            }
            catch (Exception e)
            {
                var message = $@"Error saving document history.";
                LogManager.LogError(e, message);
                response.AddError(e);
                Console.WriteLine(e);
            }

            return response;
        }

        private GetResponse<IDocument> GetResponse(SqlMapper.GridReader reader)
        {
            var response = new GetResponse<IDocument>();
            var mergeDocumentDto = reader.Read<DocumentDto>()
                .FirstOrDefault();

            if (mergeDocumentDto != null)
            {
                var document = mergeDocumentDto.ToModel();
                var fieldDtos = reader.Read<MergeDocumentFieldDto>()
                    .AsList();

                if(fieldDtos.Any())
                    document.Fields = fieldDtos
                        .Select(dto => dto.ToModel())
                        .AsList();

                response.Content = document;
            }
           
            return response;
        }

        #region Sql Statements
        #region Delete Statement
        protected override string GetDeleteStatement()
        {
            return @"
                UPDATE [v2].[Document] SET [IsDeleted] = 1 WHERE [Id] = @id;
                UPDATE [v2].[MergeDocumentField] SET [IsDeleted] = 1 WHERE [DocumentId] = @id; ";
        }
        #endregion Delete Statement

        #region Merge Statement
        private string GetDocumentMergeStatement()
        {
            return @"
                MERGE [v2].[Document] AS T
                USING (VALUES 
                        ( 
                          @id
                        , @clientId
                        , @name
                        , @description
                        , @typeId
                        , @data
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [ClientId]
                        , [Name]
                        , [Description]
                        , [TypeId]
                        , [Data]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[TypeId] = S.[TypeId] AND T.[ClientId] = S.[ClientId] AND T.[IsDeleted] = 0
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [ClientId]
                        , [Name]
                        , [Description]
                        , [TypeId]
                        , [Data]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[ClientId]
                        , S.[Name]
                        , S.[Description]
                        , S.[TypeId]
                        , S.[Data]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[Name] = S.[Name], T.[Description] = S.[Description], T.[Data] = S.[Data], T.[CreatedOn] = S.[CreatedOn], 
                        T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        private string GetDocumentHistoryMergeStatement()
        {
            return @"
                MERGE [v2].[DocumentHistory] AS T
                USING (VALUES 
                        ( @id
                        , @auditid
                        , @documentTypeId
                        , @filename
                        , @printedon
                        , @printedby
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [AuditId]
                        , [DocumentTypeId]
                        , [Filename]
                        , [PrintedOn]
                        , [PrintedBy]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[FileName] = S.[FileName]
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [AuditId]
                        , [DocumentTypeId]
                        , [Filename]
                        , [PrintedOn]
                        , [PrintedBy]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[AuditId]
                        , S.[DocumentTypeId]
                        , S.[Filename]
                        , S.[PrintedOn]
                        , S.[PrintedBy]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[PrintedOn] = S.[PrintedOn], T.[PrintedBy] = S.[PrintedBy], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                        
                OUTPUT inserted.*;";
        }
        private string GetFieldMergeStatement()
        {
            return @"
                MERGE [v2].[MergeDocumentField] AS T
                USING (VALUES 
                        ( @id
                        , @documentid
                        , @tag
                        , @fieldtype
                        , @field
                        , @createdon
                        , @createdbyid
                        , @lastmodifiedon
                        , @lastmodifiedbyid
                        )
                       ) AS S
                       (
                          [Id]
                        , [DocumentId]
                        , [Tag]
                        , [FieldType]
                        , [Field]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                       ) ON T.[DocumentId] = S.[DocumentId] AND T.[Tag] = S.[Tag] AND T.[IsDeleted] = 0
                WHEN NOT MATCHED THEN
                    INSERT ( 
                          [DocumentId]
                        , [Tag]
                        , [FieldType]
                        , [Field]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                           )
                    VALUES (
                          S.[DocumentId]
                        , S.[Tag]
                        , S.[FieldType]
                        , S.[Field]
                        , S.[CreatedOn]
                        , S.[CreatedById]
                        , S.[LastModifiedOn]
                        , S.[LastModifiedById]
                           )
                WHEN MATCHED THEN
                    UPDATE SET 
                        T.[FieldType] = S.[FieldType], T.[Field] = S.[Field], T.[CreatedOn] = S.[CreatedOn], T.[CreatedById] = S.[CreatedById], T.[LastModifiedOn] = S.[LastModifiedOn], T.[LastModifiedById] = S.[LastModifiedById] 
                OUTPUT inserted.*;";
        }
        #endregion Merge Statement

        #region Select Statement
        private string GetDocumentSelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [ClientId]
                        , [Name]
                        , [Description]
                        , [TypeId]
                        , [Data]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[Document]
                    WHERE IsDeleted = 0 
                    ";
        }
        private string GetDocumentHistorySelectStatement()
        {
            return @"
                    SELECT 
                          [Id]
                        , [AuditId]
                        , [DocumentTypeId]
                        , [Filename]
                        , [PrintedOn]
                        , [PrintedBy]
                        , [CreatedOn]
                        , [CreatedById]
                        , [LastModifiedOn]
                        , [LastModifiedById]
                    FROM [v2].[DocumentHistory]
                    WHERE IsDeleted = 0 
                    ";
        }
        private string GetDocumentFieldSelectStatement()
        {
            return @"
                    SELECT 
                          f.[Id]
                        , f.[DocumentId]
                        , f.[Tag]
                        , f.[FieldType]
                        , f.[Field]
                        , f.[CreatedOn]
                        , f.[CreatedById]
                        , f.[LastModifiedOn]
                        , f.[LastModifiedById]
                    FROM [v2].[MergeDocumentField] f INNER JOIN [v2].[Document] d ON f.[DocumentId] = d.[Id]
                    WHERE f.IsDeleted = 0";
        }

        private string GetDocumentFieldValueSelectStatement()
        {
            return @"
                    SELECT 
                          v.[Id]
                        , v.[DocumentId]
                        , v.[FieldId]
                        , v.[ClientId]
                        , v.[Key]
                        , v.[Value]
                        , v.[CreatedOn]
                        , v.[CreatedById]
                        , v.[LastModifiedOn]
                        , v.[LastModifiedById]
                        , f.[Tag]
                    FROM [v2].[MergeDocumentFieldValue] v INNER JOIN [v2].[MergeDocumentField] f ON v.[FieldId] = f.[Id] 
	                    INNER JOIN
	                        (SELECT Top 1 * 
	                         FROM [v2].[Document]
	                         WHERE [TypeId] = @typeId AND (ClientId = @clientId OR ClientId = 0)
	                         ORDER BY ClientId) d ON f.[DocumentId] = d.[Id]
                    WHERE v.IsDeleted = 0
                    ";
        }
        #endregion Select Statement
        #endregion Sql Statements


    }
}



