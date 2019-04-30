// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 12/28/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.DomainServices.Validators;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Enums;
using Apollo.Core.Messages.Responses;
using FluentValidation.Results;

namespace Apollo.Core.ApplicationServices
{
    public class AuditStepApplicationService : IAuditStepApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IAuditStepRepository _auditStepRepository;
        private readonly IAuditStepValidator _validator;
        private readonly IDictionary<ExposureBasisEnum, IReadOnlyList<IAuditStep>> _basisSteps;

        public AuditStepApplicationService(ILogManager logManager, IAuditStepRepository auditStepRepository, IAuditStepValidator validator)
        {
            _logManager = logManager;
            _auditStepRepository = auditStepRepository;
            _validator = validator;

            _basisSteps = GetExposureBasisSteps();
        }

        public async Task<ICreateResponse<IAuditStep>> CreateAsync(int auditId)
        {
            return await Task.Run(() => new CreateResponse<IAuditStep>
            {
                Content = new AuditStep(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _auditStepRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete auditStep");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IAuditStep>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IAuditStep>();
            try
            {
                getResponse = await _auditStepRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditStep");
            }

            return getResponse;
        }

        public async Task<GetResponse<IAuditStep>> GetAsync(int auditId, int entityId, WizardPageEnum page)
        {
            var getResponse = new GetResponse<IAuditStep>();
            try
            {
                getResponse = await _auditStepRepository.GetByAuditIdAsync(auditId, entityId, page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditStep");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IAuditStep>>> GetAllAsync(int auditId)
        {
            var getResponse = new GetResponse<IReadOnlyList<IAuditStep>>();
            try
            {
                getResponse = await _auditStepRepository.GetAllAsync(auditId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditSteps");
            }

            return getResponse;
        }

        public async Task<GetResponse<IAuditStep>> GetCurrentStepAsync(int auditId)
        {
            var getResponse = new GetResponse<IAuditStep>();
            try
            {
                getResponse = await _auditStepRepository.GetCurrentStepAsync(auditId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditStep");
            }

            return getResponse;
        }

        public async Task<GetResponse<IAuditStep>> GetPage(int auditId, int entityId, WizardPageEnum pageType)
        {
            var getResponse = new GetResponse<IAuditStep>();

            try
            {
                var stepsResponse = await GetAllAsync(auditId);

                if (stepsResponse.IsSuccessful)
                {
                    getResponse.Content = stepsResponse.Content
                        .FirstOrDefault(s => s.WizardPageType == pageType && s.EntityId == entityId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving auditStep");
            }
            return getResponse;
        }

        public async Task<SaveResponse<IAuditStep>> SaveAsync(IAuditStep auditStep)
        {
            var saveResponse = new SaveResponse<IAuditStep>();
            try
            {
                saveResponse = await _auditStepRepository.SaveAsync(auditStep);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditStep");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IAuditStep>>> SaveAllAsync(IReadOnlyList<IAuditStep> auditSteps)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IAuditStep>>();
            try
            {
                saveResponse = await _auditStepRepository.SaveAllAsync(auditSteps);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving auditSteps");
            }

            return saveResponse;
        }
        public Task<ValidationResult> ValidateAsync(IAuditStep auditStep)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IAuditStep> CreateAllSteps(IAudit audit)
        {
            var stepOrder = 0;
            var steps = new List<IAuditStep>
            {
                new AuditStep { WizardPageType = WizardPageEnum.Requirements, StepOrder = stepOrder++ },

            };
           
            // Add the steps for each entity the audit has
            foreach (var entity in audit.Policy.Entities)
            {
                steps.AddRange(GetEntitySteps(entity, ref stepOrder));
            }
            
            // Add the Final step
            steps.AddRange(new List<IAuditStep>
            {
                { new AuditStep {WizardPageType = WizardPageEnum.Final, StepOrder =  stepOrder}}
            });

            // Set the order of the steps and the audit id
            foreach (var step in steps)
            {
                step.AuditId = audit.Id;
            }
              
            return steps;
        }
        
        #region Private Methods
        private IList<IAuditStep> GetEntitySteps(IAuditEntity entity, ref int stepOrder)
        {
            var steps = new List<IAuditStep>
            {
                { new AuditStep{ WizardPageType = WizardPageEnum.EntityInfo, StepOrder = stepOrder++}},
                { new AuditStep{ WizardPageType = WizardPageEnum.BusinessInfo, StepOrder = stepOrder++}},
                { new AuditStep{ WizardPageType = WizardPageEnum.Location, StepOrder = stepOrder++}},
                { new AuditStep{ WizardPageType = WizardPageEnum.Officers, StepOrder = stepOrder++}},
            };

            // Add payroll steps if audit has exposure basis of payroll
            if ((entity.ExposureBasis & ExposureBasisEnum.Payroll) == ExposureBasisEnum.Payroll)
                steps.AddRange(new List<IAuditStep>
                {
                    { new AuditStep{ WizardPageType = WizardPageEnum.Payroll, StepOrder = stepOrder++}},
                    { new AuditStep{ WizardPageType = WizardPageEnum.Classification, StepOrder = stepOrder++}},
                    { new AuditStep{ WizardPageType = WizardPageEnum.Labor, StepOrder = stepOrder++}},
                });


            // Add sales steps if audit has exposure basis of sales
            if ((entity.ExposureBasis & ExposureBasisEnum.Sales) == ExposureBasisEnum.Sales)
                steps.AddRange(new List<IAuditStep>
                {
                    { new AuditStep{ WizardPageType = WizardPageEnum.SalesSetup, StepOrder = stepOrder++}},
                    { new AuditStep{ WizardPageType = WizardPageEnum.Sales, StepOrder = stepOrder++}},
                    { new AuditStep{ WizardPageType = WizardPageEnum.SalesVerification, StepOrder = stepOrder++}},
                });

            // Add final steps for entity
            steps.Add( new AuditStep { WizardPageType = WizardPageEnum.Questions, StepOrder = stepOrder++ });
            steps.Add( new AuditStep { WizardPageType = WizardPageEnum.Summary, StepOrder = stepOrder++ });

            // Set the entity id
            foreach (var step in steps)
            {
                step.EntityId = entity.Id;
            }

            return steps;
        }
        private IDictionary<ExposureBasisEnum, IReadOnlyList<IAuditStep>> GetExposureBasisSteps()
        {
            return new Dictionary<ExposureBasisEnum, IReadOnlyList<IAuditStep>>
            {
                {ExposureBasisEnum.Payroll, new List<IAuditStep>
                    {
                        { new AuditStep{ WizardPageType = WizardPageEnum.Payroll}},
                        { new AuditStep{ WizardPageType = WizardPageEnum.Classification}},
                        { new AuditStep{ WizardPageType = WizardPageEnum.Labor}}
                    }
                },
                {ExposureBasisEnum.Sales, new List<IAuditStep>
                    {
                        { new AuditStep{ WizardPageType = WizardPageEnum.SalesSetup}},
                        { new AuditStep{ WizardPageType = WizardPageEnum.Sales}},
                        { new AuditStep{ WizardPageType = WizardPageEnum.SalesVerification}},
                    }
                }
            };
        }
        
        #endregion Private Methods
    }
}
