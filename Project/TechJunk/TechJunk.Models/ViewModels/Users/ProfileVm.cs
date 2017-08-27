namespace TechJunk.Models.ViewModels.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TechJunk.Models.ViewModels.Interest;
    using TechJunk.Models.ViewModels.Sales;

    public class ProfileVm
    {
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string ProfilePictureUrl { get; set; }

        public IEnumerable<ShortSaleVm> Sales { get; set; }

        public IEnumerable<InterestVm> Interests { get; set; }
    }
}
