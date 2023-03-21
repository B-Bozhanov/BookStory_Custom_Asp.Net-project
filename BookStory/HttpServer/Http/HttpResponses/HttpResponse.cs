namespace HttpServer.Http.HttpResponses
{
    using System.Text;

    using HttpServer.Common;
    using HttpServer.Http.Enums;
    using HttpServer.Http.HttpCollections;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers = new();
        }

        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            this.Body = body;
            this.StatusCode = statusCode;
            this.ContentType = contentType;

            this.Headers = new HttpCollection
            {
                {new Header("Content-Type", this.ContentType) },
                {new Header("Content-Length", this.Body.Length.ToString()) }
            };
        }

        public HttpStatusCode StatusCode { get; set; }

        public string ContentType { get; private set; }

        public int ContentLength => this.Body.Length;

        public HttpCollection Headers { get; private set; }

        public byte[] Body { get; private set; }

        public byte[] GetHeadresBytes()
        {
            return Encoding.UTF8.GetBytes(this.HttpResponseString());
        }

        public static HttpResponse Redirect(string url)
        {
            var respone = new HttpResponse(HttpStatusCode.Redirected);
            respone.Headers.Add(new Header("Location", url));

            return respone;
        }

        private string HttpResponseString()
        {
            var responseMessage = new StringBuilder();

            responseMessage.Append($"HTTP/1.1 {(int)StatusCode} {StatusCode}" + HttpConstants.NewLine);

            foreach (var header in Headers)
            {
                responseMessage.Append($"{header}" + HttpConstants.NewLine);
            }

            responseMessage.Append(HttpConstants.NewLine);

            return responseMessage.ToString();
        }
    }
}
