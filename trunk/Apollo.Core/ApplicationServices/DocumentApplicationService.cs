// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/08/2018
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
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class DocumentApplicationService : IDocumentApplicationService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogManager _logManager;
        private readonly IDocumentProvider _documentProvider;
        private readonly IDocumentGeneratorProvider _documentGeneratorProvider;

        public DocumentApplicationService(
            ILogManager logManager, 
            IDocumentRepository documentRepository, 
            IDocumentProvider documentProvider, 
            IDocumentGeneratorProvider documentGeneratorProvider)
        {
            _logManager = logManager;
            _documentRepository = documentRepository;
            _documentProvider = documentProvider;
            _documentGeneratorProvider = documentGeneratorProvider;
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
                response = await _documentRepository.GetDocumentHistoryAsync(request);
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "DocumentApplicationService.GetDocumentHistoryAsync");
                response.AddError(e);
            }

            return response;
        }

        public async Task<GetResponse<MemoryStream>> GenerateDocumentAsync(DocumentRequest request)
        {
            // 1. Get the document generator
            var generator = _documentGeneratorProvider.GetGenerator(request);

            // 2. Check to see if this is a reprint.  If so, set the date to the original print date
            var historyResponse = await GetDocumentHistoryAsync(new DocumentHistoryRequest { AuditId = request.Audit.Id});

            if (historyResponse.IsSuccessful && historyResponse.Content.Any())
            {
                request.ReportDate = historyResponse.Content.First().CreatedOn.Date;
            }
            else
            {
                request.ReportDate = DateTime.Today;
            }

            return await generator.GenerateDocumentAsync(request);
        }
        
        public void SaveBatch(string fileName, MemoryStream document, FileTypes fileType)
        {
            _documentProvider.SaveBatch(fileName, document, fileType);
        }

        public SaveResponse<IList<IDocumentHistory>> SaveAllDocumentHistory(IList<IDocumentHistory> items)
        {
            var response = SaveAllDocumentHistoryAsync(items).Result;

            return response;
        }

        public async Task<SaveResponse<IList<IDocumentHistory>>> SaveAllDocumentHistoryAsync(IList<IDocumentHistory> items)
        {
            var response = new SaveResponse<IList<IDocumentHistory>>();

            try
            {
                response = await _documentRepository.SaveAllDocumentHistoryAsync(items);
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "DocumentApplicationService.GetFieldValuesAsync");
                response.AddError(e);
            }
            return response;
        }

        public async Task<SaveResponse<IDocumentHistory>> SaveDocumentHistoryAsync(IDocumentHistory item)
        {
            var response = new SaveResponse<IDocumentHistory>();

            try
            {
                response = await _documentRepository.SaveDocumentHistoryAsync(item);
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