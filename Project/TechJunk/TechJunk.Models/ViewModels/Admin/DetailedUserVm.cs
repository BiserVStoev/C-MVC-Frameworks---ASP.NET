namespace TechJunk.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class DetailedUserVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool IsBanned { get; set; }

        public string Roles { get; set; }
    }
}
