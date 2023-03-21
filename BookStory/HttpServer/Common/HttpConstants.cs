namespace HttpServer.Common
{
    public static class HttpConstants
    {
        internal const string NewLine = "\r\n";

        public class ContentType
        {
            private readonly static Dictionary<string, string> contentTypes =
                new()
                {
                    {".txt", "text/plane" },
                    {".html", "text/html" },
                    {".js", "text/javascript" },
                    {".css", "text/css" },
                    {".ico", "image/vnd.microsoft.icon" },
                    {".jpg", "image/jpg" },
                    {".jpeg", "image/jpg" },
                    {".png", "image/png" },
                };

            public static string GetContentType(string fileExtension)
            {
                if (contentTypes.ContainsKey(fileExtension))
                {
                    return contentTypes[fileExtension];
                }

                return contentTypes[".txt"];
            }
        }
    }
}
