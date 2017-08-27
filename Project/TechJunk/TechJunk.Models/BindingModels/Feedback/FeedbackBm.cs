namespace TechJunk.Models.BindingModels.Feedback
{
    using System.ComponentModel.DataAnnotations;

    public class FeedbackBm
    {
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
