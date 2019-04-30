// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/20/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Apollo.Core.Contracts;
using Apollo.Core.Messages.Responses;

namespace Apollo.Core.ApplicationServices
{
    public class TokenizerService
    {
        private readonly ILogManager _logManager;

        public TokenizerService(ILogManager logManager)
        {
            _logManager = logManager;
        }

        public GetResponse<object> GetValue(object source, string fieldName)
        {

            return new GetResponse<object>
            {
                Content = source.GetType().GetProperty(fieldName)?.GetValue(source, null)
            };
        }

        public GetResponse<T> GetValue<T>(object source, string fieldName)
        {
            return new GetResponse<T>
            {
                Content = (T) source.GetType().GetProperty(fieldName)?.GetValue(source,null)
            };
        }

        public GetResponse<IDictionary<string, object>> GetValues(object source, IList<string> fieldNames)
        {
            return new GetResponse<IDictionary<string, object>>
            {
                Content = fieldNames
                    .ToDictionary(f => f, f => source.GetType().GetProperty(f)?.GetValue(source, null))
            };
        }
    }
}