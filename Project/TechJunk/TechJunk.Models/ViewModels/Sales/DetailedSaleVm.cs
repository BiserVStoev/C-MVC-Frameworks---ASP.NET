namespace TechJunk.Models.ViewModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TechJunk.Models.Enums;

    public class DetailedSaleVm
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }
        
        public string Specification { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Seller Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime PostDate { get; set; }

        public string UserId { get; set; }
    }
}