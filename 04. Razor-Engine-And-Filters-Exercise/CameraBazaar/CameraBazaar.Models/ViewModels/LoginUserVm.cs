namespace CameraBazaar.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserVm
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
