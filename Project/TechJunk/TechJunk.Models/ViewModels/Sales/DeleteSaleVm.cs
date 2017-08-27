namespace TechJunk.Models.ViewModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TechJunk.Models.Enums;

    public class DeleteSaleVm
    {
        public int Id { get; set; }

        public string Url { get; set; }

        [Display(Name = "Image")]
        public string Title { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Date posted")]
        public DateTime PostDate { get; set; }
    }
}
