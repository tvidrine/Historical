// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 11/30/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.ClassCode;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain;
using Apollo.Core.Domain.ClassCode;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class ClassCodeApplicationService : IClassCodeApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IClassCodeRepository _classCodeRepository;
        private readonly IClassCodeValidator _classCodeValidator;

        public ClassCodeApplicationService(ILogManager logManager, IClassCodeValidator classCodeValidator,
            IClassCodeRepository classCodeRepository)
        {
            _logManager = logManager;
            _classCodeRepository = classCodeRepository;
            _classCodeValidator = classCodeValidator;
        }

        public async Task<ICreateResponse<IClassCode>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IClassCode>
            {
                Content = new ClassCode(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();

            try
            {
                deleteResponse = await _classCodeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete classCode");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IClassCode>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IClassCode>();

            try
            {
                getResponse = await _classCodeRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCode");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IClassCode>>();

            try
            {
                getResponse = await _classCodeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCodes");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllByAuditType(AuditTypeEnum auditType)
        {
            var getResponse = new GetResponse<IReadOnlyList<IClassCode>>();
            try
            {
                getResponse = await _classCodeRepository.GetAllByAuditType(auditType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCodes");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForAuditAsync(int auditId)
        {
            var getResponse = new GetResponse<IReadOnlyList<IClassCode>>();
            try
            {
                getResponse = await _classCodeRepository.GetAllForAuditAsync(auditId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCodes");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetAllForStatesAsync(IAudit audit)
        {
            var getResponse = new GetResponse<IReadOnlyList<IClassCode>>();
            try
            {
                getResponse = await _classCodeRepository.GetAllForStatesAsync(audit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCodes");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IExposureBasis>>> GetExposureBasisListAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IExposureBasis>>();
            try
            {
                getResponse = await _classCodeRepository.GetExposureBasisListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCodes");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IClassCode>> SaveAsync(IClassCode classCode)
        {
            var saveResponse = new SaveResponse<IClassCode>();

            try
            {
                saveResponse = await _classCodeRepository.SaveAsync(classCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving classCode");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IClassCode>>> SaveAllAsync(IReadOnlyList<IClassCode> classCodes)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IClassCode>>();

            try
            {
                saveResponse = await _classCodeRepository.SaveAllAsync(classCodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving classCodes");
            }

            return saveResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IClassCode>>> GetStandardExceptionsForAudit(IAudit audit)
        {
            var getResponse = new GetResponse<IReadOnlyList<IClassCode>>();

            try
            {
                getResponse = await _classCodeRepository.GetStandardExceptionsForAudit(audit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving classCodes");
            }

            return getResponse;
        }

        public async Task<ValidationResult> ValidateAsync(IClassCode classCode)
        {
            var result = await _classCodeValidator.ValidateAsync(classCode);

            return result;
        }
    }
}
