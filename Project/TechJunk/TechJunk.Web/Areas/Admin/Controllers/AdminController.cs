namespace TechJunk.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            this.ViewBag.IndexActive = "active";

            return this.View();
        }

        [HttpGet]
        [Route("Sales")]
        public ActionResult Sales()
        {
            this.ViewBag.SalesActive = "active";

            return this.View();
        }
    }
}