using HttpServer.Http;
using HttpServer.Http.HttpResponses;

using MvcFramework;

namespace BookStory.App.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }
    }
}
