namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using TechJunk.Models.ViewModels.Sales;

    public interface IAdminSalesService
    {
        IEnumerable<ShortSaleVm> GetAllSales();

        DetailedSaleVm GetDetailedSale(int id);

        bool SaleExists(int saleId);

        DeleteSaleVm GetDeleteVm(int saleId);

        void DeleteSale(int id);

        IEnumerable<ShortSaleVm> GetAllSalesByCategory(int category);

        IEnumerable<ShortSaleVm> GetAllSalesByTitle(string title);

        IEnumerable<ShortSaleVm> GetAllSalesByCategoryAndTitle(int category, string title);
    }
}