namespace TechJunk.Models.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserVm
    {
        [Required]
        [Display(Name = "Image")]
        public string ProfilePicture { get; set; }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
