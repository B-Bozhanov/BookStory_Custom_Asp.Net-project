namespace HttpServer
{
    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;
    using HttpServer.RoutTable;

    public interface IHttpServer : IRouteTable
    {
        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action);

        public Task Start();
    }
}
