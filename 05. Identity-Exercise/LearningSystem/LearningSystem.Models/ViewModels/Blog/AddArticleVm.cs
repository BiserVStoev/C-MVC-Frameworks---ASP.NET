namespace LearningSystem.Models.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;

    public class AddArticleVm
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
