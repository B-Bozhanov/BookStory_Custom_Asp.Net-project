namespace HttpServer.Http
{
    using System.Text;

    using HttpServer.Common;
    using HttpServer.Http.Enums;
    using HttpServer.Http.HttpCollections;

    public class HttpRequest
    {
        //TODO: queryString
        public HttpRequest()
        {
            this.Headers = new();
        }

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public HttpCollection Headers { get; init; }

        public string Body { get; private set; }

        public HttpRequest Parse(string requestString)
        {
            var lines = requestString.Split(HttpConstants.NewLine);

            var firstLine = lines[0];
            var firstLineParts = firstLine.Split(' ');
            var isValidMethod = Enum.TryParse(firstLineParts[0], true, out HttpMethod method);

            if (!isValidMethod)
            {
                throw new ArgumentException(HttpExceptions.UnsupportedMethod);

            }
            var path = firstLineParts[1];

            var lineCount = 1;
            var isHeader = true;
            var bodyBuilder = new StringBuilder();

            while (lineCount != lines.Length)
            {
                string line = lines[lineCount];

                if (string.IsNullOrEmpty(line))
                {
                    isHeader = false;
                    lineCount++;
                    continue;
                }

                if (isHeader)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }

                lineCount++;
            }

            var request = new HttpRequest
            {
                Method = method,
                Path = path,
                Headers = this.Headers,
                Body = bodyBuilder.ToString()
            };

            return request;
        }
    }
}
