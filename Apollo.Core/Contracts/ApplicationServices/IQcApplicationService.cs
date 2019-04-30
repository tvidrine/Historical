// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 02/04/2019
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IQcApplicationService
    {
        //Task<ICreateResponse<IQcData>> CreateAsync();
        Task<DeleteResponse> DeleteQcDataByAuditIdAsync(int auditId);

        Task<GetResponse<bool>> ReturnForCorrectionsAsync(int auditId, int userId);

        //Task<GetResponse<IQcData>> GetAsync(int id);
        //Task<GetResponse<IReadOnlyList<IQcData>>> GetAllAsync();
        //Task<SaveResponse<IQcData>> SaveAsync(IQcData qcData);
        //Task<SaveResponse<IReadOnlyList<IQcData>>> SaveAllAsync(IReadOnlyList<IQcData> qcDatas);
        //Task<ValidationResult> ValidateAsync(IQcData qcData);
    }
}