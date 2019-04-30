// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/26/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Providers
{
    public interface IDocumentProvider
    {
        Task<GetResponse<MemoryStream>> MergeDocumentAsync(Stream documentStream, IDictionary<string, MergeDocumentField> values);
        void SaveBatch(string fileName, MemoryStream document, FileTypes fileType);
    }
}