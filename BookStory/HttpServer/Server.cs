namespace HttpServer
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;

    public class Server : IHttpServer
    {
        private const int BufferSize = 4096;

        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        private readonly IDictionary<string, Route> routeTable;

        public Server(string ipAddress, int port, IDictionary<string, Route> routeTable)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
            this.listener = new TcpListener(this.ipAddress, this.port);

            this.routeTable = routeTable;
        }

        public async Task StartAsync()
        {
            this.listener.Start();

            Console.WriteLine("Server is runing...");
            Console.WriteLine($"Listening on por {this.port}");

            while (true)
            {
                var clientConnection = await this.listener.AcceptTcpClientAsync();
                _ = ProccessingClient(clientConnection);
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

            if (this.routeTable.ContainsKey(request.Path))
            {
                var route = this.routeTable[request.Path];
                response = route.Action(request);
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
