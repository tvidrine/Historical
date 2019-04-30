// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/05/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Domain.Rule;
using Apollo.Core.DomainServices.Rules;
using Apollo.Core.Extensions;
using Apollo.Core.Messages.Results;
using FluentAssertions;
using Moq;
using Xunit;

namespace Apollo.Core.Tests.DomainServices
{
    public class RuleSetTranslatorTests
    {
        #region Traanslate Tests
        [Fact]
        public void TranslateTest()
        {
            var ruleSet = CreateTestRuleSet();
            var expectedCode = CreateExpectedCode();
            var mockBusinessRuleParser = new Mock<IBusinessRuleParser>();

            mockBusinessRuleParser.Setup(x => x.Translate(It.IsAny<Rule>()))
                .Returns(new TranslateResult
                {
                    Code = @"
                            if(token.Audit.AuditStatus == AuditStatuses.Submitted && token.Audit.Policy.EffectiveEnd >= DateTime.Now.AddDays(-token.Client.Settings.WelcomeLetterDays))
                            {
                                token.Params.Add(""ReportType"", ""Welcome"");
                                token.Exception = _actions[""GenerateLetter""](token);
                            }"
                });

            // Act
            var sut = new RuleSetTranslator(mockBusinessRuleParser.Object);
            var code = sut.Translate(ruleSet, false);

            // Assert
            code.Length.Should().BeGreaterThan(0);
            code.RemoveWhitespace().Should().Be(expectedCode.RemoveWhitespace());

            mockBusinessRuleParser.Verify(x => x.Translate(It.IsAny<Rule>()));
        }
        #endregion Translate Tests

        #region Private Methods
        private string CreateExpectedCode()
        {
            return @"
                    using System;
                    using System.Collections.Generic;
                    using Apollo.Core.Contracts.Workflow;
                    using Apollo.Core.Domain.Audit;

                    namespace Apollo.RulesEngine.Rules
                    {
                        public class TestRuleSet
                        {
                            private readonly Dictionary<string, Func<Exception, IToken> _actions;

                            public TestRuleSet(Dictionary<string, Func<Exception, IToken> actions)
                            {
                                _actions = actions;
                            }

                            if(token.Audit.AuditStatus == AuditStatuses.Submitted && token.Audit.Policy.EffectiveEnd >= DateTime.Now.AddDays(-token.Client.Settings.WelcomeLetterDays))
                            {
                                token.Params.Add(""ReportType"", ""Welcome"");
                                token.Exception = _actions[""GenerateLetter""](token);
                            }
                        }
                    }";
        }
        private IRuleSet CreateTestRuleSet()
        {
            return new RuleSet
            {
                Name = "Test Rule Set",
                Rules = new List<IRule>
                {
                    new Rule { Body = "If an audit is ordered and the audit is workable, then generate the welcome letter.", IsPublished = true}
                }
            };
        }
        #endregion Private Methods
    }
}