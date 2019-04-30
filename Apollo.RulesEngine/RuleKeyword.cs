// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/20/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Apollo.RulesEngine.Lexer;

namespace Apollo.RulesEngine
{
    public static class RuleKeyword
    {
        public static readonly Dictionary<string, TokenType> KeywordCache;

        static RuleKeyword()
        {
            KeywordCache = new Dictionary<string, TokenType>
            {
                { "if", TokenType.If},
                {"then", TokenType.Then },
                {"and", TokenType.And },
                {"or", TokenType.Or },
                {"else", TokenType.Else },
                {"within", TokenType.Keyword },
                {"of", TokenType.Keyword },
                {"have", TokenType.Keyword },
                {"lapsed", TokenType.Keyword },
                {"since", TokenType.Keyword },
                {"at", TokenType.Keyword },
                {"from", TokenType.Keyword },
                {"a", TokenType.Article },
                {"an", TokenType.Article },
                {"the", TokenType.Article },
                {"is", TokenType.NoOp },
                {"support", TokenType.Keyword },
                {"call", TokenType.Keyword },
                {"welcome", TokenType.Keyword },
                {"letter", TokenType.Keyword },
                {"login", TokenType.Keyword },
                {"first", TokenType.Keyword },
                {"contact", TokenType.Keyword },
                {"task", TokenType.Keyword },
                {"completed", TokenType.Keyword },
                {"generate", TokenType.Keyword },
                {"audit", TokenType.Keyword },
                {"policy", TokenType.Keyword },
                {"holder", TokenType.Keyword },
                {"insured", TokenType.Keyword },
                {"agent", TokenType.Keyword },
                {"ordered", TokenType.Keyword },
                {"workable", TokenType.Keyword },
                {"established", TokenType.Keyword },
                {"days", TokenType.Keyword },
                {"minutes", TokenType.Keyword },
                {"months", TokenType.Keyword},
                {"years", TokenType.Keyword }
            };
            
        }

        public static TokenType GetKeywordTokenType(string buffer)
        {
            return KeywordCache.ContainsKey(buffer) ? KeywordCache[buffer] : TokenType.InvalidToken;
        }
    }
}