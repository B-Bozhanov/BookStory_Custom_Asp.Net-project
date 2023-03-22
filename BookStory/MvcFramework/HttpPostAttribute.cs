namespace MvcFramework
{
    using HttpServer.Http.Enums;

    public class HttpPostAttribute : BaseAttribute
    {
        public HttpPostAttribute()
        {
        }

        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.Post;
    }
}
