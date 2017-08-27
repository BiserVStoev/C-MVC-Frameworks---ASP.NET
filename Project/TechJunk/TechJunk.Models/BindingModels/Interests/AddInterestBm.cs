namespace TechJunk.Models.BindingModels.Interests
{
    using TechJunk.Models.Enums;

    public class AddInterestBm
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public string PhoneNumber { get; set; }
    }
}
