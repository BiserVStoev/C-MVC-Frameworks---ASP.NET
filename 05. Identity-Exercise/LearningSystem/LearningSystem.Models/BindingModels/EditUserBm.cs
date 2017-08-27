namespace LearningSystem.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserBm
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
