// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 10/19/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Messages.Responses;
using Apollo.RulesEngine.Contracts;
using Apollo.RulesEngine.Models;

namespace Apollo.RulesEngine.ApplicationServices
{
    public class RuleGrammarTerminalApplicationService : IRuleGrammarTerminalApplicationService
    {
        private readonly ILogManager _logManager;
        private readonly IRuleGrammarTerminalRepository _ruleGrammarTerminalRepository;

        public RuleGrammarTerminalApplicationService(ILogManager logManager, IRuleGrammarTerminalRepository ruleGrammarTerminalRepository)
        {
            _logManager = logManager;
            _ruleGrammarTerminalRepository = ruleGrammarTerminalRepository;
        }

        public async Task<ICreateResponse<IRuleGrammarTerminal>> CreateAsync()
        {
            return await Task.Run(() => new CreateResponse<IRuleGrammarTerminal>
            {
                Content = new RuleGrammarTerminal(),
            });
        }

        public async Task<DeleteResponse> DeleteAsync(int id)
        {
            var deleteResponse = new DeleteResponse();
            try
            {
                deleteResponse = await _ruleGrammarTerminalRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteResponse.AddError(ex);
                _logManager.LogError(ex, "Unable to delete ruleGrammarTerminal");
            }

            return deleteResponse;
        }

        public async Task<GetResponse<IRuleGrammarTerminal>> GetAsync(int id)
        {
            var getResponse = new GetResponse<IRuleGrammarTerminal>();
            try
            {
                getResponse = await _ruleGrammarTerminalRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving ruleGrammarTerminal");
            }

            return getResponse;
        }

        public async Task<GetResponse<IReadOnlyList<IRuleGrammarTerminal>>> GetAllAsync()
        {
            var getResponse = new GetResponse<IReadOnlyList<IRuleGrammarTerminal>>();
            try
            {
                getResponse = await _ruleGrammarTerminalRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                getResponse.AddError(ex);
                _logManager.LogError(ex, "Error retrieving ruleGrammarTerminals");
            }

            return getResponse;
        }

        public async Task<SaveResponse<IRuleGrammarTerminal>> SaveAsync(IRuleGrammarTerminal ruleGrammarTerminal)
        {
            var saveResponse = new SaveResponse<IRuleGrammarTerminal>();
            try
            {
                saveResponse = await _ruleGrammarTerminalRepository.SaveAsync(ruleGrammarTerminal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving ruleGrammarTerminal");
            }

            return saveResponse;
        }

        public async Task<SaveResponse<IReadOnlyList<IRuleGrammarTerminal>>> SaveAllAsync(IReadOnlyList<IRuleGrammarTerminal> ruleGrammarTerminals)
        {
            var saveResponse = new SaveResponse<IReadOnlyList<IRuleGrammarTerminal>>();
            try
            {
                saveResponse = await _ruleGrammarTerminalRepository.SaveAllAsync(ruleGrammarTerminals);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                saveResponse.AddError(ex);
                _logManager.LogError(ex, "Error saving ruleGrammarTerminals");
            }

            return saveResponse;
        }
    }
}
