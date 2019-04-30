// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/10/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Configuration;
using Apollo.Core.Domain.Communication;
using Apollo.Core.Messages.Results;

namespace Apollo.Core.Contracts.Providers
{
    public interface ICommunicationProvider
    {
        bool Clear(ClientConfiguration clientConfiguration);
        Task<bool> ClearAsync(ClientConfiguration clientConfiguration);
        Task<GetResult<IReadOnlyList<Packet>>> GetPacketsAsync(ClientConfiguration clientConfiguration);
        Task<SendResult> SendPacketAsync(Packet packet, ClientConfiguration clientConfiguration = null);
    }
}