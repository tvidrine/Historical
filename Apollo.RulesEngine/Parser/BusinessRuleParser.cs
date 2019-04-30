// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/20/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Messages.Results;
using Apollo.RulesEngine.Contracts;
using Apollo.RulesEngine.Lexer;

namespace Apollo.RulesEngine.Parser
{
    public class BusinessRuleParser : IBusinessRuleParser
    {
        private const string ErrorMessageFormat = "**Error://Expecting '{0}', but found '{1}' instead.**";
        private readonly IDictionary<TerminalTypes, Dictionary<string, IRuleGrammarTerminal>> _terminalCache;
        private readonly ILogManager _logManager;

        public BusinessRuleParser(ILogManager logManager, IRuleGrammarTerminalApplicationService ruleGrammarTerminalApplicationService)
        {
            _logManager = logManager;
            _terminalCache = GetRuleTerminals(ruleGrammarTerminalApplicationService);
        }

        
        public TranslateResult Translate(IRule rule)
        {
            var lexer = new BusinessRuleLexer()
                .LoadLexer(rule.Body);

            var result = BusinessRule(lexer);
            
            result.Code = $@"public IToken {GetClassName(rule)}(IToken token)
                            {{
                                {result.Code}

                                return token;
                            }}";
            
            return result;
        }

        #region Grammar Rules

        private TranslateResult BusinessRule(ILexer lexer)
        {
            var result = new TranslateResult();

            var token = lexer.PeekNextToken();

            if (token.Type == TokenType.If)
            {
                result = IfClause(lexer);
            }
            else
            {
                result.ErrorMessage = string.Format(ErrorMessageFormat, "If", token.Text);
            }
            
            return result;
        }
        // IfStatement ::= "If" Condition "Then" ActionClause ElseClause?
        private TranslateResult IfClause(ILexer lexer)
        {
            var result = new TranslateResult();
            lexer.ConsumeNextToken();

            // Execute ConditionStatement
            var conditionClauseResult = ConditionClause(lexer);
            result.Join(conditionClauseResult);

            // Check for "Then"
            var token = lexer.PeekNextToken();

            if (token.Type != TokenType.Then)
            {
                result.ErrorMessage = string.Format(ErrorMessageFormat, "then", token.Text);

                return result;
            }

            lexer.ConsumeNextToken();
            var actionClauseResult = ActionClause(lexer);
            result.Join(actionClauseResult);

            if (result.IsSuccessful)
            {
                // Execute ElseClause
                result.Code = $@"if({conditionClauseResult.Code})
                                {{
                                    {actionClauseResult.Code}
                                }}";
            }

            return result;
        }

        // ActionClause ::= Action Article ActionSubjectTerminal
        private TranslateResult ActionClause(ILexer lexer)
        {
            var result = new TranslateResult();
            var simpleActionResult = ActionVerbTerminal(lexer);
            result.Join(simpleActionResult);

            if (result.IsSuccessful)
            {
                var nextToken = lexer.PeekNextToken();

                while (nextToken.Type == TokenType.Article)
                {
                    lexer.ConsumeNextToken();
                    nextToken = lexer.PeekNextToken();
                }

                var actionSubjectResult = ActionSubjectTerminal(lexer);
                result.Join(actionSubjectResult);

                if (result.IsSuccessful)
                {
                    result.Code =$"{actionSubjectResult.Code}\n\t\t{simpleActionResult.Code}";
                }
            }

            return result;
        }
        // Condition ::= SimpleCondition | SimpleCondition ("and" | "or") Condition
        private TranslateResult ConditionClause(ILexer lexer)
        {
            var result = SimpleConditionClause(lexer);

            if (result.IsSuccessful)
            {
                var nextToken = lexer.PeekNextToken();

                if (nextToken.Type == TokenType.And || nextToken.Type == TokenType.Or)
                {
                    var conditionOperator = GetConditionalOperator(lexer);
                    var conditionClauseResult = ConditionClause(lexer);
                    result.Join(conditionClauseResult);
                    result.Code = $@"{result.Code} {conditionOperator} {conditionClauseResult.Code}";
                }
            }
            
            return result;
        }

        //  SimpleCondition ::= SubjectClause VerbClause TimeCondition?
        private TranslateResult SimpleConditionClause(ILexer lexer)
        {
            var result = SubjectClause(lexer);

            if (result.IsSuccessful)
            {
                var verbclauseResult = VerbClause(lexer);
                result.Join(verbclauseResult);

                if (result.IsSuccessful)
                {
                    result.Code = $@"token.{result.Code}{verbclauseResult.Code}";
                }
            }
            return result;
        }

        // SubjectClause ::= ( "a" | "an" | "the")? ConditionSubjectTerminal
        private TranslateResult SubjectClause(ILexer lexer)
        {
            var nextToken = lexer.PeekNextToken();

            while (nextToken.Type == TokenType.Article)
            {
                lexer.ConsumeNextToken();
                nextToken = lexer.PeekNextToken();
            }
            
            var result = ConditionSubjectTerminal(lexer);

            return result;
        }

        // VerbClause ::= ("is")? ConditionVerbTerminal
        private TranslateResult VerbClause(ILexer lexer)
        {
            var nextToken = lexer.PeekNextToken();

            while (nextToken.Type == TokenType.NoOp)
            {
                lexer.ConsumeNextToken();
                nextToken = lexer.PeekNextToken();
            }
            
            var result = ConditionVerbTerminal(lexer);
            
            return result;
        }

        #region Terminal Rules

        // ActionSubjectTerminal ::= "support" "call" | "welcome" "letter" | "login"
        private TranslateResult ActionSubjectTerminal(ILexer lexer)
        {
            return GetTerminalTranslatedCode(lexer, TerminalTypes.ActionSubject);
        }

        // ActionVerbTerminal ::= "generate" | "task"
        private TranslateResult ActionVerbTerminal(ILexer lexer)
        {
            return GetTerminalTranslatedCode(lexer, TerminalTypes.ActionVerb);
        }

        // ConditionSubjectTerminal ::= "audit" | "policy" "holder" | "insured" | "agent"
        private TranslateResult ConditionSubjectTerminal(ILexer lexer)
        {
            return GetTerminalTranslatedCode(lexer, TerminalTypes.ConditionSubject);
        }

        // ConditionVerbTerminal ::= "ordered" | "workable" | "established"
        private TranslateResult ConditionVerbTerminal(ILexer lexer)
        {
            return GetTerminalTranslatedCode(lexer, TerminalTypes.ConditionVerb);
        }
        #endregion Terminal Rules
        #endregion Grammar Rules

        #region Helper Methods
        private string GetClassName(IRule rule)
        {
            return Regex.Replace(rule.Name, @"\s+", "");
        }

        private string GetConditionalOperator(ILexer lexer)
        {
            var token = lexer.GetNextToken();

            return token.Type == TokenType.And ? "&&" : "||";
        }
        private string GetKeywordList(Dictionary<string, IRuleGrammarTerminal> terminals)
        {
            var buffer = terminals.Values
                .Select(t => t.Keyword)
                .ToArray();

            if (buffer.Length > 1)
                buffer[buffer.Length - 1] = $@"or {buffer[buffer.Length - 1]}";

            return buffer.Length == 1 ?
                buffer[0] :
                buffer.Length == 2 ?
                    string.Join(" ", buffer) :
                    string.Join(", ", buffer);
        }

        private Dictionary<TerminalTypes, Dictionary<string,IRuleGrammarTerminal>> GetRuleTerminals(IRuleGrammarTerminalApplicationService ruleGrammarTerminalApplicationService)
        {
            try
            {
                var result = ruleGrammarTerminalApplicationService.GetAllAsync().Result;

                if(!result.IsSuccessful)
                    throw new InvalidOperationException("Unable to load grammar terminals");

                return result.Content
                    .GroupBy(x => x.TerminalType)
                    .ToDictionary(x => x.Key, 
                        x => x.ToDictionary(t => t.Keyword, t => t));
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "BuslnessRuleParser.GetRuleTerminals");

                throw;
            }
        }

        private TranslateResult GetTerminalTranslatedCode(ILexer lexer, TerminalTypes terminalType)
        {
            var terminals = _terminalCache[terminalType];

            var result = new TranslateResult() {Code = " "};
            var token = lexer.GetNextToken();

            if (token.Type != TokenType.Keyword)
            {
                result.ErrorMessage = string.Format(ErrorMessageFormat, GetKeywordList(terminals), token.Text);

                return result;
            }

            if (!terminals.ContainsKey((token.Text.ToLower())))
                result.ErrorMessage = $@"Keyword not defined: {token.Text}";
            else
            {
                var terminal = terminals[token.Text.ToLower()];

                if (terminal.SupportingKeywords != null)
                {
                    // consume supporting keywords
                    for (var i = 0; i < terminal.SupportingKeywords.Length; i++)
                    {
                        var supportingKeyword = terminal.SupportingKeywords[i];

                        token = lexer.GetNextToken();

                        if (token.Type != TokenType.Keyword)
                        {
                            result.ErrorMessage = string.Format(ErrorMessageFormat, supportingKeyword, token.Text);

                            return result;
                        }
                    }
                }
                result.Code = terminal.TranslateTo;
            }
            return result;
        }
        #endregion Helper Methods
    }
}