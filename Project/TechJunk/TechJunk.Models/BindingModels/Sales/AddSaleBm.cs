namespace TechJunk.Models.BindingModels.Sales
{
    using TechJunk.Models.Enums;

    public class AddSaleBm
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public string Specification { get; set; }

        public decimal Price { get; set; }

        public string PhoneNumber { get; set; }
    }
}
