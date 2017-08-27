namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using TechJunk.Models.ViewModels.Interest;

    public interface IAdminInterestsService
    {
        IEnumerable<InterestVm> GetAllInterests();

        bool InterestExists(int interestId);

        DeleteInterestVm GetDeleteVm(int interestId);

        void DeleteSale(int interestId);

        IEnumerable<InterestVm> GetAllInterestsByTitle(string search);

        IEnumerable<InterestVm> GetAllInterestsByCategory(int category);

        IEnumerable<InterestVm> GetAllInterestsByCategoryAndTitle(int category, string search);
    }
}