namespace TechJunk.Services
{
    using AutoMapper;
    using TechJunk.Models.BindingModels.Feedback;
    using TechJunk.Models.EntityModels;
    using TechJunk.Services.Interfaces;

    public class HomeService : Service, IHomeService
    {
        public void AddFeedback(FeedbackBm bm, string userId)
        {
            var user = this.Context.Users.Find(userId);
            Feedback feedback = Mapper.Map<FeedbackBm, Feedback>(bm);
            feedback.User = user;
            this.Context.Feedbacks.Add(feedback);
            this.Context.SaveChanges();
        }
    }
}
