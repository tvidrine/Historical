// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Contracts.Domain.Client
{
    public interface IClientSetting : IHaveAuditData, IHaveId
    {
        int ClientId { get; set; }
        ClientSettingsEnum SettingType { get; set; }
        object Value { get; set; }
    }
}