namespace TechJunk.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TechJunk.Data;
    using TechJunk.Data.Interfaces;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Admin;
    using TechJunk.Services.Interfaces;

    public class AdminUsersService : Service, IAdminUsersService
    {
        public AdminUsersService(ITechJunkDbContext context) : base(context)
        {
        }

        public IEnumerable<ShortUserDetails> GetAllUsers()
        {
            IEnumerable<ApplicationUser> users = this.Context.Users.OrderBy(u => u.Email);
            IEnumerable<ShortUserDetails> vms = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ShortUserDetails>>(users);

            return vms;
        }

        public DetailedUserVm GetUserDetails(string userId)
        {
            ApplicationUser user = this.Context.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            DetailedUserVm vm = Mapper.Map<ApplicationUser, DetailedUserVm>(user);
            var allRolesIds = user.Roles.Select(r => r.RoleId);
            List<string> roleNames = new List<string>();

            foreach (var roleId in allRolesIds)
            {
                roleNames.Add(this.Context.Roles.Find(roleId).Name);
            }

            vm.Roles = string.Join(", ", roleNames);

            return vm;
        }

        public bool UserExists(string userId)
        {
            if (this.Context.Users.Find(userId) == null)
            {
                return false;
            }

            return true;
        }

        public ShortUserDetails GetUserShortDetailById(string userId)
        {
            var user = this.Context.Users.Find(userId);

            ShortUserDetails vm = Mapper.Map<ApplicationUser, ShortUserDetails>(user);

            return vm;
        }

        public void BanUser(string userId)
        {
            ApplicationUser user = this.Context.Users.Find(userId);
            user.IsBanned = true;
            this.Context.SaveChanges();
        }

        public void UnBanUser(string userId)
        {
            ApplicationUser user = this.Context.Users.Find(userId);
            user.IsBanned = false;
            this.Context.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetRoles()
        {
            var roles = this.Context.Roles.ToList().Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text = r.Name
            });

            return roles;
        }

        public void AssignRole(string userId, string role)
        {
            //CASTING HERE SMELLS, I KNOW :(
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.Context as TechJunkContext));
            var user = userManager.FindById(userId);
            userManager.AddToRole(userId, role);
        }

        public void DeAssignRole(string userId, string role)
        {
            //CASTING HERE SMELLS, I KNOW :(
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.Context as TechJunkContext));
            var user = userManager.FindById(userId);
            userManager.RemoveFromRole(userId, role);
        }

        public IEnumerable<ShortUserDetails> GetAllUsersByEmail(string search)
        {
            var users = this.Context.Users.Where(u => u.Email.ToLower().Contains(search.ToLower()));

            IEnumerable<ShortUserDetails> vms = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ShortUserDetails>>(users);

            return vms;
        }
    }
}
