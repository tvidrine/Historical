// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IClassCodeRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllAsync();
        Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllByAuditType(AuditTypeEnum auditType);
        Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForAuditAsync(int auditId);
        Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForStatesAsync(IAudit audit);
        Task<GetResponse<IClassCode>> GetByIdAsync(int id);
        Task<GetResponse<IReadOnlyList<IExposureBasis>>> GetExposureBasisListAsync();
        Task<GetResponse<IReadOnlyList<IClassCode>>> GetStandardExceptionsForAudit(IAudit audit);
        Task<SaveResponse<IClassCode>> SaveAsync(IClassCode classCode);
        Task<SaveResponse<IReadOnlyList<IClassCode>>> SaveAllAsync(IReadOnlyList<IClassCode> classCode);
        
    }
}
