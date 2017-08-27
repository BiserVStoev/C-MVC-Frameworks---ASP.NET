namespace LearningSystem.Models.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserVm
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
