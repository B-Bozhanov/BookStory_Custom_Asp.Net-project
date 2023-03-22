namespace HttpServer
{
    using HttpServer.RoutTable;

    public interface IHttpServer 
    {
        public Task StartAsync();
    }
}
