using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Core.Contracts.Domain.Document;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Document;
using Apollo.Core.Messages.Requests;

namespace Apollo.Core.DomainServices.Letters
{
    public abstract class BaseWelcomeLetter
    {
        protected IList<MergeDocumentValue> GetDocumentCommonValues(DocumentRequest request)
        {
            var client = request.Client;
            var address = client.Address;
            var audit = request.Audit;

            return new List<MergeDocumentValue>
            {
                {new MergeDocumentValue {Key = "AuditId", Value = audit.Id, Type = MergeDocumentFieldTypes.String} },
                {new MergeDocumentValue {Key = "PolicyNumber", Value = audit.Policy.PolicyNumber, Type = MergeDocumentFieldTypes.String} },
                {new MergeDocumentValue {Key = "PolicyPeriod", Value =$@"{audit.Policy.EffectiveStart:d}-{audit.Policy.EffectiveEnd:d}", Type = MergeDocumentFieldTypes.String} },
                {new MergeDocumentValue {Key = "ClientAddress", Value = address.Line1, Type = MergeDocumentFieldTypes.String} },
                {new MergeDocumentValue {Key = "ClientCityStateZip", Value = $@"{address.City}, {address.State}  {address.Zipcode}", Type = MergeDocumentFieldTypes.String} },
                {new MergeDocumentValue {Key = "ClientLogo", Value = client.Settings.Logo, Type = MergeDocumentFieldTypes.Image} },
                {new MergeDocumentValue {Key = "CurrentDate", Value = request.ReportDate.ToString("MMMM dd, yyyy"), Type = MergeDocumentFieldTypes.String} },
                {new MergeDocumentValue {Key = "InsuredName", Value = GetInsuredInfo(audit.Policy), Type = MergeDocumentFieldTypes.String} },

            };
        }
        protected IDocumentFieldValue GetValue(DocumentRequest request, IList<IDocumentFieldValue> valueCache, string fieldTag, string key)
        {
            var value = valueCache.FirstOrDefault(v => request.Client.Id == v.ClientId && v.FieldTag == fieldTag && v.Key == key);

            return value ?? valueCache.FirstOrDefault(v => v.FieldTag == fieldTag && v.Key == key);

        }
        protected string GetSalutation(DocumentRequest request, IList<IDocumentFieldValue> valueCache)
        {
            var fieldTag = "Salutation";
            var value = GetValue(request, valueCache, fieldTag, "Default");
            return value == null ? string.Empty : value.Value;
        }

        private string GetInsuredInfo(IPolicy policy)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(policy.InsuredName))
                sb.AppendLine(policy.InsuredName);
            sb.AppendLine(policy.CompanyName);
            sb.AppendLine(policy.Address.Line1);
            if (!string.IsNullOrEmpty(policy.Address.Line2))
                sb.AppendLine(policy.Address.Line2);
            sb.AppendLine($@"{policy.Address.City}, {policy.Address.State}  {policy.Address.Zipcode}");

            return sb.ToString();

        }

    }


}
