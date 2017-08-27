namespace LearningSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using LearningSystem.Models.ViewModels.Courses;
    using LearningSystem.Services;

    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<CourseVm> vms = this.service.GetAllCourses();

            return this.View(vms);
        }
    }
}