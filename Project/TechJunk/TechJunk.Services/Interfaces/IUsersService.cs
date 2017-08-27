namespace TechJunk.Services.Interfaces
{
    using TechJunk.Models.BindingModels.Users;
    using TechJunk.Models.ViewModels.Users;

    public interface IUsersService
    {
        ProfileVm GetCurrentUserProfileVm(string username);

        ProfileVm GetUserVmById(string userId);

        bool UserExists(string userName);

        EditUserVm GetEditVm(string userId);

        void EditUser(EditUserBm bind, string currentUsername);

        string GetAdequatePathToSave(string path, string fileName);

        string GetImageUrl(string imagePath);

        ProfileVm GetProfileVmForUser(string userId);
    }
}