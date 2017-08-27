namespace TechJunk.Services.Interfaces
{
    using TechJunk.Models.BindingModels.Feedback;

    public interface IHomeService
    {
        void AddFeedback(FeedbackBm bm, string userId);
    }
}