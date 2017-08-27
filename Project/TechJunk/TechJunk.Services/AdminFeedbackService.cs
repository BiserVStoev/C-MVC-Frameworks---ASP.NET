namespace TechJunk.Services
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using TechJunk.Models.BindingModels.Admin;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Admin;
    using TechJunk.Services.Interfaces;

    public class AdminFeedbackService : Service, IAdminFeedbackService
    {
        public IEnumerable<ShortFeedbackVm> GetAllFeedbacks()
        {
            var feedbacks = this.Context.Feedbacks;
            IEnumerable<ShortFeedbackVm> vms = Mapper.Map<IEnumerable<Feedback>, IEnumerable<ShortFeedbackVm>>(feedbacks);
            
            return vms;
        }

        public DetailedFeedbackVm GetDetailedFeedback(int feedbackId)
        {
            Feedback feedback = this.Context.Feedbacks.Find(feedbackId);
            if (feedback == null)
            {
                return null;
            }

            DetailedFeedbackVm vm = Mapper.Map<Feedback, DetailedFeedbackVm>(feedback);

            return vm;
        }

        public bool FeedbackExists(int feedbackId)
        {
            Feedback feedback = this.Context.Feedbacks.Find(feedbackId);

            if (feedback == null)
            {
                return false;
            }

            return true;
        }

        public DeleteFeedbackVm GetDeleteVm(int saleId)
        {
            Feedback sale = this.Context.Feedbacks.Find(saleId);
            var vm = Mapper.Map<Feedback, DeleteFeedbackVm>(sale);

            return vm;
        }

        public void DeleteFeedback(int feedbackId)
        {
            Feedback feedback = this.Context.Feedbacks.Find(feedbackId);
            this.Context.Feedbacks.Remove(feedback);
            this.Context.SaveChanges();
        }

        public void AddResponse(RespondToFeedbackBm bm, string userId)
        {
            Message message = new Message();
            message.DateMade = DateTime.Now;
            message.Description = "Feedback response:" + bm.Content;
            message.Topic = bm.Topic;
            ApplicationUser sender = this.Context.Users.Find(userId);
            ApplicationUser reciever = this.Context.Users.Find(bm.UserId);
            message.Reciever = reciever;
            message.Sender = sender;
            message.DateMade = DateTime.Now;

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();
        }
    }
}
