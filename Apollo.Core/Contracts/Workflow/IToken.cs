// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 07/31/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Client;

namespace Apollo.Core.Contracts.Workflow
{
    public interface IToken
    {
        Exception Exception { get; }
        bool IsSuccessful { get; }
        string Message { get; set; }
        IAudit Audit { get; set; }
        IClient Client { get; set; }
        MemoryStream Document { get; set; }
        string ActivityNote { get; }
        IDictionary<string, object> Params { get; }
        IToken CopyTo<T>(T token) where T : IToken;

        void SetActivityNote(string note);
        void SetException(Exception ex);
       
    }
}