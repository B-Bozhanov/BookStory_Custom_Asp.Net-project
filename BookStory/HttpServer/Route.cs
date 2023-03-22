namespace HttpServer
{
    using HttpServer.Http;
    using HttpServer.Http.Enums;
    using HttpServer.Http.HttpResponses;

    public class Route
    {
        public Route(string url, HttpMethod method, Func<HttpRequest, HttpResponse> action )
        {
            this.Url = url;
            this.HttpMethod = method;
            this.Action = action;
        }

        public string Url { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}
