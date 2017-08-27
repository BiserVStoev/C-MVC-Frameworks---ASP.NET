namespace TechJunk.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserBm
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
