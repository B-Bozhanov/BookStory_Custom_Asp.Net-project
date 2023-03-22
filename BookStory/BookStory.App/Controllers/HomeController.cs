namespace BookStory.App.Controllers
{
    using HttpServer.Http.HttpResponses;

    using MvcFramework;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            return this.View();
        }

        public HttpResponse Search()
        {
            return this.View();
        }

        public HttpResponse MyProfile()
        {
            return this.Redirect("/Users/Login");
        }

        public HttpResponse About()
        {
            return this.View();
        }

        public HttpResponse Help()
        {
            return this.View();
        }

        public HttpResponse Contacts()
        {
            return this.View();
        }
    }
}
