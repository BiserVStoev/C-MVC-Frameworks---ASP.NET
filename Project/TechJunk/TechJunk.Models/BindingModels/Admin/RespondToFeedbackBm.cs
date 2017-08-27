namespace TechJunk.Models.BindingModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class RespondToFeedbackBm
    {
        public string UserId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Topic { get; set; }

        [Required]
        [MinLength(40)]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
