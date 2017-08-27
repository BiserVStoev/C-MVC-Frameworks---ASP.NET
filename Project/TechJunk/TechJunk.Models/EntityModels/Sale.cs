namespace TechJunk.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TechJunk.Models.Enums;

    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [MinLength(30, ErrorMessage = "The description must be at least 30 symbols")]
        [MaxLength(1000, ErrorMessage = "The description cannot be more than 1000 symbols")]
        public string Specification { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The price cannot be a negative number")]
        public decimal Price { get; set; }

        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$",
            ErrorMessage = "Please enter valid phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        public DateTime PostDate { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}
