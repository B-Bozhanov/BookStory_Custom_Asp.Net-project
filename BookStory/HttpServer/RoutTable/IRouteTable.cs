namespace HttpServer.RoutTable
{
    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;

    public interface IRouteTable
    {
        public void AddRoute(string route, Func<HttpRequest, HttpResponse> action);

        public void RouteStaticFiles();

        public void RoutePaths();
    }
}
