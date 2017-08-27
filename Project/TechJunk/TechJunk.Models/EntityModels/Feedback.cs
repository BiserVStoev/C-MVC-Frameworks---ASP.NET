namespace TechJunk.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Topic { get; set; }

        [Required]
        [MinLength(40)]
        [MaxLength(1000)]
        public string Content { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
