namespace CameraBazaar.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserBm
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
