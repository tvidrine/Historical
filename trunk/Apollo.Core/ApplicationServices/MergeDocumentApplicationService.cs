// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class MergeDocumentApplicationService : IMergeDocumentApplicationService
    {
        private readonly IFileShareProvider _fileShareProvider;
        private readonly ILogManager _logManager;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentProvider _documentProvider;

        public MergeDocumentApplicationService(IFileShareProvider fileShareProvider, ILogManager logManager, IDocumentRepository documentRepository, IDocumentProvider documentProvider)
        {
            _fileShareProvider = fileShareProvider;
            _logManager = logManager;
            _documentRepository = documentRepository;
            _documentProvider = documentProvider;
        }
        public async Task<GetResponse<MemoryStream>> MergeDocumentAsync(int clientId, DocumentTypes documentType, IList<MergeDocumentValue> values)
        {
            var response = new GetResponse<MemoryStream>();

            try
            {
                var repositoryResponse = await _documentRepository.GetByTypeAsync(clientId, documentType);


                if (repositoryResponse.IsSuccessful)
                {
                    var mergeDocument = repositoryResponse.Content;

                    var fieldValues = values
                        .ToDictionary(value => value.Key, value =>
                            new MergeDocumentField { Value = value.Value, FieldType = value.Type });

                    using (var ms = new MemoryStream(mergeDocument.Data))
                    {
                        var providerResponse = await _documentProvider.MergeDocumentAsync(ms, fieldValues);

                        if (providerResponse.IsSuccessful)
                            response.Content = providerResponse.Content;
                    }

                }
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "DocumentApplicationService.GenerateDocumentAsync");
                response.AddError(e);
            }

            return response;
        }
        public GetResponse<MemoryStream> MergeDocumentToShare(int clientId, DocumentTypes documentType, IList<MergeDocumentValue> values, string shareFolder)
        {
            var response = new GetResponse<MemoryStream>();

            try
            {
                var repositoryResponse = _documentRepository.GetByTypeAsync(clientId, documentType).Result;

                if (repositoryResponse.IsSuccessful)
                {
                    var mergeDocument = repositoryResponse.Content;

                    // Create a share and get the link to merge
                    var shareReponse = _fileShareProvider.CreateShareFolderLink(shareFolder, "Share Audit Folder");

                    if (shareReponse.IsSuccessful)
                        values.Add(new MergeDocumentValue { Key = "ShareFileLink", Value = shareReponse.Content, Type = MergeDocumentFieldTypes.Hyperlink });

                    var fieldValues = values
                        .ToDictionary(value => value.Key, value =>
                            new MergeDocumentField { Value = value.Value, FieldType = value.Type });

                    using (var ms = new MemoryStream(mergeDocument.Data))
                    {
                        var providerResponse = _documentProvider.MergeDocumentAsync(ms, fieldValues).Result;

                        if (providerResponse.IsSuccessful)
                            response.Content = providerResponse.Content;
                    }

                }
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "DocumentApplicationService.GenerateDocumentToShare");
                response.AddError(e);
            }
            return response;
        }
        public async Task<GetResponse<MemoryStream>> MergeDocumentToShareAsync(int clientId, DocumentTypes documentType, IList<MergeDocumentValue> values, string shareFolders)
        {
            var response = new GetResponse<MemoryStream>();

            try
            {
                var repositoryResponse = await _documentRepository.GetByTypeAsync(clientId, documentType);


                if (repositoryResponse.IsSuccessful)
                {
                    var mergeDocument = repositoryResponse.Content;

                    // Create a share and get the link to merge
                    var shareReponse = await _fileShareProvider.CreateShareFolderLinkAsync(shareFolders, "Share Audit Folder");

                    if (shareReponse.IsSuccessful)
                        values.Add(new MergeDocumentValue { Key = "ShareFileLink", Value = shareReponse.Content, Type = MergeDocumentFieldTypes.Hyperlink });

                    var fieldValues = values
                        .ToDictionary(value => value.Key, value =>
                            new MergeDocumentField { Value = value.Value, FieldType = value.Type });

                    using (var ms = new MemoryStream(mergeDocument.Data))
                    {
                        var providerResponse = await _documentProvider.MergeDocumentAsync(ms, fieldValues);

                        if (providerResponse.IsSuccessful)
                            response.Content = providerResponse.Content;
                    }

                }
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "DocumentApplicationService.GenerateDocumentToShareAsync");
                response.AddError(e);
            }

            return response;
        }
        public async Task<GetResponse<IList<IDocumentFieldValue>>> GetMergeFieldValuesAsync(int clientId, DocumentTypes documentType)
        {
            var response = new GetResponse<IList<IDocumentFieldValue>>();

            try
            {
                response = await _documentRepository.GetFieldValuesAsync(clientId, documentType);
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "DocumentApplicationService.GetFieldValuesAsync");
                response.AddError(e);
            }
            return response;
        }

    }
}