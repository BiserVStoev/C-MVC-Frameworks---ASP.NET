namespace LearningSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using AutoMapper;
    using LearningSystem.Models.BindingModels;
    using LearningSystem.Models.EntityModels;
    using LearningSystem.Models.ViewModels.Blog;

    public class BlogService : Service
    {
        public IEnumerable<ArticleVm> GetAllArticles()
        {
            IEnumerable<Articles> models = this.Context.Articles;

            IEnumerable<ArticleVm> vms = Mapper.Map<IEnumerable<Articles>, IEnumerable<ArticleVm>>(models);

            return vms;
        }

        public void AddArticle(AddArticleBm bm, string username)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.UserName == username);
            Articles article = Mapper.Map<AddArticleBm, Articles>(bm);
            article.Author = currentUser;
            article.PublishDate = DateTime.Today;
            this.Context.Articles.Add(article);
            this.Context.SaveChanges();
        }
    }
}
