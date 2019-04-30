// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts
{
    public interface IDocumentGenerator
    {
        Task<GetResponse<MemoryStream>> GenerateDocumentAsync(DocumentRequest request);
    }
}