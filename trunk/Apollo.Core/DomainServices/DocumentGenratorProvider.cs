// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.DomainServices.Letters;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;

namespace Apollo.Core.DomainServices
{
    public class DocumentGenratorProvider : IDocumentGeneratorProvider
    {
        private readonly IDictionary<DocumentTypes, IDocumentGenerator> _generatorCache;
        public DocumentGenratorProvider(IWelcomeLetterGl welcomeLetterGl, IWelcomeLetterWc welcomeLetterWc)
        {
            _generatorCache = new Dictionary<DocumentTypes, IDocumentGenerator>
            {
                {DocumentTypes.WelcomeLetterGl, welcomeLetterGl },
                {DocumentTypes.WelcomeLetterWc, welcomeLetterWc }
            };
        }
        public IDocumentGenerator GetGenerator(DocumentRequest request)
        {
            return _generatorCache[request.DocumentType];
        }
    }
}