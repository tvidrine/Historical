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
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IAuditApplicationService
    {
        Task<ICreateResponse<IAudit>> CreateAsync(AuditRequest request);
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IReadOnlyList<IAudit>>> GetAllAsync(bool activeOnly = false);

        GetResponse<IAudit> Get(int id);
        Task<GetResponse<IAudit>> GetAsync(int id);
        Task<SaveResponse<IReadOnlyList<IAudit>>> SaveAllAsync(IReadOnlyList<IAudit> audits);
        Task<SaveResponse<IAudit>> SaveAsync(IAudit audit);
        Task SendWorkProductDeliveryDocumentAsync();
        Task<ValidationResult> ValidateAsync(IAudit audit);

        // The following will be removed when the full implementation is done
        Task<SaveResponse> UpdateAuditStatus(int auditId, AuditStatuses status, int userId);
        Task<SaveResponse> NotifyPolicyHolder(int auditId, int i, DateTime nowDate);
    }
}