namespace HttpServer
{
    using HttpServer.Http;
    using HttpServer.Http.Enums;
    using HttpServer.Http.HttpResponses;

    public class Route
    {
        public Route(string url, HttpMethod method, Func<HttpRequest, HttpResponse> action)
        {
            this.Url = url;
            this.Method = method;
            this.Action = action;
        }

        public string Url { get; }

        public HttpMethod Method { get; }

        public Func<HttpRequest, HttpResponse> Action { get; }
    }
}
