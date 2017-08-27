namespace TechJunk.Web.Controllers
{
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using TechJunk.Models.BindingModels.Interests;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Services.Interfaces;

    [RoutePrefix(("interests"))]
    [Authorize(Roles = "JunkLover")]
    public class InterestsController : Controller
    {
        private IInterestsService service;

        public InterestsController(IInterestsService service)
        {
            this.service = service;
        }

        [Route]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult All(int? pageNumber, string category, string search)
        {
            IPagedList<InterestVm> vms = null;
            if ((category == null || category == string.Empty) && (search == null || search == string.Empty))
            {
                vms = this.service.GetAllInterests().ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                vms = this.service.GetAllInterestsByTitle(search).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null || search == string.Empty) && (category != null && category != string.Empty))
            {
                vms = this.service.GetAllInterestsByCategory(int.Parse(category)).ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                vms = this.service.GetAllInterestsByCategoryAndTitle(int.Parse(category), search).ToPagedList(pageNumber ?? 1, 10);
            }

            return this.View(vms);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add")]
        public ActionResult Add(AddInterestBm bm)
        {
            if (this.ModelState.IsValid)
            {
                HttpPostedFileBase file = this.Request.Files["url"];

                if (file == null || !file.ContentType.Contains("image"))
                {
                    this.ModelState.AddModelError("url", "Invalid image");
                }
                else
                {
                    var pathToFolder = this.Server.MapPath("~/SalePictures");
                    string fileName = Path.GetFileName(file.FileName);
                    string path = this.service.GetAdequatePathToSave(pathToFolder, fileName);
                    file.SaveAs(path);

                    var imageUrl = this.service.GetImageUrl(path);
                    bm.Url = imageUrl;

                    var userId = this.User.Identity.GetUserId();
                    this.service.AddInterest(bm, userId);

                    return this.RedirectToAction("All", "Interests");
                }
            }

            AddInterestVm vm = Mapper.Map<AddInterestBm, AddInterestVm>(bm);

            return this.View("Add", vm);
        }

        [HttpGet]
        [Route("mine")]
        public ActionResult Mine(int? pageNumber, string category, string search)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var salesVms = this.GetVmsForUser(currentUserId, pageNumber, category, search);

            return this.View(salesVms);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            string userId = this.User.Identity.GetUserId();

            if (!this.service.InterestExists(id))
            {
                return this.HttpNotFound("Interest not found.");
            }

            bool isValid = this.service.IsUserAuthenticatedToEditCurrent(id, userId);

            if (isValid)
            {
                EditInterestVm vm = this.service.GetEditVm(id);

                if (vm == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(vm);
            }

            return this.RedirectToAction("All");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public ActionResult EditPost(EditInterestBm bind)
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
                    var pathToFolder = this.Server.MapPath("~/SalePictures");
                    string fileName = Path.GetFileName(file.FileName);
                    string path = this.service.GetAdequatePathToSave(pathToFolder, fileName);
                    file.SaveAs(path);

                    var imageUrl = this.service.GetImageUrl(path);
                    bind.SalePicture = imageUrl;

                    string currentUsername = this.User.Identity.Name;
                    this.service.EditInterest(bind, currentUsername);

                    return this.RedirectToAction("Mine");
                }
            }

            EditInterestVm vm = this.service.GetEditVm(bind.Id);

            return this.View(vm);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            string userId = this.User.Identity.GetUserId();

            if (!this.service.InterestExists(id))
            {
                return this.HttpNotFound("Interest not found.");
            }

            bool isValid = this.service.IsUserAuthenticatedToEditCurrent(id, userId);

            if (isValid)
            {
                var vm = this.service.GetDeleteVm(id);
                return this.View(vm);
            }

            return this.RedirectToAction("All");
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult DeletePost(int id)
        {
            this.service.DeleteInterest(id);
            
            return this.RedirectToAction("Mine");
        }

        [HttpGet]
        [Route("user/{userId}")]
        public ActionResult UserInterests(string userId, int? pageNumber, string category, string search)
        {
            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User does not exist.");
            }

            this.ViewBag.UserEmail = this.service.GetUserEmail(userId);
            this.ViewBag.UserId = userId;
            IPagedList<InterestVm> vms = this.GetVmsForUser(userId, pageNumber, category, search);

            return this.View(vms);
        }

        private IPagedList<InterestVm> GetVmsForUser(string userId, int? pageNumber, string category, string search)
        {
            IPagedList<InterestVm> vms = this.service.GetAllInterests().ToPagedList(pageNumber ?? 1, 10);
            if ((category == null || category == string.Empty) && (category == null || search == string.Empty))
            {
                vms = this.service.GetAllInterests().ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                vms = this.service.GetAllInterestsByTitleForUser(search, userId).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null || search == string.Empty) && (category != null && category != string.Empty))
            {
                vms = this.service.GetAllInterestsByCategoryForUser(int.Parse(category), userId)
                    .ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                vms =
                    this.service.GetAllInterestsByCategoryAndTitleForUser(int.Parse(category), search, userId)
                        .ToPagedList(pageNumber ?? 1, 10);
            }
            return vms;
        }
    }
}