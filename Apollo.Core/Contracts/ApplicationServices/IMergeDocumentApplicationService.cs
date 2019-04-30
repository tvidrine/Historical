// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IMergeDocumentApplicationService
    {
        Task<GetResponse<MemoryStream>> MergeDocumentAsync(int clientId, DocumentTypes documentType, IList<MergeDocumentValue> values);
        GetResponse<MemoryStream> MergeDocumentToShare(int clientId, DocumentTypes documentType, IList<MergeDocumentValue> values, string shareFolder);
        Task<GetResponse<MemoryStream>> MergeDocumentToShareAsync(int clientId, DocumentTypes documentType, IList<MergeDocumentValue> values, string shareFolder);
        Task<GetResponse<IList<IDocumentFieldValue>>> GetMergeFieldValuesAsync(int clientId, DocumentTypes documentType);
    }
}