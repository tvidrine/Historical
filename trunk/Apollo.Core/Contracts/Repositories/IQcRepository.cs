// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 2/4/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IQcRepository
    {
        Task<DeleteResponse> DeleteQcDataByAuditIdAsync(int auditId);
        //Task<GetResponse<IReadOnlyList<IQcData>>> GetAllAsync();
        //Task<GetResponse<IQcData>> GetByIdAsync(int id);
        //Task<SaveResponse<IQcData>> SaveAsync(IQcData qcData);
        //Task<SaveResponse<IReadOnlyList<IQcData>>> SaveAllAsync(IReadOnlyList<IQcData> qcData);
    }
}
