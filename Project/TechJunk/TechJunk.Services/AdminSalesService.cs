namespace TechJunk.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using TechJunk.Data.Interfaces;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Sales;
    using TechJunk.Services.Interfaces;

    public class AdminSalesService : Service, IAdminSalesService
    {
        public AdminSalesService(ITechJunkDbContext context) : base(context)
        {
        }

        public IEnumerable<ShortSaleVm> GetAllSales()
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public DetailedSaleVm GetDetailedSale(int id)
        {
            Sale sale = this.Context.Sales.Find(id);
            if (sale == null)
            {
                return null;
            }

            DetailedSaleVm vm = Mapper.Map<Sale, DetailedSaleVm>(sale);

            return vm;
        }

        public bool SaleExists(int saleId)
        {
            if (this.Context.Sales.Find(saleId) == null)
            {
                return false;
            }

            return true;
        }

        public DeleteSaleVm GetDeleteVm(int saleId)
        {
            Sale sale = this.Context.Sales.Find(saleId);
            var vm = Mapper.Map<Sale, DeleteSaleVm>(sale);

            return vm;
        }

        public void DeleteSale(int id)
        {
            var sale = this.Context.Sales.Find(id);
            this.Context.Sales.Remove(sale);
            this.Context.SaveChanges();
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByCategory(int category)
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false && (int)s.Category == category);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByTitle(string title)
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false && s.Title.Contains(title));
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByCategoryAndTitle(int category, string title)
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false && s.Title.Contains(title) && (int)s.Category == category);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);
            
            return salesVms;
        }
    }
}
