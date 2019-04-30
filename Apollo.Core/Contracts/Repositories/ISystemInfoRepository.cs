// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/9/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface ISystemInfoRepository
    {
        Task<GetResponse<ISystemInfo>> GetAsync();
        Task<SaveResponse<ISystemInfo>> SaveAsync(ISystemInfo systemInfo);
    }
}
