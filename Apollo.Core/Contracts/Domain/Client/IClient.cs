using System.Collections.Generic;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Client;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Client
{
    public interface IClient : IHaveId, IHaveAuditData
    {
        IAddress Address { get; set; }
        AuditTypeEnum AuditType { get; set; }
        ClientTypeEnum ClientType { get; set; }
        IList<IContact> Contacts { get; set; }
        string Name { get; set; }
        int ParentId { get; set; }
        AuditProcessTypeEnum ProcessType { get; set; }
        IClientSettings Settings { get; set; }
    }
}