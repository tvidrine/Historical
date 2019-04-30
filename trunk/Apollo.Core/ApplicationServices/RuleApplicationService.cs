// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/21/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Apollo.Core.Base;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Contracts.DomainServices.Rules;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Extensions;
using Apollo.Core.Messages.Responses;
using RuleSet = Apollo.Core.Domain.Rule.RuleSet;

namespace Apollo.Core.ApplicationServices
{
    public class RuleApplicationService : IRuleApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IRuleSetRepository _ruleSetRepository;
        private readonly IRuleSetTranslator _ruleSetTranslator;
        private readonly IRuleSetAssemblyService _ruleAssemblyService;

        public RuleApplicationService(ILogManager logManager, IRuleSetRepository ruleSetRepository, IRuleSetTranslator ruleSetTranslator, IRuleSetAssemblyService ruleAssemblyService)
        {
            _logManager = logManager;
            _ruleSetRepository = ruleSetRepository;
            _ruleSetTranslator = ruleSetTranslator;
            _ruleAssemblyService = ruleAssemblyService;
        }
        public async Task<ICreateResponse<IRuleSet>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IRuleSet>
            {
                Content = new RuleSet(),
            });
        }

        public GetResponse<IRuleSet> CreateAssembly(IRuleSet ruleSet, bool forTesting = false)
        {
            var response = new GetResponse<IRuleSet>();
            try
            {
                // 1. Translate the code
                ruleSet.Code = _ruleSetTranslator.Translate(ruleSet, forTesting);

                // 2. Create the assembly
                ruleSet.GeneratedAssembly = _ruleAssemblyService.CreateAssembly(ruleSet);

                response.Content = ruleSet;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response.AddError(ex);
                _logManager.LogError(ex, "RuleApplicationService.CreateAssembly");
            }

            return response;

        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _ruleSetRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete ruleSet");
            }

            return deleteResponse;
        }

        public object ExecuteRule(IRuleSet ruleset, string ruleName)
        {
            var generatedAssembly = _ruleAssemblyService.CreateAssembly(ruleset);

            if (generatedAssembly != null)
            {
                var assembly = Assembly.Load(generatedAssembly);
                var className = $@"{Constants.RuleNamespace}.{ruleset.Name.RemoveWhitespace()}";
                var rule = ruleset.Rules.FirstOrDefault(r => r.Name == ruleName);

                if (rule != null)
                {
                    var type = assembly.GetType(className);
                    var methodName = rule.Name.RemoveWhitespace();
                    var obj = Activator.CreateInstance(type);
                    return type.InvokeMember(methodName,
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        obj,
                        null);
                }
                
            }
            
            return null;
        }

        public async Task<GetResponse<IRuleSet>> GetByIdAsync(int id)
        {
            var getResponse = new GetResponse<IRuleSet>();
            try
            {
                getResponse = await _ruleSetRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving ruleSet");
            }

            return getResponse;
        }

        public async Task<GetResponse<IRuleSet>> GetByNameAsync(string name)
        {
            var getResponse = new GetResponse<IRuleSet>();
            try
            {
                getResponse = await _ruleSetRepository.GetByNameAsync(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving ruleSet");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IRuleSet>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IRuleSet>>();
            try
            {
                getResponse = await _ruleSetRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving ruleSets");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IRuleSet>> SaveAsync(IRuleSet ruleSet)
        {
            var saveResponse = new SaveResponse<IRuleSet>();
            try
            {
                saveResponse = await _ruleSetRepository.SaveAsync(ruleSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving ruleSet");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IRuleSet>>> SaveAllAsync(IReadOnlyList<IRuleSet> ruleSets)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IRuleSet>>();
            try
            {
                saveResponse = await _ruleSetRepository.SaveAllAsync(ruleSets);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving ruleSets");
            }

            return saveResponse;
        }

    }
}