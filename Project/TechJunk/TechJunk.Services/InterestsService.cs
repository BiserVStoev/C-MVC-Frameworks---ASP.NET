namespace TechJunk.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using TechJunk.Models.BindingModels.Interests;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Services.Interfaces;

    public class InterestsService : Service, IInterestsService
    {
        public IEnumerable<InterestVm> GetAllInterests()
        {
            var interests = this.Context.Interests.OrderByDescending(i => i.PostDate).Where(i => i.User.IsBanned == false);

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }

        public string GetAdequatePathToSave(string path, string fileName)
        {
            var files = Directory.GetFiles(path);

            string finalPath = Path.Combine(path, fileName);

            foreach (var file in files)
            {
                if (file.Contains(fileName))
                {
                    var extension = Path.GetExtension(fileName);
                    var newFileName = fileName.Replace(extension, "(1)") + extension;
                    finalPath = Path.Combine(path, newFileName);

                    return this.GetAdequatePathToSave(path, newFileName);
                }
            }

            return finalPath;
        }

        public string GetImageUrl(string imagePath)
        {
            var fileName = Path.GetFileName(imagePath);
            var path = "/SalePictures/" + Path.GetFileName(imagePath);

            return path;
        }

        public void AddInterest(AddInterestBm bm, string userId)
        {
            ApplicationUser currentUser = this.Context.Users.Find(userId);
            Interest sale = Mapper.Map<AddInterestBm, Interest>(bm);
            sale.PostDate = DateTime.Now;
            currentUser.Interests.Add(sale);
            this.Context.SaveChanges();
        }

        public IEnumerable<InterestVm> GetCurrentUsersSales(string currentUserId)
        {
            var currentUser = this.Context.Users.Find(currentUserId);
            var interests = currentUser.Interests;
            IEnumerable<InterestVm> salesVms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests).OrderByDescending(s => s.PostDate);

            return salesVms;
        }

        public bool IsUserAuthenticatedToEditCurrent(int id, string userId)
        {
            var interest = this.Context.Interests.Find(id);
            if (interest.User.Id != userId)
            {
                return false;
            }

            return true;
        }

        public bool InterestExists(int id)
        {
            if (this.Context.Interests.Find(id) == null)
            {
                return false;
            }

            return true;
        }

        public EditInterestVm GetEditVm(int interestId)
        {
            Interest interest = this.Context.Interests.Find(interestId);

            if (interest == null)
            {
                return null;
            }

            EditInterestVm vm = Mapper.Map<Interest, EditInterestVm>(interest);

            return vm;
        }

        public void EditInterest(EditInterestBm bind, string currentUsername)
        {
            Interest interest = this.Context.Interests.Find(bind.Id);
            interest.Url = bind.SalePicture;
            interest.Category = bind.Category;
            interest.PhoneNumber = bind.PhoneNumber;
            interest.Title = bind.Title;
            var user = interest.User;
            this.Context.SaveChanges();
        }

        public DeleteInterestVm GetDeleteVm(int id)
        {
            var interest = this.Context.Interests.Find(id);

            DeleteInterestVm vm = Mapper.Map<Interest, DeleteInterestVm>(interest);

            return vm;
        }

        public void DeleteInterest(int id)
        {
            var interest = this.Context.Interests.Find(id);
            this.Context.Interests.Remove(interest);
            this.Context.SaveChanges();
        }

        public bool UserExists(string userId)
        {
            if (this.Context.Users.Find(userId) == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<InterestVm> GetAllSalesForUser(string userId)
        {
            IEnumerable<Interest> sales = this.Context.Interests.Where(s => s.User.Id == userId).OrderByDescending(sale => sale.PostDate);
            IEnumerable<InterestVm> salesVms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(sales);

            return salesVms;
        }

        public string GetUserEmail(string userId)
        {
            var email = this.Context.Users.Find(userId).Email;

            return email;
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

        public IEnumerable<InterestVm> GetAllInterestsByCategoryAndTitleForUser(int category, string search, string userId)
        {
            var interests = this.Context.Users.Find(userId).Interests.OrderByDescending(i => i.PostDate).Where(i => (int)i.Category == category && i.Title.ToLower().Contains(search.ToLower()));

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }

        public IEnumerable<InterestVm> GetAllInterestsByTitleForUser(string search, string userId)
        {
             var interests = this.Context.Users.Find(userId).Interests.OrderByDescending(i => i.PostDate).Where(i => i.Title.ToLower().Contains(search.ToLower()));

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }

        public IEnumerable<InterestVm> GetAllInterestsByCategoryForUser(int category, string userId)
        {
            var interests = this.Context.Users.Find(userId).Interests.OrderByDescending(i => i.PostDate).Where(i => (int)i.Category == category);

            IEnumerable<InterestVm> vms = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(interests);

            return vms;
        }
    }
}
