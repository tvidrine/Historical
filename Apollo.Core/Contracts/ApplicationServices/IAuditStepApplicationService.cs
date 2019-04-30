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
using FluentValidation.Results;

namespace Apollo.Core.Contracts.ApplicationServices
{
    public interface IAuditStepApplicationService
    {
        Task<ICreateResponse<IAuditStep>> CreateAsync(int auditId);
        Task<DeleteResponse> DeleteAsync(int id);
        Task<GetResponse<IAuditStep>> GetAsync(int id);
        Task<GetResponse<IAuditStep>> GetAsync(int auditId, int entityId,  WizardPageEnum page);
        Task<GetResponse<IReadOnlyList<IAuditStep>>> GetAllAsync(int auditId);
        Task<SaveResponse<IAuditStep>> SaveAsync(IAuditStep auditStep);
        Task<SaveResponse<IReadOnlyList<IAuditStep>>> SaveAllAsync(IReadOnlyList<IAuditStep> auditSteps);
        Task<ValidationResult> ValidateAsync(IAuditStep auditStep);
        IReadOnlyList<IAuditStep> CreateAllSteps(IAudit audit);
        Task<GetResponse<IAuditStep>> GetCurrentStepAsync(int auditId);
        Task<GetResponse<IAuditStep>> GetPage(int auditId, int entityId, WizardPageEnum pageType);
    }
}
