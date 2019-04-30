using System;

namespace Apollo.Core.Domain.Audit
{
    [Flags]
    public enum AuditProcessTypeEnum
    {
        NotSet = 0x0,
        AssistedAudit = 0x1,
        eAudit = 0x2,
        eAuditLight = 0x4,
        SharedAudit = 0x8,
        Physical = 0x10,
        Voluntary = 0x20
    }
}