namespace TechJunk.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using TechJunk.Models.BindingModels.Admin;
    using TechJunk.Models.ViewModels.Admin;
    using TechJunk.Services.Interfaces;

    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class FeedbackController : Controller
    {
        private IAdminFeedbackService service;

        public FeedbackController(IAdminFeedbackService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("feedbacks")]
        public ActionResult Index(int? pageNumber)
        {
            var vms = this.service.GetAllFeedbacks().ToPagedList(pageNumber ?? 1, 10);
            this.ViewBag.FeedbacksActive = "active";

            return this.View(vms);
        }

        [HttpGet]
        [Route("Feedback/Details/{feedbackId}")]
        public ActionResult Details(int feedbackId)
        {
            DetailedFeedbackVm vm = this.service.GetDetailedFeedback(feedbackId);
            if (vm == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.FeedbacksActive = "active";

            return this.View(vm);
        }

        [HttpGet]
        [Route("Feedback/Delete/{feedbackId}")]
        public ActionResult Delete(int feedbackId)
        {
            if (!this.service.FeedbackExists(feedbackId))
            {
                return this.HttpNotFound("Feedback not found.");
            }

            this.ViewBag.FeedbacksActive = "active";
            DeleteFeedbackVm vm = this.service.GetDeleteVm(feedbackId);

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Feedback/Delete/{feedbackId}")]
        public ActionResult DeletePost(int feedbackId)
        {
            this.service.DeleteFeedback(feedbackId);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Feedback/Respond")]
        public ActionResult Respond(RespondToFeedbackBm bm)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                this.service.AddResponse(bm, userId);
            }

            return this.RedirectToAction("Index");
        }
    }
}