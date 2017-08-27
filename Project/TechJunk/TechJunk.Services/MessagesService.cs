namespace TechJunk.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using TechJunk.Models.BindingModels.Messages;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Messages;
    using TechJunk.Services.Interfaces;

    public class MessagesService : Service, IMessagesService
    {
        public IEnumerable<ShortMessagesVm> GetInboxMessages(string userId)
        {
            var messages = this.Context.Users.Find(userId)
                .SentMessages
                .OrderByDescending(m => m.DateMade)
                .Where(m => m.HasRecieverDeleted == false);
            IEnumerable<ShortMessagesVm> vms = Mapper.Map<IEnumerable<Message>, IEnumerable<ShortMessagesVm>>(messages);

            return vms;
        }

        public IEnumerable<ShortMessagesVm> GetSentMessages(string userId)
        {
            var messages = this.Context.Users.Find(userId)
                .RecievedMessages
                .OrderByDescending(m => m.DateMade)
                .Where(m => m.HasSenderDeleted == false);
            IEnumerable<ShortMessagesVm> vms = Mapper.Map<IEnumerable<Message>, IEnumerable<ShortMessagesVm>>(messages);

            return vms;
        }

        public bool UserExists(string userId)
        {
            var user = this.Context.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public void AddMessage(AddMessageBm bm, string currentUserId)
        {
            Message message = Mapper.Map<AddMessageBm, Message>(bm);
            ApplicationUser sender = this.Context.Users.Find(currentUserId);
            ApplicationUser reciever = this.Context.Users.Find(bm.RecieverId);
            message.Reciever = reciever;
            message.Sender = sender;
            message.DateMade = DateTime.Now;

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();
        }

        public bool MessageExists(int messageId)
        {
            var message = this.Context.Messages.Find(messageId);

            if (message == null)
            {
                return false;
            }

            return true;
        }

        public bool MessageExistsInUserData(int messageId)
        {
            Message message = this.Context.Messages.Find(messageId);
            if (message.HasRecieverDeleted && message.HasSenderDeleted)
            {
                return false;
            }

            return true;
        }

        public bool IsUserAuthenticatedToViewMessage(int messageId, string userId)
        {
            Message message = this.Context.Messages.Find(messageId);
            if (message.Reciever.Id != userId && message.Sender.Id != userId)
            {
                return false;
            }

            return true;
        }

        public DetailedMessageVm GetMessageDetails(int messageId)
        {
            Message message = this.Context.Messages.Find(messageId);

            DetailedMessageVm vm = Mapper.Map<Message, DetailedMessageVm>(message);

            return vm;
        }

        public DeleteMessageVm GetDeleteVm(int messageId, string userId)
        {
            Message message = this.Context.Messages.Find(messageId);
            var vm = Mapper.Map<Message, DeleteMessageVm>(message);
            vm.UserId = userId;

            return vm;
        }

        public void DeleteMessage(int messageId, string userId)
        {
            Message message = this.Context.Messages.Find(messageId);
            if (message.Reciever.Id == userId && message.Sender.Id == userId)
            {
                message.HasRecieverDeleted = true;
                message.HasSenderDeleted = true;
            }
            else if (message.Reciever.Id == userId)
            {
                message.HasRecieverDeleted = true;
            }
            else if (message.Sender.Id == userId)
            {
                message.HasSenderDeleted = true;
            }

            this.Context.SaveChanges();
        }
    }
}