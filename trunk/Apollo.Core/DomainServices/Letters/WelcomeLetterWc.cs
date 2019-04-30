// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.DomainServices.Letters;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.DomainServices.Letters
{
    public class WelcomeLetterWc : BaseWelcomeLetter, IWelcomeLetterWc
    {
        private readonly IMergeDocumentApplicationService _mergeDocumentApplicationService;

        public WelcomeLetterWc(IMergeDocumentApplicationService mergeDocumentApplicationService)
        {
            _mergeDocumentApplicationService = mergeDocumentApplicationService;
        }

        public async Task<GetResponse<MemoryStream>> GenerateDocumentAsync(DocumentRequest request)
        {
            // 1. Get stored values
            var valueResponse = await _mergeDocumentApplicationService.GetMergeFieldValuesAsync(request.Client.Id, DocumentTypes.WelcomeLetterWc);
            var valueCache = valueResponse.Content;

            // 2. Get document values as a list
            var documentValues = GetDocumentCommonValues(request);

            // 3. Add values specific to WC
            documentValues.Add(new MergeDocumentValue { Key = "ClientName", Value = request.Client.Name, Type = MergeDocumentFieldTypes.String });
            documentValues.Add(new MergeDocumentValue { Key = "Salutation", Value = GetSalutation(request, valueCache), Type = MergeDocumentFieldTypes.String });

            // 4. Load document
            var response = _mergeDocumentApplicationService.MergeDocumentAsync(request.Client.Id, DocumentTypes.WelcomeLetterWc, documentValues).Result;

            return response;
        }
    }
}