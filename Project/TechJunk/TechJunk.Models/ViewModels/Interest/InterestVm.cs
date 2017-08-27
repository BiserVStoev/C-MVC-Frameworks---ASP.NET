namespace TechJunk.Models.ViewModels.Interest
{
    using System;
    using TechJunk.Models.Enums;

    public class InterestVm
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime PostDate { get; set; }

        public string UserId { get; set; }

        public string Looker { get; set; }
    }
}
