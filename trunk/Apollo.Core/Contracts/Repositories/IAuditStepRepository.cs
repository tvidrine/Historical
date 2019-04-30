// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAuditStepRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAuditStep>>> GetAllAsync(int auditId);
        Task<GetResponse<IAuditStep>> GetByIdAsync(int id);

        Task<GetResponse<IAuditStep>> GetByAuditIdAsync(int auditId, int entityId, WizardPageEnum page);
        Task<SaveResponse<IAuditStep>> SaveAsync(IAuditStep auditStep);
        Task<SaveResponse<IReadOnlyList<IAuditStep>>> SaveAllAsync(IReadOnlyList<IAuditStep> auditStep);
        Task<GetResponse<IAuditStep>> GetCurrentStepAsync(int auditId);
    }
}
