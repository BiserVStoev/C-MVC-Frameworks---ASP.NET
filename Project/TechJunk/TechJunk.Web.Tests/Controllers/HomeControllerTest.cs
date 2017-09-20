namespace TechJunk.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity.Core.Mapping;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TechJunk.Data.Interfaces;
    using TechJunk.Models.BindingModels.Feedback;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Feedback;
    using TechJunk.Services;
    using TechJunk.Services.Interfaces;
    using TechJunk.Web.Controllers;
    using TechJunk.Web.Tests.Mocks;
    using TestStack.FluentMVCTesting;

    //Had Users, Sales and Messages Controller Tests as well, but 
    //deleted them by accident... Might make them again if i have time
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
            {
                expression.CreateMap<Feedback, FeedbackBm>();
                expression.CreateMap<FeedbackBm, Feedback>();
                expression.CreateMap<FeedbackBm, FeedbackVm>();
            });
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

            foreach (var feedback in this.feedbacks)
            {
                this.context.Feedbacks.Add(feedback);
            }

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, new string[] { "Admin" });

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            this.service = new HomeService(this.context);
            this.controller = new HomeController(this.service);
            this.controller.ControllerContext = controllerContext.Object;
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
        public void Feedback_ShouldAddAndRedirect()
        {
            var bm = new FeedbackBm()
            {
                Topic = "Za parite sttava duma",
                Content = "Dai mi pariteeeeee asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd"

            };
            
            this.controller
                .WithCallTo(c => c.Feedback(bm))
                .ShouldRedirectTo<SalesController>(c2 => c2.All(null, null, null));

            Assert.AreEqual(3, this.context.Feedbacks.Count());
        }

        [TestMethod]
        public void Feedback_ShouldNotAddAndReturnDefaultView()
        {
            var bm = new FeedbackBm()
            {
                Topic = "",
                Content = "asdasdasdasdasd"

            };

            this.controller.ModelState.AddModelError("topic", "Topic cannot be less than 5 characters long.");


            this.controller
                .WithCallTo(c => c.Feedback(bm))
                .ShouldRenderDefaultView();
            Assert.AreEqual(2, this.context.Feedbacks.Count());

            var bm2 = new FeedbackBm()
            {
                Topic = "asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd",
                Content = "asddd"

            };

            this.controller.ModelState.AddModelError("Content", "Content cannot be less than 40 characters long.");


            this.controller
                .WithCallTo(c => c.Feedback(bm2))
                .ShouldRenderDefaultView();
            Assert.AreEqual(2, this.context.Feedbacks.Count());
        }
    }
}
