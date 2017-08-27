namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using LearningSystem.Models.BindingModels;
    using LearningSystem.Models.EntityModels;
    using LearningSystem.Models.ViewModels.Users;
    using LearningSystem.Services;

    [Authorize(Roles = "Student")]
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }

        [HttpPost]
        [Route("enroll")]
        public ActionResult Enroll(int courseId)
        {
            string username = this.User.Identity.Name;
            Student student = this.service.GetCurrentStudent(username);
            this.service.EnrollStudentInCourse(courseId, student.Id);

            return this.RedirectToAction("Profile");
        }

        [Route("profile")]
        public ActionResult Profile()
        {
            string username = this.User.Identity.Name;
            ProfileVm vm = this.service.GetProfileVm(username);

            return this.View(vm);
        }

        [HttpGet]
        [Route("edit")]
        public ActionResult Edit()
        {
            string username = this.User.Identity.Name;
            EditUserVm vm = this.service.GetEditVm(username);

            return this.View(vm);
        }

        [HttpPost]
        [Route("edit")]
        public ActionResult Edit(EditUserBm bind)
        {
            if (this.ModelState.IsValid)
            {
                string currentUsername = this.User.Identity.Name;
                this.service.EditUser(bind, currentUsername);
                return this.RedirectToAction("Profile");
            }

            string username = this.User.Identity.Name;
            EditUserVm vm = this.service.GetEditVm(username);

            return this.View(vm);
        }
    }
}