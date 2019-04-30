// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/10/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Contracts.DomainServices.Letters;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Document;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.DomainServices.Letters
{
    public class WelcomeLetterGl : BaseWelcomeLetter, IWelcomeLetterGl
    {
        private readonly IMergeDocumentApplicationService _mergeDocumentApplicationService;

        public WelcomeLetterGl(IMergeDocumentApplicationService mergeDocumentApplicationService)
        {
            _mergeDocumentApplicationService = mergeDocumentApplicationService;
        }
        public async Task<GetResponse<MemoryStream>> GenerateDocumentAsync(DocumentRequest request)
        {
            // 1. Get stored values
            var valueResponse = await _mergeDocumentApplicationService.GetMergeFieldValuesAsync(request.Client.Id, DocumentTypes.WelcomeLetterGl);

            var valueCache = valueResponse.Content;

            // 2. Get document values as a list
            var documentValues = GetDocumentCommonValues(request);

            // 3. Add values specific to GL
            documentValues.Add(new MergeDocumentValue { Key = "Paragraph1", Value = GetParagraph1(request, valueCache), Type = MergeDocumentFieldTypes.String });
            documentValues.Add(new MergeDocumentValue { Key = "Paragraph2", Value = GetParagraph2(request, valueCache), Type = MergeDocumentFieldTypes.String });
            documentValues.Add(new MergeDocumentValue { Key = "Paragraph3", Value = GetParagraph3(request, valueCache), Type = MergeDocumentFieldTypes.String });
            documentValues.Add(new MergeDocumentValue { Key = "Title", Value = GetGlTitle(request), Type = MergeDocumentFieldTypes.String });
            documentValues.Add(new MergeDocumentValue { Key = "Salutation", Value = GetSalutation(request, valueCache), Type = MergeDocumentFieldTypes.String });

            // 4. Load document
            var response = _mergeDocumentApplicationService.MergeDocumentAsync(request.Client.Id, DocumentTypes.WelcomeLetterGl, documentValues).Result;

            return response;
        }

        
        private string GetGlTitle(DocumentRequest request)
        {
            var title = string.Empty;

            switch (request.Audit.AuditFrequency)
            {
                case AuditFrequencyEnum.Annual:
                    title = "AUDIT NOTICE";
                    break;
                case AuditFrequencyEnum.Monthly:
                    title = "AUDIT NOTICE";
                    break;
                case AuditFrequencyEnum.Quarterly:
                    title = "AUDIT NOTICE";
                    break;
                case AuditFrequencyEnum.SemiAnnual:
                    title = $@"MID-TERM{Environment.NewLine}AUDIT NOTICE";
                    break;
                case AuditFrequencyEnum.PreAudit:
                    title = $@"PRE-AUDIT{Environment.NewLine}AUDIT NOTICE";
                    break;
            }
            return title;
        }
        
        private string GetParagraph1(DocumentRequest request, IList<IDocumentFieldValue> valueCache)
        {
            var fieldTag = "Paragraph1";

            var paragraph = string.Empty;

            switch (request.Audit.AuditFrequency)
            {
                case AuditFrequencyEnum.Annual:
                case AuditFrequencyEnum.Monthly:
                case AuditFrequencyEnum.Quarterly:
                {
                    var value = GetValue(request, valueCache, fieldTag, "Term").Value;

                    paragraph = value.Replace("[PolicyEndDate]", $@"{request.Audit.Policy.EffectiveEnd:d}");
                    break;
                }

                case AuditFrequencyEnum.SemiAnnual:
                {
                    var value = GetValue(request, valueCache, fieldTag, "MidTerm").Value;

                    paragraph = value.Replace("[PolicyEndDate]", $@"{request.Audit.Policy.EffectiveEnd:d}");
                    break;
                }
                case AuditFrequencyEnum.PreAudit:
                {
                    var value = GetValue(request, valueCache, fieldTag, "PreTerm").Value;

                    paragraph = value.Replace("[PolicyEndDate]", $@"{request.Audit.Policy.EffectiveEnd:d}");

                    break;
                }

            }
            return paragraph;
        }
        private string GetParagraph2(DocumentRequest request, IList<IDocumentFieldValue> valueCache)
        {
            var audit = request.Audit;
            var sb = new StringBuilder();
            var auditType = audit.AuditMethod == AuditMethods.eAudit ? "eAudit" : "ShareAudit";
            var fieldTag = "Paragraph2";

            sb.AppendLine($@"Information needed to complete the online {auditType} for the policy period listed above:");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("• Business Details such as description of operations, locations and owner/officer information.");
            sb.AppendLine("");
            sb.AppendLine("");
            // Exposure basis is Payroll or Cost 
            if ((audit.AuditExposureBasis & ExposureBasisEnum.Payroll) == ExposureBasisEnum.Payroll || (audit.AuditExposureBasis & ExposureBasisEnum.Costs) == ExposureBasisEnum.Costs)
            {
                sb.AppendLine("• Payroll Summary Report");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("• Breakdown of Names/Duties of Each Employee engaged exclusively in clerical work within the office, employees exclusively engaged in outside sales work, or any employee who strictly is employed as a driver operating a company vehicle primarily on public roads (does not include heavy equipment operators).");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("• Copies of Federal 941 of State Unemployment Tax Forms");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("• Vendor Report detailing the amounts paid to subcontractors/contract laborers utilized during the policy period.");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("• Copies of Certificates of Insurance showing General Liability Coverage for each subcontractor used during the policy period that provided their own General Liability Coverage.");
                sb.AppendLine("");
                sb.AppendLine("");
            }

            // Exposure basis is Cost 
            if ((audit.AuditExposureBasis & ExposureBasisEnum.Costs) == ExposureBasisEnum.Costs)
            {

                sb.AppendLine("• Total amount paid to all subcontractors that provided their own General Liability Coverage.");
                sb.AppendLine("");
                sb.AppendLine("");
            }

            // Exposure basis is Sales 
            if ((audit.AuditExposureBasis & ExposureBasisEnum.Sales) == ExposureBasisEnum.Sales)
            {
                sb.AppendLine("• Monthly Sales Reports Broken out for Each Location");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("• State Sales Tax Returns");
                sb.AppendLine("");
                sb.AppendLine("");
            }

            return sb.ToString();
        }
        private string GetParagraph3(DocumentRequest request, IList<IDocumentFieldValue> valueCache)
        {
            var fieldTag = "Paragraph3";

            return GetValue(request, valueCache, fieldTag, "Default").Value;
        }
        
    }
}