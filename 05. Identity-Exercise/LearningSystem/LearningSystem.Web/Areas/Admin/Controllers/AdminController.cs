namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using LearningSystem.Models.ViewModels.Admin;
    using LearningSystem.Services;

    [Authorize(Roles = "Admin")]
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        private AdminService service;

        public AdminController()
        {
            this.service = new AdminService();
        }

        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            AdminPageVm vm = this.service.GetAdminPage();

            return this.View(vm);
        }

        [HttpGet]
        [Route("course/add")]
        public ActionResult AddCourse()
        {
            return this.View();
        }

        [HttpPost]
        [Route("course/add")]
        public ActionResult AddCourses()
        {
            return this.View();
        }

        [HttpGet]
        [Route("course/{id}/edit")]
        public ActionResult EditCourse(int id)
        {
            return this.View();
        }

        [HttpGet]
        [Route("users/{id}/edit")]
        public ActionResult EditUser(int id)
        {
            return this.View();
        }
    }
}