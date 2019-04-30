// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/22/2019
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.Core.Contracts.Domain.Client;
using Apollo.Core.Domain.Core;
using Apollo.Core.Domain.Enums;

namespace Apollo.Core.Domain.Client
{
    public class ClientSetting : ModelBase, IClientSetting
    {
        public int ClientId { get; set; }
        public ClientSettingsEnum SettingType { get; set; }
        public object Value { get; set; }
    }
}