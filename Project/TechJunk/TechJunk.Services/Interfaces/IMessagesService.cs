namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using TechJunk.Models.BindingModels.Messages;
    using TechJunk.Models.ViewModels.Messages;

    public interface IMessagesService
    {
        IEnumerable<ShortMessagesVm> GetInboxMessages(string userId);

        IEnumerable<ShortMessagesVm> GetSentMessages(string userId);

        bool UserExists(string userId);

        void AddMessage(AddMessageBm bm, string currentUserId);

        bool MessageExists(int messageId);

        bool MessageExistsInUserData(int messageId);

        bool IsUserAuthenticatedToViewMessage(int messageId, string userId);

        DetailedMessageVm GetMessageDetails(int messageId);

        DeleteMessageVm GetDeleteVm(int messageId, string userId);

        void DeleteMessage(int messageId, string userId);
    }
}