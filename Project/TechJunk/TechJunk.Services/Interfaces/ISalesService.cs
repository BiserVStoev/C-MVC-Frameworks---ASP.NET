namespace TechJunk.Services.Interfaces
{
    using System.Collections.Generic;
    using TechJunk.Models.BindingModels.Sales;
    using TechJunk.Models.ViewModels.Sales;

    public interface ISalesService
    {
        IEnumerable<ShortSaleVm> GetAllSales();

        void AddSale(AddSaleBm bm, string userId);

        DetailedSaleVm GetDetailedSale(int? id);

        string GetImageUrl(string imagePath);

        string GetAdequatePathToSave(string path, string fileName);

        IEnumerable<ShortSaleVm> GetCurrentUsersSales(string currentUserId);

        EditSaleVm GetEditVm(int? saleId);

        void EditSale(EditSaleBm bind, string currentUsername);

        bool IsUserAuthenticatedToEditCurrent(int? id, string userId);

        bool SaleExists(int? id);

        DeleteSaleVm GetDeleteVm(int id);

        void DeleteSale(int id);

        bool UserExists(string userId);

        IEnumerable<ShortSaleVm> GetAllSalesForUser(string userId);

        string GetUserEmail(string userId);

        IEnumerable<ShortSaleVm> GetAllSalesByCategory(int category);

        IEnumerable<ShortSaleVm> GetAllSalesByTitle(string title);

        IEnumerable<ShortSaleVm> GetAllSalesByCategoryAndTitle(int category, string title);

        IEnumerable<ShortSaleVm> GetAllSalesByTitleForUser(string userId, string search);

        IEnumerable<ShortSaleVm> GetAllSalesByCategoryForUser(string userId, int category);

        IEnumerable<ShortSaleVm> GetAllSalesByCategoryAndTitleForUser(string userId, int category, string search);
    }
}