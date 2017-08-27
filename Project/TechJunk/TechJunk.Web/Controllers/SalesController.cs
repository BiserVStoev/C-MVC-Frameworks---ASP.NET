namespace TechJunk.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using TechJunk.Models.BindingModels.Sales;
    using TechJunk.Models.ViewModels.Sales;
    using TechJunk.Services.Interfaces;

    [RoutePrefix("sales")]
    [Authorize(Roles = "JunkLover")]
    public class SalesController : Controller
    {
        private ISalesService service;

        public SalesController(ISalesService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [Route("all")]
        [HttpGet]
        public ActionResult All(int? pageNumber, string category, string search)
        {
            IPagedList<ShortSaleVm> salesVms = null;
            if ((category == null || category == string.Empty) && (search == null ||search == string.Empty))
            {
                salesVms = this.service.GetAllSales().ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                salesVms = this.service.GetAllSalesByTitle(search).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null ||search == string.Empty) && (category != null && category != string.Empty))
            {
                salesVms = this.service.GetAllSalesByCategory(int.Parse(category)).ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                salesVms = this.service.GetAllSalesByCategoryAndTitle(int.Parse(category), search).ToPagedList(pageNumber ?? 1, 10);
            }

            return this.View(salesVms);
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
        public ActionResult Add(AddSaleBm bm)
        {
            if (this.ModelState.IsValid)
            {
                HttpPostedFileBase file = this.Request.Files["salePicture"];

                if (file == null || !file.ContentType.Contains("image"))
                {
                    this.ModelState.AddModelError("salePicture", "Invalid image");
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
                    this.service.AddSale(bm, userId);
                   
                    return this.RedirectToAction("All");
                }
            }

            AddSaleVm vm = Mapper.Map<AddSaleBm, AddSaleVm>(bm);

            return this.View("Add", vm);
        }

        [HttpGet]
        [Route("details/{id:int}")]
        public ActionResult Details(int id)
        {
            DetailedSaleVm vm = this.service.GetDetailedSale(id);
            if (vm == null)
            {
                return this.HttpNotFound();
            }

            return this.View(vm);
        }

        [HttpGet]
        [Route("mine")]
        public ActionResult Mine(int? pageNumber, string category, string search)
        {
            var currentUserId = this.User.Identity.GetUserId();

            IPagedList<ShortSaleVm> salesVms = null;
            var userId = this.User.Identity.GetUserId();
            if ((category == null || category == string.Empty) && (category == null || search == string.Empty))
            {
                salesVms = this.service.GetAllSalesForUser(userId).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                salesVms = this.service.GetAllSalesByTitleForUser(userId, search).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null || search == string.Empty) && (category != null && category != string.Empty))
            {
                salesVms = this.service.GetAllSalesByCategoryForUser(userId, int.Parse(category)).ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                salesVms = this.service.GetAllSalesByCategoryAndTitleForUser(userId, int.Parse(category), search).ToPagedList(pageNumber ?? 1, 10);
            }

            return this.View(salesVms);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (!this.service.SaleExists(id))
            {
                return this.HttpNotFound("Sale not found.");
            }

            string userId = this.User.Identity.GetUserId();
            bool isValid = this.service.IsUserAuthenticatedToEditCurrent(id, userId);

            if (isValid)
            {
                EditSaleVm vm = this.service.GetEditVm(id);

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
        public ActionResult EditPost(EditSaleBm bind)
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
                    this.service.EditSale(bind, currentUsername);

                    return this.RedirectToAction("Mine");
                }
            }

            EditSaleVm vm = this.service.GetEditVm(bind.Id);

            return this.View(vm);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            string userId = this.User.Identity.GetUserId();

            if (!this.service.SaleExists(id))
            {
                return this.HttpNotFound("Interest not found.");
            }

            bool isValid = this.service.IsUserAuthenticatedToEditCurrent(id, userId);

            if (isValid)
            {
                DeleteSaleVm vm = this.service.GetDeleteVm(id);
                return this.View(vm);
            }
            
            return this.RedirectToAction("All");
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult DeletePost(int id)
        {
            this.service.DeleteSale(id);

            return this.RedirectToAction("Mine");
        }

        [HttpGet]
        [Route("user/{userId}")]
        [OutputCache(Duration = 43400)]
        public ActionResult UserSales(string userId, int? pageNumber, string category, string search)
        {
            if (!this.service.UserExists(userId))
            {
                return this.HttpNotFound("User does not exist.");
            }

            this.ViewBag.UserEmail = this.service.GetUserEmail(userId);
            this.ViewBag.UserId = userId;
            IPagedList<ShortSaleVm> salesVms = null;
            if ((category == null || category == string.Empty) && (category == null || search == string.Empty))
            {
                salesVms = this.service.GetAllSalesForUser(userId).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                salesVms = this.service.GetAllSalesByTitleForUser(userId, search).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null || search == string.Empty) && (category != null && category != string.Empty))
            {
                salesVms = this.service.GetAllSalesByCategoryForUser(userId, int.Parse(category)).ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                salesVms = this.service.GetAllSalesByCategoryAndTitleForUser(userId, int.Parse(category), search).ToPagedList(pageNumber ?? 1, 10);
            }

            return this.View(salesVms);
        }
    }
}