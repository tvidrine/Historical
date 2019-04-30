// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/11/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface ISystemInfoApplicationService
    {
        Task<GetResponse<ISystemInfo>> GetAsync();
        Task<SaveResponse<ISystemInfo>> SaveAsync(ISystemInfo systemInfo);
    }
}
