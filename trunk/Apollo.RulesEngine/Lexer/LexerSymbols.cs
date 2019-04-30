// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/19/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.RulesEngine.Lexer
{
    public class LexerSymbols
    {
        public static readonly char[] WhiteSpaceCharacters = { ' ', '\t', '\r'};
        public static readonly char[] Digits = { '0', '1', '2','3','4','5','6','7','8','9' };
        public static readonly char[] AlphaCharacters =
        {
            'A','a', 'B', 'b', 'C','c','D','d','E','e','F','f','G','g','H','h','I','i',
            'J','j','K','k','L','l','M','m','N','n','O','o','P','p','Q','q','R','r',
            'S','s','T','t','U','u','V','v','W','w','X','x','Y','y','Z','z'
        };
    }
}