namespace Apollo.Core.Messages.Responses
{
    public sealed class GetResponse<T> : BaseResponse
    {
        public T Content { get; set; }
        public override object GetContent()
        {
            return Content;
        }
    }
}