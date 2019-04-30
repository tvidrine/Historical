// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/05/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Apollo.Core.ApplicationServices;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Contracts.Repositories;
using Apollo.Core.Domain.Rule;
using Apollo.Core.DomainServices.Rules;
using Apollo.Core.Messages.Responses;
using Apollo.RulesEngine;
using Apollo.RulesEngine.Contracts;
using Apollo.RulesEngine.Models;
using Apollo.RulesEngine.Parser;
using FluentAssertions;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.ApplicationServices
{
    public class RuleApplicationServiceIntegrationTests
    {
        #region CreateAssembly Tests
        [Fact]
        public void CreatePublishedAssemblyTest()
        {
            // Arrange
            var mockLogManager = new Mock<ILogManager>();
            var mockRuleSetRepository = new Mock<IRuleSetRepository>();
            var mockRuleTerminalApplicationService = new Mock<IRuleGrammarTerminalApplicationService>();
            mockRuleTerminalApplicationService.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(GetMockRuleTerminals()));

            var businessRuleParser =
                new BusinessRuleParser(mockLogManager.Object, mockRuleTerminalApplicationService.Object);

            var rulesetTranslator = new RuleSetTranslator(businessRuleParser);
            var ruleAssemblyService = new RuleSetAssemblyService(mockLogManager.Object);

           
            var testRuleSet = CreateTestRuleSet();
            
            // Act
            var sut = new RuleApplicationService(
                mockLogManager.Object, mockRuleSetRepository.Object, rulesetTranslator, ruleAssemblyService);

            var response = sut.CreateAssembly(testRuleSet, true);

            // Assert
            response.IsSuccessful.Should().BeTrue();
            response.Errors.Count.Should().Be(0);
            response.Content.GeneratedAssembly.Length.Should().BeGreaterThan(0);

        }
        #endregion CreateAssembly Tests

        #region Private Methods
        private IRuleSet CreateTestRuleSet()
        {
            return new RuleSet
            {
                Name = "Test Rule Set",
                Rules = new List<IRule>
                {
                    new Rule { Name = "Test Rule", Body = "If an audit is ordered and the audit is workable, then generate the welcome letter.", IsPublished = true}
                }
            };
        }
        private GetResponse<IReadOnlyList<IRuleGrammarTerminal>> GetMockRuleTerminals()
        {
            var response = new GetResponse<IReadOnlyList<IRuleGrammarTerminal>>
            {
                Content = new List<IRuleGrammarTerminal>
                {
                    new RuleGrammarTerminal
                    {
                        Keyword = "welcome",
                        SupportingKeywords = new [] {"letter"},
                        TerminalType = TerminalTypes.ActionSubject,
                        TranslateTo = @"token.Params.Add(""ReportType"", ""Welcome"");"
                    },
                    new RuleGrammarTerminal
                    {
                        Keyword = "generate",
                        TerminalType = TerminalTypes.ActionVerb,
                        TranslateTo = @"token.Exception = _actions[""GenerateLetter""](token);"
                    },
                    new RuleGrammarTerminal
                    {
                        Keyword = "audit",
                        TerminalType = TerminalTypes.ConditionSubject,
                        TranslateTo = @"Audit"
                    },
                    new RuleGrammarTerminal
                    {
                        Keyword = "ordered",
                        TerminalType = TerminalTypes.ConditionVerb,
                        TranslateTo = @".AuditStatus == AuditStatuses.Submitted"
                    },
                    new RuleGrammarTerminal
                    {
                        Keyword = "workable",
                        TerminalType = TerminalTypes.ConditionVerb,
                        TranslateTo = @".Policy.EffectiveEnd >= DateTime.Now.AddDays(-token.Client.Settings.WelcomeLetterDays)"
                    }
                }
            };

            return response;

        }
        #endregion Private Methods
    }
}