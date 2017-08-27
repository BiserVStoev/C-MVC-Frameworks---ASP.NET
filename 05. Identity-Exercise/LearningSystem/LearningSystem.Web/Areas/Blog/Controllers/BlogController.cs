namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using LearningSystem.Models.BindingModels;
    using LearningSystem.Models.ViewModels.Blog;
    using LearningSystem.Services;

    [RouteArea("blog")]
    [Authorize(Roles = "Student")]
    public class BlogController : Controller
    {
        private BlogService service;

        public BlogController()
        {
            this.service = new BlogService();
        }

        [HttpGet]
        [Route("Articles")]
        public ActionResult Articles()
        {
            IEnumerable<ArticleVm> vms = this.service.GetAllArticles();

            return this.View(vms);
        }

        [HttpGet]
        [Authorize(Roles = "BlogAuthor")]
        [Route("articles/add")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "BlogAuthor")]
        [Route("articles/add")]
        public ActionResult Add(AddArticleBm bm)
        {
            if (this.ModelState.IsValid)
            {
                string username = this.User.Identity.Name;
                this.service.AddArticle(bm, username);

                this.RedirectToAction("Articles");
            }

            return this.View();
        }
    }
}