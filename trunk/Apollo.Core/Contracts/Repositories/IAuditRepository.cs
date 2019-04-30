// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.Contracts.Repositories
{
    public interface IAuditRepository
    {
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAudit>>> GetAllAsync(bool activeOnly = false);
        Task<GetResponse<IAudit>> GetAsync(AuditRequest request);
        Task<GetResponse<IAudit>> GetByIdAsync(int id);
        Task<SaveResponse<IAudit>> SaveAsync(IAudit audit);
        Task<SaveResponse> UpdateAuditStatus(int auditId, AuditStatuses status, int userId);
        Task<SaveResponse> NotifyPolicyHolder(int auditId, int notificationType, DateTime sentDate);
    }
}