// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 09/18/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.RulesEngine.Lexer
{
    public enum TokenType
    {
        InvalidToken,
        EOR ,
        Comma,
        Integer,
        NoOp,
        Keyword,
        ActionSubject,
        AuditEvent,
        Action,
        SimpleSubject,
        SimpleVerb,
        TimePeriod,
        If,
        Then,
        Else,
        And,
        Or,
        Article
    }
}