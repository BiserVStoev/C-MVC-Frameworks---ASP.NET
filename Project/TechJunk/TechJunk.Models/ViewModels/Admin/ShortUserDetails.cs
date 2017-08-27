namespace TechJunk.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class ShortUserDetails
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [Display(Name = "Sales posted")]
        public int NumberOfSales { get; set; }

        [Display(Name = "Interests posted")]
        public int NumberOfInterests { get; set; }
    }
}
