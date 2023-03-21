namespace MvcFramework
{
    using System.Runtime.CompilerServices;
    using System.Text;

    using HttpServer.Http.HttpResponses;

    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName] string fileName = null!)
        {
            var views = "Views/";

            var controllerName = this.GetType().Name.Replace("Controller", string.Empty) + '/';

            var path = views + controllerName;

            // This will not catch file with the same names and differents extencions.
            var filePath = Directory.GetFiles(path)
               .Where(f => new FileInfo(f).Name == fileName + new FileInfo(f).Extension)
               .FirstOrDefault();

            if (filePath == null)
            {
                throw new ArgumentException("File path does not exist!");
            }

           // var layout = System.IO.File.ReadAllText("Views/layout.cshtml");

            var result = System.IO.File.ReadAllText(filePath);

            //layout = layout.Replace("{{RenderBody}}", result);

            var viewBytes = Encoding.UTF8.GetBytes(result);

            return new HttpResponse("text/html", viewBytes);
        }

        public static HttpResponse File(string contenType, [CallerMemberName] string fileName = null!) 
        {
            var rootPath = "wwwroot/";
            var test = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories);
            var filePath = Directory.GetFiles(rootPath)
                .Where(f => new FileInfo(f).Name == fileName.ToLower() + new FileInfo(f).Extension)
                .FirstOrDefault();

            if (filePath == null)
            {
                throw new ArgumentException("File does not exist!");
            }

            var result = System.IO.File.ReadAllBytes(filePath);

            return new HttpResponse(contenType, result);
        }

        public HttpResponse Redirect(string url)
        {
            return HttpResponse.Redirect(url);
        }
    }
}
