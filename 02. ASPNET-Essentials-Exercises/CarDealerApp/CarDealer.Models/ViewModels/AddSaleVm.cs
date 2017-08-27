namespace CarDealer.Models.ViewModels
{
    using System.Collections.Generic;

    public class AddSaleVm
    {
        public IEnumerable<AddSaleCustomerVm> Customers { get; set; }

        public IEnumerable<AddSaleCarVm> Cars { get; set; }

        public IEnumerable<int> Discounts { get; set; }
    }
}
