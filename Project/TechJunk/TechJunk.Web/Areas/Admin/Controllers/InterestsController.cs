namespace TechJunk.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using PagedList;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Services.Interfaces;

    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Interests")]
    public class InterestsController : Controller
    {
        private IAdminInterestsService service;

        public InterestsController(IAdminInterestsService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("All")]
        public ActionResult AllInterests(int? pageNumber, string category, string search)
        {
            this.ViewBag.InterestsActive = "active";
            IPagedList<InterestVm> salesVms = null;
            if ((category == null || category == string.Empty) && (search == null || search == string.Empty))
            {
                salesVms = this.service.GetAllInterests().ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                salesVms = this.service.GetAllInterestsByTitle(search).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null || search == string.Empty) && (category != null && category != string.Empty))
            {
                salesVms = this.service.GetAllInterestsByCategory(int.Parse(category)).ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                salesVms = this.service.GetAllInterestsByCategoryAndTitle(int.Parse(category), search).ToPagedList(pageNumber ?? 1, 10);
            }

            return this.View(salesVms);
        }

        [HttpGet]
        [Route("Delete/{interestId}")]
        public ActionResult Delete(int interestId)
        {
            if (!this.service.InterestExists(interestId))
            {
                return this.HttpNotFound("Interest not found.");
            }

            this.ViewBag.InterestsActive = "active";
            var vm = this.service.GetDeleteVm(interestId);
            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{interestId}")]
        public ActionResult DeletePost(int interestId)
        {
            this.service.DeleteSale(interestId);

            return this.RedirectToAction("AllInterests");
        }
    }
}