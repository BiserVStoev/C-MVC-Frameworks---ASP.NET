namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TechJunk.Models.ViewModels.Admin;

    public interface IAdminUsersService
    {
        IEnumerable<ShortUserDetails> GetAllUsers();

        DetailedUserVm GetUserDetails(string userId);

        bool UserExists(string userId);

        ShortUserDetails GetUserShortDetailById(string userId);

        void BanUser(string userId);

        void UnBanUser(string userId);

        IEnumerable<SelectListItem> GetRoles();

        void AssignRole(string userId, string role);

        void DeAssignRole(string userId, string role);

        IEnumerable<ShortUserDetails> GetAllUsersByEmail(string search);
    }
}