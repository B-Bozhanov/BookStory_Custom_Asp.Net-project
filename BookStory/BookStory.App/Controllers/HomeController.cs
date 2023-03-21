namespace BookStory.App.Controllers
{
    using HttpServer.Http;
    using HttpServer.Http.HttpResponses;

    using MvcFramework;

    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Search(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse MyProfile(HttpRequest request)
        {
            return this.Redirect("/Users/Login");
        }

        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Help(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Contacts(HttpRequest request)
        {
            return this.View();
        }
    }
}
