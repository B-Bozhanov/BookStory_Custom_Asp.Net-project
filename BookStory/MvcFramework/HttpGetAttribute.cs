namespace MvcFramework
{
    using HttpServer.Http.Enums;

    public class HttpGetAttribute : BaseAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.Get;
    }
}
