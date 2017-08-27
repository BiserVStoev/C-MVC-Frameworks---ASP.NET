namespace TechJunk.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TechJunk.Models.Enums;

    public class Interest
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

        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$",
            ErrorMessage = "Please enter valid phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        public DateTime PostDate { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}
