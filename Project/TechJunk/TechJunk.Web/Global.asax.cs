namespace TechJunk.Web
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using TechJunk.Models.BindingModels.Feedback;
    using TechJunk.Models.BindingModels.Interests;
    using TechJunk.Models.BindingModels.Messages;
    using TechJunk.Models.BindingModels.Sales;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Admin;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Models.ViewModels.Messages;
    using TechJunk.Models.ViewModels.Sales;
    using TechJunk.Models.ViewModels.Users;
    using FeedbackVm = TechJunk.Models.ViewModels.Feedback.FeedbackVm;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMappings()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Sale, ShortSaleVm>()
                .ForMember(vm => vm.Seller,
                    configurationExpression =>
                    configurationExpression.MapFrom(sale => sale.User.Email));
                expression.CreateMap<AddSaleBm, AddSaleVm>();
                expression.CreateMap<AddSaleBm, Sale>();
                expression.CreateMap<Sale, DetailedSaleVm>()
                .ForMember(vm => vm.UserId,
                    configurationExpression =>
                    configurationExpression.MapFrom(sale => sale.User.Id));
                expression.CreateMap<ApplicationUser, ProfileVm>();
                expression.CreateMap<ApplicationUser, EditUserVm>();
                expression.CreateMap<Sale, EditSaleVm>()
                .ForMember(vm => vm.UserId,
                configurationExpression =>
                configurationExpression.MapFrom(sale => sale.User.Id));
                expression.CreateMap<Interest, InterestVm>()
                .ForMember(vm => vm.UserId,
                    configurationExpression =>
                    configurationExpression.MapFrom(sale => sale.User.Id))
                .ForMember(vm => vm.Looker,
                    configurationExpression =>
                    configurationExpression.MapFrom(sale => sale.User.Email));
                expression.CreateMap<AddInterestBm, AddInterestVm>();
                expression.CreateMap<AddInterestBm, Interest>();
                expression.CreateMap<Interest, EditInterestVm>();
                expression.CreateMap<Interest, DeleteInterestVm>();
                expression.CreateMap<FeedbackBm, Feedback>();
                expression.CreateMap<FeedbackBm, FeedbackVm>();
                expression.CreateMap<Sale, DeleteSaleVm>();
                expression.CreateMap<Message, ShortMessagesVm>()
                .ForMember(vm => vm.RecieverId,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Reciever.Id))
                .ForMember(vm => vm.SenderId,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Sender.Id))
                .ForMember(vm => vm.SenderEmail,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Sender.Email));
                expression.CreateMap<AddMessageBm, Message>();
                expression.CreateMap<Message, DetailedMessageVm>()
                 .ForMember(vm => vm.RecieverId,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Reciever.Id))
                 .ForMember(vm => vm.SenderId,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Sender.Id))
                 .ForMember(vm => vm.RecieverEmail,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Reciever.Email))
                 .ForMember(vm => vm.SenderEmail,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Sender.Email));
                expression.CreateMap<Message, DeleteMessageVm>()
                 .ForMember(vm => vm.RecieverEmail,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Reciever.Email))
                 .ForMember(vm => vm.SenderEmail,
                    configurationExpression =>
                    configurationExpression.MapFrom(msg => msg.Sender.Email));
                expression.CreateMap<ApplicationUser, ShortUserDetails>()
                    .ForMember(vm => vm.NumberOfInterests,
                    configurationExpression => 
                    configurationExpression.MapFrom(user => user.Interests.Count))
                    .ForMember(vm => vm.NumberOfSales,
                    configurationExpression =>
                    configurationExpression.MapFrom(user => user.Sales.Count));
                expression.CreateMap<ApplicationUser, DetailedUserVm>();
                expression.CreateMap<Feedback, ShortFeedbackVm>()
                    .ForMember(vm => vm.Content,
                    configurationExpression =>
                    configurationExpression.MapFrom(feedback => string.Join(string.Empty, feedback.Content.Take(40).Concat("..."))));
                expression.CreateMap<Feedback, DetailedFeedbackVm>()
                    .ForMember(vm => vm.UserId,
                    configurationExpression =>
                    configurationExpression.MapFrom(feedback => feedback.User.Id))
                    .ForMember(vm => vm.UserEmail,
                    configurationExpression =>
                    configurationExpression.MapFrom(feedback => feedback.User.Email));
                expression.CreateMap<Feedback, DeleteFeedbackVm>();
            });
        }
    }
}
