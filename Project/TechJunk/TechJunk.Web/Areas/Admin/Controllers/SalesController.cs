namespace TechJunk.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using PagedList;
    using TechJunk.Models.ViewModels.Sales;
    using TechJunk.Services.Interfaces;

    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Sales")]
    public class SalesController : Controller
    {
        private IAdminSalesService service;

        public SalesController(IAdminSalesService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("All")]
        public ActionResult AllSales(int? pageNumber, string category, string search)
        {
            IPagedList<ShortSaleVm> salesVms = null;
            if ((category == null || category == string.Empty) && (search == null || search == string.Empty))
            {
                salesVms = this.service.GetAllSales().ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((category == null || category == string.Empty) && (search != null && search != string.Empty))
            {
                salesVms = this.service.GetAllSalesByTitle(search).ToPagedList(pageNumber ?? 1, 10);
            }
            else if ((search == null || search == string.Empty) && (category != null && category != string.Empty))
            {
                salesVms = this.service.GetAllSalesByCategory(int.Parse(category)).ToPagedList(pageNumber ?? 1, 10);
            }
            else
            {
                salesVms = this.service.GetAllSalesByCategoryAndTitle(int.Parse(category), search).ToPagedList(pageNumber ?? 1, 10);
            }

            this.ViewBag.SalesActive = "active";

            return this.View(salesVms);
        }

        [HttpGet]
        [Route("Details/{id:int}")]
        public ActionResult Details(int id)
        {
            DetailedSaleVm vm = this.service.GetDetailedSale(id);
            if (vm == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.SalesActive = "active";

            return this.View(vm);
        }

        [HttpGet]
        [Route("Delete/{saleId}")]
        public ActionResult Delete(int saleId)
        {
            if (!this.service.SaleExists(saleId))
            {
                return this.HttpNotFound("Interest not found.");
            }

            this.ViewBag.SalesActive = "active";
            DeleteSaleVm vm = this.service.GetDeleteVm(saleId);
            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{saleId}")]
        public ActionResult DeletePost(int id)
        {
            this.service.DeleteSale(id);

            return this.RedirectToAction("AllSales");
        }
    }
}