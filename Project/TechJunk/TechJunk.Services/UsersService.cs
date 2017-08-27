namespace TechJunk.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using TechJunk.Models.BindingModels.Users;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Models.ViewModels.Sales;
    using TechJunk.Models.ViewModels.Users;
    using TechJunk.Services.Interfaces;

    public class UsersService : Service, IUsersService
    {
        public ProfileVm GetCurrentUserProfileVm(string username)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.UserName == username);
            ProfileVm vm = Mapper.Map<ApplicationUser, ProfileVm>(currentUser);
            vm.Sales = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(currentUser.Sales).OrderByDescending(s => s.PostDate).Take(5);
            vm.Interests = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(currentUser.Interests).OrderByDescending(i => i.PostDate).Take(5);

            return vm;
        }

        public ProfileVm GetUserVmById(string userId)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.Id == userId);
            ProfileVm vm = Mapper.Map<ApplicationUser, ProfileVm>(currentUser);
            vm.Sales = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(currentUser.Sales).OrderByDescending(s => s.PostDate);

            return vm;
        }

        public bool UserExists(string userName)
        {
            var user = this.Context.Users.FirstOrDefault(u => u.Id == userName);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public EditUserVm GetEditVm(string userId)
        {
            ApplicationUser user =
                this.Context.Users.FirstOrDefault(applicationUser => applicationUser.Id == userId);
            EditUserVm vm = Mapper.Map<ApplicationUser, EditUserVm>(user);
            return vm;
        }

        public void EditUser(EditUserBm bind, string currentUsername)
        {
            ApplicationUser user =
                 this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == currentUsername);
            user.Name = bind.Name;
            user.Email = bind.Email;
            user.Address = bind.Address;
            user.PhoneNumber = bind.PhoneNumber;
            user.ProfilePictureUrl = bind.ProfilePictureUrl;
            this.Context.SaveChanges();
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
            var path = "/ProfilePictures/" + Path.GetFileName(imagePath);

            return path;
        }

        public ProfileVm GetProfileVmForUser(string userId)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.Id == userId);
            ProfileVm vm = Mapper.Map<ApplicationUser, ProfileVm>(currentUser);
            vm.Sales = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(currentUser.Sales).OrderByDescending(s => s.PostDate).Take(5);
            vm.Interests = Mapper.Map<IEnumerable<Interest>, IEnumerable<InterestVm>>(currentUser.Interests).OrderByDescending(i => i.PostDate).Take(5);

            return vm;
        }
    }
}
