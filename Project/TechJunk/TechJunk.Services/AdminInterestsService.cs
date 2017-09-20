namespace TechJunk.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using TechJunk.Data.Interfaces;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Services.Interfaces;

    public class AdminInterestsService : Service, IAdminInterestsService
    {
        public AdminInterestsService(ITechJunkDbContext context) : base(context)
        {
        }

        public IEnumerable<InterestVm> GetAllInterests()
        {
            var interests = this.Context.Interests.OrderByDescending(i => i.PostDate).Where(i => i.User.IsBanned == false);

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }

        public bool InterestExists(int interestId)
        {
            if (this.Context.Interests.Find(interestId) == null)
            {
                return false;
            }

            return true;
        }

        public DeleteInterestVm GetDeleteVm(int interestId)
        {
            var interest = this.Context.Interests.Find(interestId);

            DeleteInterestVm vm = Mapper.Map<Interest, DeleteInterestVm>(interest);

            return vm;
        }

        public void DeleteSale(int interestId)
        {
            var interest = this.Context.Interests.Find(interestId);
            this.Context.Interests.Remove(interest);
            this.Context.SaveChanges();
        }

        public IEnumerable<InterestVm> GetAllInterestsByTitle(string search)
        {
            var interests = this.Context.Interests.OrderByDescending(i => i.PostDate).Where(i => i.User.IsBanned == false && i.Title.ToLower().Contains(search.ToLower()));

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }

        public IEnumerable<InterestVm> GetAllInterestsByCategory(int category)
        {
            var interests = this.Context.Interests.OrderByDescending(i => i.PostDate).Where(i => i.User.IsBanned == false && (int)i.Category == category);

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }

        public IEnumerable<InterestVm> GetAllInterestsByCategoryAndTitle(int category, string search)
        {
            var interests = this.Context.Interests.OrderByDescending(i => i.PostDate).Where(i => i.User.IsBanned == false && (int)i.Category == category && i.Title.ToLower().Contains(search.ToLower()));

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }
    }
}
