namespace MvcFramework
{
    using HttpServer;
    using HttpServer.Common;
    using HttpServer.Http.Enums;
    using HttpServer.Http.HttpResponses;

    public class Host
    {
        private const string DefaultIpAddress = "127.0.0.1";
        private const int DefaultPort = 80;

        public static async Task CreateHostAsync(IMvcApplication application, string ipAddress = DefaultIpAddress, int port = DefaultPort)
        {
            var routeTable = new Dictionary<string, Route>();

            AutoRouteStaticFiles(routeTable);
            AutoRoutePaths(routeTable, application);

            application.ConfigurateServices();
            application.Configurate(routeTable);

            IHttpServer httpServer = new Server(ipAddress, port, routeTable);
            await httpServer.StartAsync();
        }

        private static void AutoRouteStaticFiles(Dictionary<string, Route> routeTable)
        {
            var staticFilesPaths = Directory.GetFiles("wwwroot/", "*", SearchOption.AllDirectories);

            foreach (var staticFilesPath in staticFilesPaths)
            {
                var filePath = staticFilesPath.Replace("wwwroot", string.Empty).Replace("\\", "/").ToLower();

                routeTable.Add(filePath, new Route(filePath, HttpMethod.Get, (request) =>
                {
                    var fileExtencion = new FileInfo(staticFilesPath);

                    var contentType = HttpConstants.ContentType.GetContentType(fileExtencion.Extension);

                    return new HttpResponse(contentType, File.ReadAllBytes(staticFilesPath));
                }));
            }
        }

        private static void AutoRoutePaths(Dictionary<string, Route> routeTable, IMvcApplication application)
        {
            var controllers = application
                    .GetType()
                    .Assembly
                    .GetTypes()
                    .Where(t => t.IsClass
                           && t.IsSubclassOf(typeof(Controller))
                           && !t.IsAbstract && t.IsPublic);

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace(nameof(Controller), string.Empty);
                var methods = controller
                    .GetMethods()
                    .Where(m => !m.IsConstructor
                           && !m.IsSpecialName
                           && m.DeclaringType == controller);


                var controllerInstance = Activator.CreateInstance(controller) as Controller;

                foreach (var method in methods)
                {
                    var path = $"/{controllerName}/{method.Name}".ToLower();

                    var httpMethod = HttpMethod.Get;

                    var attribute = method.GetCustomAttributes(false)
                        .Where(a => a.GetType().IsSubclassOf(typeof(BaseAttribute)))
                        .FirstOrDefault() as BaseAttribute;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        path = attribute.Url;
                    }

                    routeTable.Add(path, new Route(path, httpMethod, (request) =>
                    {
                        var response = method.Invoke(controllerInstance, new object[] { request }) as HttpResponse;

                        return response!;
                    }));
                }
            }
        }
    }
}
