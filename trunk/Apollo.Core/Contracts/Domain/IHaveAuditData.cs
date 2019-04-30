using System;

namespace Apollo.Core.Contracts.Domain
{
    public interface IHaveAuditData
    {
        int CreatedById { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        int LastModifiedById { get; set; }
        DateTimeOffset LastModifiedOn { get; set; }
    }
}