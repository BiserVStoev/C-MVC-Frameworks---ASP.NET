namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using TechJunk.Models.BindingModels.Admin;
    using TechJunk.Models.ViewModels.Admin;

    public interface IAdminFeedbackService
    {
        IEnumerable<ShortFeedbackVm> GetAllFeedbacks();

        DetailedFeedbackVm GetDetailedFeedback(int feedbackId);

        bool FeedbackExists(int feedbackId);

        DeleteFeedbackVm GetDeleteVm(int saleId);

        void DeleteFeedback(int feedbackId);

        void AddResponse(RespondToFeedbackBm bm, string userId);
    }
}