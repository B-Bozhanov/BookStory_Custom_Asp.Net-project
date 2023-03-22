namespace BookStory.App.Controllers
{
    using HttpServer.Http.HttpResponses;

    using MvcFramework;

    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse DoLogin()
        {
            return this.View("Hello from DoLogin");
        }

        public HttpResponse Register()
        {
            return this.View();
        }
    }
}
