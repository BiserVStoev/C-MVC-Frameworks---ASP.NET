namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using LearningSystem.Models.ViewModels.Courses;
    using LearningSystem.Services;

    [Authorize(Roles = "Student")]
    [RoutePrefix("courses")]
    public class CoursesController : Controller
    {
        private CoursesService service;

        public CoursesController()
        {
            this.service = new CoursesService();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            DetailsCourseVm vm = this.service.GetDetails(id);

            if (vm == null)
            {
                return new HttpNotFoundResult();
            }

             return this.View(vm);
        }
    }
}