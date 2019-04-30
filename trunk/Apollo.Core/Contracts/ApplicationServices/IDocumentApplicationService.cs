// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IDocumentApplicationService
    {
        GetResponse<IList<IDocumentHistory>> GetDocumentHistory(DocumentHistoryRequest request);
        Task<GetResponse<IList<IDocumentHistory>>> GetDocumentHistoryAsync(DocumentHistoryRequest request);
        Task<GetResponse<MemoryStream>> GenerateDocumentAsync(DocumentRequest request);
        void SaveBatch(string fileName, MemoryStream tokenDocument, FileTypes pdf);
        SaveResponse<IList<IDocumentHistory>> SaveAllDocumentHistory(IList<IDocumentHistory> items);
        Task<SaveResponse<IList<IDocumentHistory>>> SaveAllDocumentHistoryAsync(IList<IDocumentHistory> items);
        Task<SaveResponse<IDocumentHistory>> SaveDocumentHistoryAsync(IDocumentHistory item);

    }
}