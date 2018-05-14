namespace TechJunk.Models.ViewModels.Sales
{
    using TechJunk.Models.Enums;

    public class SalesIndexVm
    {
        public PagedList.IPagedList<ShortSaleVm> Sales { get; set; }

        public Category Category { get; set; }
    }
}
