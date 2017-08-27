namespace TechJunk.Web.Controllers
{
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using TechJunk.Models.BindingModels.Users;
    using TechJunk.Models.ViewModels.Users;
    using TechJunk.Services.Interfaces;

    [RoutePrefix("users")]
    [Authorize(Roles = "JunkLover")]
    public class UsersController : Controller
    {
        private IUsersService service;

        public UsersController(IUsersService usersService)
        {
            this.service = usersService;
        }

        [HttpGet]
        [Route("profile")]
        public ActionResult Profile()
        {
            string username = this.User.Identity.Name;
            ProfileVm vm = this.service.GetCurrentUserProfileVm(username);

            return this.View(vm);
        }

        [HttpGet]
        [Route("details/{userId}")]
        [OutputCache(Duration = 43400)]
        public ActionResult Details(string userId)
        {
            if (userId == null)
            {
                return this.HttpNotFound("Username cannot be empty.");
            }

            bool exists = this.service.UserExists(userId);
            if (exists)
            {
                ProfileVm vm = this.service.GetProfileVmForUser(userId);
                this.ViewBag.UserId = userId;
                return this.View("UserDetails", vm);
            }
            else
            {
                return this.HttpNotFound("User not found.");
            }
        }

        [HttpGet]
        [Route("edit")]
        public ActionResult Edit()
        {
            string userId = this.User.Identity.GetUserId();
            EditUserVm vm = this.service.GetEditVm(userId);

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public ActionResult Edit(EditUserBm bind)
        {
            if (this.ModelState.IsValid)
            {
                HttpPostedFileBase file = this.Request.Files["salePicture"];

                if (file == null || !file.ContentType.Contains("image"))
                {
                    this.ModelState.AddModelError("profilePicture", "Invalid image");
                }
                else
                {
                    var pathToFolder = this.Server.MapPath("~/ProfilePictures");
                    string fileName = Path.GetFileName(file.FileName);
                    string path = this.service.GetAdequatePathToSave(pathToFolder, fileName);
                    file.SaveAs(path);

                    var imageUrl = this.service.GetImageUrl(path);
                    bind.ProfilePictureUrl = imageUrl;

                    string currentUsername = this.User.Identity.Name;
                    this.service.EditUser(bind, currentUsername);

                    return this.RedirectToAction("Profile");
                }
            }

            string username = this.User.Identity.Name;
            EditUserVm vm = this.service.GetEditVm(username);

            return this.View(vm);
        }
    }
}