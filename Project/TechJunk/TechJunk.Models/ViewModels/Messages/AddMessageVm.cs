namespace TechJunk.Models.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    public class AddMessageVm
    {
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Topic { get; set; }

        [Required]
        [MinLength(30)]
        [MaxLength(1000)]
        public string Description { get; set; }

        public string RecieverId { get; set; }
    }
}
