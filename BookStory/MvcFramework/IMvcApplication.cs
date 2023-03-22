namespace MvcFramework
{
    using HttpServer;

    public interface IMvcApplication
    {
        public void ConfigurateServices();

        public void Configurate(IDictionary<string, Route> routeTable);
    }
}
