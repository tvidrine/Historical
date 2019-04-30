// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.RulesEngine.Contracts;

namespace Apollo.RulesEngine.Lexer
{
    public class BusinessRuleLexer : ILexer
    {
        private char EOR = '.';
        private int _current;
        private char[] _ruleBuffer;
        private Queue<Token> _tokenQueue;

        public void ConsumeNextToken()
        {
            _tokenQueue.Dequeue();
        }

        public Token GetNextToken()
        {
            return _tokenQueue.Count == 0 ?
                new Token() { Text = "<EOR>", Type = TokenType.EOR }
                : _tokenQueue.Dequeue();
        }
        public ILexer LoadLexer(string rule)
        {
            if (string.IsNullOrEmpty(rule))
                throw new InvalidOperationException("Rule is blank");

            _tokenQueue = new Queue<Token>();
            _ruleBuffer = rule.ToCharArray();

            var currentToken = LoadNextToken();

            while (currentToken.Type != TokenType.EOR)
            {
                _tokenQueue.Enqueue(currentToken);
                currentToken = LoadNextToken();
            }

            return this;
        }

        public Token PeekNextToken()
        {
            return _tokenQueue.Count == 0 ?
                new Token() { Text = "<EOR>", Type = TokenType.EOR }
                : _tokenQueue.Peek();
        }

        #region Private Methods
        private Token LoadNextToken()
        {
            
            var c = GetChar();

            while (c != EOR)
            {
                if (IsWhiteSpace(c))
                {
                   ConsumeWhitespace();
                } 
                else if (IsDigit(c))
                {
                    return ConsumeInteger();
                }
                else if(IsAlphaCharacter(c))
                {
                    return ConsumeKeyword();
                }
                else
                {
                    _current++;
                }
               
                c = GetChar();
            }

            return new Token() {Text = "<EOR>", Type = TokenType.EOR};
        }

        private bool IsAlphaCharacter(char c)
        {
            return LexerSymbols.AlphaCharacters.Contains(c);
        }
        private bool IsDigit(char c)
        {
            return LexerSymbols.Digits.Contains(c);
        }
        private bool IsWhiteSpace(char c)
        {
            return LexerSymbols.WhiteSpaceCharacters.Contains(c);
        }

        private Token ConsumeInteger()
        {
            var c = GetChar();
            var buffer = new StringBuilder();
            while (IsDigit(c))
            {
                buffer.Append(c);
                _current++;
                c = GetChar();
            }
            return new Token
            {
                Type = TokenType.Integer,
                Text = buffer.ToString()
            };
        }

        private Token ConsumeKeyword()
        {
            var c = _ruleBuffer[_current];
            var buffer = new StringBuilder();

            while (IsAlphaCharacter(c))
            {
                buffer.Append(c);
                _current++;
                c = GetChar();
            }

            var text = buffer.ToString();
            return new Token
            {
                Type = RuleKeyword.GetKeywordTokenType(text.ToLower()),
                Text = text
            };
        }
        private void ConsumeWhitespace()
        {
            var c = _ruleBuffer[_current];
            while (IsWhiteSpace(c))
            {
                _current++;
                c = GetChar();
            }
        }

        private char GetChar()
        {
            return _current == _ruleBuffer.Length ? EOR : _ruleBuffer[_current];
        }
        #endregion Private Methods
    }
}