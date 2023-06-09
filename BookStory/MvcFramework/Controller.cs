﻿namespace MvcFramework
{
    using System.Reflection.Metadata;
    using System.Runtime.CompilerServices;
    using System.Text;

    using HttpServer.Common;
    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;

    public abstract class Controller
    {
        public HttpRequest Request { get; set; }

        public HttpResponse View([CallerMemberName] string fileName = null!)
        {
            var (contentType, fileBytes) = this.GetPathBytes("Views/", fileName);

            return new HttpResponse(contentType, fileBytes);
        }

        public HttpResponse File(string contenType, [CallerMemberName] string fileName = null!)
        {
            var (contentType, fileBytes) = this.GetPathBytes("wwwroot/", fileName);

            return new HttpResponse(contenType, fileBytes);
        }

        public HttpResponse Redirect(string url)
        {
            return HttpResponse.Redirect(url);
        }

        private (string contentType, byte[] fileBytes) GetPathBytes(string folderName, string fileName)
        {
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty) + '/';

            var path = folderName + controllerName;
                      
            var filesPath = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            var layoutPath = string.Empty;
            var layoutText = string.Empty;

            if (folderName == "Views/")
            {
                layoutPath = folderName + "Layout.cshtml";
                layoutText = System.IO.File.ReadAllText(layoutPath);
            }


            byte[] fileBytes = null!;

            var contentType = string.Empty;

            foreach (var filePath in filesPath)
            {
                var file = new FileInfo(filePath);

                if (file.Name == fileName + file.Extension)
                {
                    var fileText = System.IO.File.ReadAllText(filePath);

                    if (!string.IsNullOrEmpty(layoutPath))
                    {
                        layoutText = layoutText.Replace("[[RenderBody]]", fileText);
                        fileBytes = Encoding.UTF8.GetBytes(layoutText);
                        break;
                    }
                    fileBytes = Encoding.UTF8.GetBytes(fileText);
                    contentType = HttpConstants.ContentType.GetContentType(file.Extension);
                    break;
                }
            }

            if (fileBytes == null)
            {
                throw new ArgumentException(HttpExceptions.FileNotExist);
            }

            return (contentType, fileBytes);
        }
    }
}
