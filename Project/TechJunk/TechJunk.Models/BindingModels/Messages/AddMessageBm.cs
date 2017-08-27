namespace TechJunk.Models.BindingModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    public class AddMessageBm
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
