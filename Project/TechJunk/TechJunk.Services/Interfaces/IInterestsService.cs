namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using TechJunk.Models.BindingModels.Interests;
    using TechJunk.Models.ViewModels.Interest;

    public interface IInterestsService
    {
        IEnumerable<InterestVm> GetAllInterests();

        string GetAdequatePathToSave(string path, string fileName);

        string GetImageUrl(string imagePath);

        void AddInterest(AddInterestBm bm, string userId);

        IEnumerable<InterestVm> GetCurrentUsersSales(string currentUserId);

        bool IsUserAuthenticatedToEditCurrent(int id, string userId);

        bool InterestExists(int id);

        EditInterestVm GetEditVm(int interestId);

        void EditInterest(EditInterestBm bind, string currentUsername);

        DeleteInterestVm GetDeleteVm(int id);

        void DeleteInterest(int id);

        bool UserExists(string userId);

        IEnumerable<InterestVm> GetAllSalesForUser(string userId);

        string GetUserEmail(string userId);

        IEnumerable<InterestVm> GetAllInterestsByTitle(string search);

        IEnumerable<InterestVm> GetAllInterestsByCategory(int category);

        IEnumerable<InterestVm> GetAllInterestsByCategoryAndTitle(int category, string search);

        IEnumerable<InterestVm> GetAllInterestsByCategoryAndTitleForUser(int category, string search, string userId);

        IEnumerable<InterestVm> GetAllInterestsByTitleForUser(string search, string userId);

        IEnumerable<InterestVm> GetAllInterestsByCategoryForUser(int category, string userId);
    }
}