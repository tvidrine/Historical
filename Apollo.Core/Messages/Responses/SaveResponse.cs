using System;
using System.Collections.Generic;
using Apollo.Core.Base;

namespace Apollo.Core.Messages.Responses
{
    public class SaveResponse : BaseResponse
    {
    }

    public class SaveResponse<T> : SaveResponse
    {
        public T Content { get; set; }

        public override object GetContent()
        {
            return Content;
        }

        public void AddErrors(IList<Failure> errors)
        {
            ((List<Failure>)Errors)
                .AddRange(errors);
        }
    }
}