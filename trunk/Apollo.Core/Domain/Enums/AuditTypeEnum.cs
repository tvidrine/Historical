using System;

namespace Apollo.Core.Domain.Enums
{
    [Flags]
    public enum AuditTypeEnum
    {
        NotSet = 0x0,
        WC = 0x1,
        GL = 0x2,
        Combo = WC | GL
    }
}