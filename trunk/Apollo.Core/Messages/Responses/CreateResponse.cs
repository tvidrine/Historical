// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 04/09/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Messages.Responses
{
	public interface ICreateResponse<out T> : IBaseResponse<T>
	{
	}
    public class CreateResponse<T> : BaseResponse, ICreateResponse<T>
    {
        public T Content { get; set; }
        public override object GetContent()
        {
            return Content;
        }
    }

}