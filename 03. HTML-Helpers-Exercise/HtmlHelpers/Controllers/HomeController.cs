namespace HtmlHelpers.Controllers
{
    using System.Web.Mvc;
    using HtmlHelpers.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var people = new Person[]
            {
                new Person() {Name = "John Doe", Age = 40, Email = "john@office.com", IsSubscribed = true},
                new Person() {Name = "John Doe Jr.", Email = "john@office.com"},
                new Person() {Name = "Mickey Mouse", Age = 20, IsSubscribed = true},
            };

            return this.View(people);
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            var people = new Human[]
           {
                new Human() {Name = "John Doe", Age = 40},
                new Human() {Name = "John Doe Jr."},
                new Human() {Name = "Mickey Mouse", Age = 20},
           };

            return this.View(people);
        }
    }
}