// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/20/2018
// ------------------------------------------------------------------------------------------------------------------------

using Apollo.RulesEngine.Lexer;

namespace Apollo.RulesEngine.Contracts
{
    public interface ILexer
    {
        Token GetNextToken();
        Token PeekNextToken();
        void ConsumeNextToken();
        ILexer LoadLexer(string rule);
    }
}