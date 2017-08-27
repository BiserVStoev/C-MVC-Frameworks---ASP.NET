namespace TechJunk.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using PagedList;
    using TechJunk.Models.BindingModels.Admin;
    using TechJunk.Models.ViewModels.Admin;
    using TechJunk.Services.Interfaces;

    [RouteArea("Admin")]
    [RoutePrefix("Users")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IAdminUsersService service;

        public UsersController(IAdminUsersService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route]
        public ActionResult Users(int? pageNumber, string search)
        {
            IPagedList<ShortUserDetails> vms = null;
            if (search == null || search == string.Empty)
            {
                vms = this.service.GetAllUsers().ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                vms = this.service.GetAllUsersByEmail(search).ToPagedList(pageNumber ?? 1, 10);
            }

            this.ViewBag.UsersActive = "active";

            return this.View(vms);
        }

        [HttpGet]
        [Route("{userId}")]
        [OutputCache(Duration = 86400)]
        public ActionResult UserDetails(string userId)
        {
            this.ViewBag.UsersActive = "active";

            DetailedUserVm vm = this.service.GetUserDetails(userId);
            this.ViewBag.Roles = this.service.GetRoles();

            return this.View(vm);
        }

        [HttpGet]
        [Route("Ban/{userId}")]
        public ActionResult Ban(string userId)
        {
            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User not Found");
            }

            ShortUserDetails vm = this.service.GetUserShortDetailById(userId);

            return this.View("BanConfirmation", vm);
        }

        [HttpPost]
        [Route("Ban/{userId}")]
        [ValidateAntiForgeryToken]
        public ActionResult BanPost(string userId)
        {
            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User not Found");
            }

            this.service.BanUser(userId);

            return this.RedirectToAction("Users");
        }

        [HttpGet]
        [Route("Unban/{userId}")]
        public ActionResult UnBan(string userId)
        {
            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User not Found");
            }

            ShortUserDetails vm = this.service.GetUserShortDetailById(userId);
            
            return this.View("UnBanConfirmation", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Unban/{userId}")]
        public ActionResult UnBanPost(string userId)
        {
            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User not Found");
            }

            ShortUserDetails vm = this.service.GetUserShortDetailById(userId);

            this.service.UnBanUser(userId);

            return this.RedirectToAction("Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Assign")]
        public ActionResult AssignRole(UsersRoleBm bm)
        {
            this.service.AssignRole(bm.Id, bm.Roles);

            return this.RedirectToAction("UserDetails", "Users", new {userId = bm.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Deassign")]
        public ActionResult DeassignRole(UsersRoleBm bm)
        {
            this.service.DeAssignRole(bm.Id, bm.Roles);

            return this.RedirectToAction("UserDetails", "Users", new { userId = bm.Id });
        }
    }
}