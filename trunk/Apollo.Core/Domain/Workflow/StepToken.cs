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
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Contracts.Workflow;

namespace Apollo.Core.Domain.Workflow
{
    public class StepToken : IToken
    {
        public StepToken()
        {
            Exception = null;
            Message = string.Empty;
            Params = new Dictionary<string, object>();
        }

        public Exception Exception { get; private set; }
        public bool IsSuccessful => Exception == null;
        public string Message { get; set; }
        public IAudit Audit { get; set; }
        public IClient Client { get; set; }
        public MemoryStream Document { get; set; }
        public IDictionary<string, object> Params { get; }
        public string ActivityNote { get; private set; }

        public IToken CopyTo<T>(T token) where T : IToken
        {
            token.Audit = Audit;
            token.Client = Client;

            foreach (var param in Params)
            {
                token.Params.Add(param.Key, param.Value);
            }
            return token;
        }

        public void SetActivityNote(string note)
        {
            if (string.IsNullOrEmpty(note))
                return;

            ActivityNote = string.IsNullOrEmpty(ActivityNote) ? note : $@"{ActivityNote}{Environment.NewLine}{note}";
        }
        public void SetException(Exception ex)
        {
            Exception = ex;
            Message = ex.Message;
        }
    }
}