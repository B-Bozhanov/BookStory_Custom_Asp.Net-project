using System.Reflection;

using BookStory.App.Controllers;

using HttpServer;
using HttpServer.Common;
using HttpServer.Http.HttpResponses;

using MvcFramework;

//TODO: Move ipAddress and por from here:
var ipAddress = "127.0.0.1";
var port = 80;

IHttpServer server = new Server(ipAddress, port);

server.RouteStaticFiles();
//server.RoutePaths();

//AutoRouteStaticFiles(server);

AutoRoutePaths(server);

await server.Start();

static void AutoRoutePaths(IHttpServer server)
{
    var controllers = Assembly.GetCallingAssembly()
        .GetTypes()
        .Where(t => t.IsClass && t.IsSubclassOf(typeof(Controller))
                              && !t.IsAbstract && t.IsPublic);

    foreach (var controller in controllers)
    {
        var controllerName = controller.Name.Replace(nameof(Controller), string.Empty);
        var methods = controller
            .GetMethods(BindingFlags.DeclaredOnly |
                        BindingFlags.Public |
                        BindingFlags.Instance)
            .Where(m => !m.IsConstructor && !m.IsSpecialName);

        var controllerInstance = Activator.CreateInstance(controller) as Controller;

        foreach (var method in methods)
        {
            var parameters = method.GetParameters().Length;

            var path = string.Empty;

            if (method.Name == nameof(HomeController.Index))
            {
                path = "/";
            }
            else
            {
                path = $"/{controllerName}/{method.Name}";
            }

            server.AddRoute(path, (request) =>
            {
                var response = method.Invoke(controllerInstance, new object[parameters]) as HttpResponse;

                return response;
            });
        }
    }
}
static void AutoRouteStaticFiles(IHttpServer server)
{
    var staticFilesPaths = Directory.GetFiles("wwwroot/", "*", SearchOption.AllDirectories);

    foreach (var staticFilesPath in staticFilesPaths)
    {
        var filePath = staticFilesPath.Replace("wwwroot", string.Empty).Replace("\\", "/");

        server.AddRoute(filePath, (request) =>
        {
            var fileExtencion = new FileInfo(staticFilesPath);

            var contentType = HttpConstants.ContentType.GetContentType(fileExtencion.Extension);

            return new HttpResponse(contentType, File.ReadAllBytes("wwwroot" + filePath));
        });
    }
}