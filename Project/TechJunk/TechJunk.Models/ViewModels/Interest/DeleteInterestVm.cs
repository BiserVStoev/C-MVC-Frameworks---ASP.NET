namespace TechJunk.Models.ViewModels.Interest
{
    using System;
    using TechJunk.Models.Enums;

    public class DeleteInterestVm
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime PostDate { get; set; }
    }
}
