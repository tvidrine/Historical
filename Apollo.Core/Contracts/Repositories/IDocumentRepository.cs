// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 8/17/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IDocumentRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IDocument>>> GetAllAsync();
        Task<GetResponse<IDocument>> GetByIdAsync(int id);
        Task<GetResponse<IDocument>> GetByTypeAsync(int clientId, DocumentTypes documentType);
        GetResponse<IList<IDocumentHistory>> GetDocumentHistory(DocumentHistoryRequest request);
        Task<GetResponse<IList<IDocumentHistory>>> GetDocumentHistoryAsync(DocumentHistoryRequest request);
        Task<GetResponse<IList<IDocumentFieldValue>>> GetFieldValuesAsync(int clientId, DocumentTypes documentType);
        Task<SaveResponse<IDocument>> SaveAsync(IDocument mergeDocument);
        Task<SaveResponse<IDocumentHistory>> SaveDocumentHistoryAsync(IDocumentHistory item);
        Task<SaveResponse<IList<IDocumentHistory>>> SaveAllDocumentHistoryAsync(IList<IDocumentHistory> items);
    }
}
