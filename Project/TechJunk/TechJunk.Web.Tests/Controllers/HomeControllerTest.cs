namespace TechJunk.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechJunk.Data.Interfaces;
    using TechJunk.Models.BindingModels.Feedback;
    using TechJunk.Models.EntityModels;
    using TechJunk.Services.Interfaces;
    using TechJunk.Web.Controllers;
    using TechJunk.Web.Tests.Mocks;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private IHomeService service;
        private ITechJunkDbContext context;
        private IList<Feedback> feedbacks;


        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
                expression.CreateMap<Feedback, FeedbackBm>());
        }

        [TestInitialize]
        public void Init()
        {
            this.ConfigureMapper();
            this.feedbacks = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    Topic = "Cmoon",
                    Content = "VolgaVolgaVolgaVolgaVolgaVolgaVolga"
                },
                new Feedback()
                {
                    Id = 2,
                    Topic = "Cmoon",
                    Content = "VolgaVolgaVolgaVolgaVolgaVolgaVolga"
                }
            };

            this.context = new FakeTechJunkDbContext();
        }

        [TestMethod]
        public void About_ShouldPass()
        {
            this.controller.WithCallTo(c => c.About()).ShouldRenderDefaultView();
        }


        [TestMethod]
        public void Contact_ShouldPass()
        {
            this.controller.WithCallTo(c => c.Contact()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Feedback_ShouldAdd()
        {
            var bm = Mapper.Map<Feedback, FeedbackBm>(this.feedbacks[0]);
            var asd = this.controller.Feedback(bm);
            var asdd = "";
        }
    }
}
