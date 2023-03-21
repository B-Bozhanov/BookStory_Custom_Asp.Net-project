namespace HttpServer
{
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;

    using HttpServer.Common;
    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;
    using HttpServer.RoutTable;

    public class Server : IHttpServer, IRouteTable
    {
        private const int BufferSize = 4096;

        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        private readonly Dictionary<string, Func<HttpRequest, HttpResponse>> routingTable;

        public Server(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
            this.listener = new TcpListener(this.ipAddress, this.port);

            this.routingTable = new();
        }

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            //TODO: Validations:

            this.routingTable.Add(path.ToLower(), action);
        }

        public void RoutePaths()
        {
            throw new NotImplementedException();
        }

        public void RouteStaticFiles()
        {
            var path = GetEntryAssemblyPath();

            var staticFilesPaths = Directory.GetFiles("wwwroot/", "*", SearchOption.AllDirectories);

            foreach (var staticFilesPath in staticFilesPaths)
            {
                var filePath = staticFilesPath.Replace("wwwroot", string.Empty).Replace("\\", "/");

                this.routingTable.Add(filePath, (request) =>
                {
                    var fileExtencion = new FileInfo(staticFilesPath);

                    var contentType = HttpConstants.ContentType.GetContentType(fileExtencion.Extension);

                    return new HttpResponse(contentType, File.ReadAllBytes("wwwroot" + filePath));
                });
            }
        }

        private static string GetEntryAssemblyPath()
        {
            var assembly = Assembly.GetEntryAssembly();

            var path = assembly.Location;
            var fileName = new FileInfo(path).Name;

            return _ = path.Replace("\\", "/").Replace(fileName, string.Empty);
        }

        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine("Server is runing...");
            Console.WriteLine($"Listening on por {this.port}");

            while (true)
            {
                var clientConnection = await this.listener.AcceptTcpClientAsync();
                ProccessingClient(clientConnection);
            }
        }

        private async Task ProccessingClient(TcpClient clientConnection)
        {
            using NetworkStream stream = clientConnection.GetStream();

            var buffer = new byte[BufferSize];

            var requestBuilder = new StringBuilder();

            while (true)
            {
                var readedBytes = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length));

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, readedBytes).ToLower());

                if (readedBytes < buffer.Length)
                {
                    break;
                }
            }

            var request = new HttpRequest().Parse(requestBuilder.ToString());

            HttpResponse response;

            if (this.routingTable.ContainsKey(request.Path))
            {
                var action = this.routingTable[request.Path];
                response = action(request);
            }
            else 
            {
                response = new HttpResponse("test/html", Array.Empty<byte>(), Http.Enums.HttpStatusCode.NotFound);
            }

            await stream.WriteAsync(response.GetHeadresBytes());
            await stream.WriteAsync(response.Body);

            clientConnection.Close();
        }
    }
}
