namespace HttpServer.RoutTable
{
    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;

    public class RouteTable : IRouteTable
    {
        public string Path { get; set; }

        public Func<HttpRequest, HttpResponse> Action { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public void AddRoute(string route, Func<HttpRequest, HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public void RoutePaths()
        {
            throw new NotImplementedException();
        }

        public void RouteStaticFiles()
        {
            throw new NotImplementedException();
        }

        public void RoutPaths()
        {
            throw new NotImplementedException();
        }

        public void RoutStaticFiles()
        {
            throw new NotImplementedException();
        }
    }
}
