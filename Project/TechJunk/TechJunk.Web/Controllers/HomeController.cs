namespace TechJunk.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using TechJunk.Models.BindingModels.Feedback;
    using TechJunk.Models.ViewModels.Feedback;
    using TechJunk.Services.Interfaces;

    public class HomeController : Controller
    {
        private IHomeService service;

        public HomeController(IHomeService homeService)
        {
            this.service = homeService;
        }

        [OutputCache(Duration = 150400)]
        public ActionResult About()
        {
            return this.View();
        }

        [Authorize(Roles = "JunkLover")]
        public ActionResult Contact()
        {
            return this.View();
        }
        
        [HttpPost]
        [Authorize(Roles = "JunkLover")]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(FeedbackBm bm)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                this.service.AddFeedback(bm, userId);

                return this.RedirectToAction("All", "Sales");
            }

            var vm = Mapper.Map<FeedbackBm, FeedbackVm>(bm);

            return this.View(vm);
        }
    }
}