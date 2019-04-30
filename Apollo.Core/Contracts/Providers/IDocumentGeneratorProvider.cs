// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Messages.Requests;

namespace Apollo.Core.Contracts.Providers
{
    public interface IDocumentGeneratorProvider
    {
        IDocumentGenerator GetGenerator(DocumentRequest request);
    }
}