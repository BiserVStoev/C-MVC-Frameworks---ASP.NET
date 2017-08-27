namespace TechJunk.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class DetailedFeedbackVm
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Topic { get; set; }

        [Required]
        [MinLength(40)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Display(Name = "User")]
        public string UserEmail { get; set; }
    }
}
