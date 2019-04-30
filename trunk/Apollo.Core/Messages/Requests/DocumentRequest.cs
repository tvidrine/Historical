using System;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Messages.Requests
{
    public class DocumentRequest
    {
        public IAudit Audit { get; set; }
        public IClient Client { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
