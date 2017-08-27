namespace TechJunk.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using TechJunk.Models.BindingModels.Sales;
    using TechJunk.Models.EntityModels;
    using TechJunk.Models.ViewModels.Sales;
    using TechJunk.Services.Interfaces;

    public class SalesService : Service, ISalesService
    {
        public IEnumerable<ShortSaleVm> GetAllSales()
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public void AddSale(AddSaleBm bm, string userId)
        {
            ApplicationUser currentUser = this.Context.Users.Find(userId);
            Sale sale = Mapper.Map<AddSaleBm, Sale>(bm);
            sale.PostDate = DateTime.Now;
            currentUser.Sales.Add(sale);
            this.Context.SaveChanges();
        }

        public DetailedSaleVm GetDetailedSale(int? id)
        {
            Sale sale = this.Context.Sales.Find(id);
            if (sale == null)
            {
                return null;
            }

            DetailedSaleVm vm = Mapper.Map<Sale, DetailedSaleVm>(sale);

            return vm;
        }

        public string GetImageUrl(string imagePath)
        {
            var fileName = Path.GetFileName(imagePath);
            var path = "/SalePictures/" + Path.GetFileName(imagePath);

            return path;
        }

        public string GetAdequatePathToSave(string path, string fileName)
        {   
            var files = Directory.GetFiles(path);

            string finalPath = Path.Combine(path, fileName);

            foreach (var file in files)
            {
                if (file.Contains(fileName))
                {
                    var extension = Path.GetExtension(fileName);
                    var newFileName = fileName.Replace(extension, "(1)") + extension;
                    finalPath = Path.Combine(path, newFileName);

                    return this.GetAdequatePathToSave(path, newFileName);
                }
            }

            return finalPath;
        }

        public IEnumerable<ShortSaleVm> GetCurrentUsersSales(string currentUserId)
        {
            var currentUser = this.Context.Users.Find(currentUserId);
            var sales = currentUser.Sales;
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales).OrderByDescending(s => s.PostDate);

            return salesVms;
        }

        public EditSaleVm GetEditVm(int? saleId)
        {
            Sale sale = this.Context.Sales.Find(saleId);
            if (sale == null)
            {
                return null;
            }

            EditSaleVm vm = Mapper.Map<Sale, EditSaleVm>(sale);

            return vm;
        }

        public void EditSale(EditSaleBm bind, string currentUsername)
        {
            Sale sale = this.Context.Sales.Find(bind.Id);
            sale.Url = bind.SalePicture;
            sale.Category = bind.Category;
            sale.PhoneNumber = bind.PhoneNumber;
            sale.Specification = bind.Specification;
            sale.Price = bind.Price;
            sale.Title = bind.Title;
            var user = sale.User;
            this.Context.SaveChanges();
        }

        public bool IsUserAuthenticatedToEditCurrent(int? id, string userId)
        {
            var sale = this.Context.Sales.Find(id);
            if (sale.User.Id != userId)
            {
                return false;
            }

            return true;
        }

        public bool SaleExists(int? id)
        {
            if (this.Context.Sales.Find(id) == null)
            {
                return false;
            }

            return true;
        }

        public DeleteSaleVm GetDeleteVm(int id)
        {
            Sale sale = this.Context.Sales.Find(id);
            var vm = Mapper.Map<Sale, DeleteSaleVm>(sale);

            return vm;
        }

        public void DeleteSale(int id)
        {
            var sale = this.Context.Sales.Find(id);
            this.Context.Sales.Remove(sale);
            this.Context.SaveChanges();
        }

        public bool UserExists(string userId)
        {
            if (this.Context.Users.Find(userId) == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesForUser(string userId)
        {
            IEnumerable<Sale> sales = this.Context.Sales.Where(s => s.User.Id == userId).OrderByDescending(sale => sale.PostDate);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public string GetUserEmail(string userId)
        {
            var email = this.Context.Users.Find(userId).Email;

            return email;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByCategory(int category)
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false && (int)s.Category == category);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByTitle(string title)
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false && s.Title.ToLower().Contains(title.ToLower()));
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByCategoryAndTitle(int category, string title)
        {
            IEnumerable<Sale> sales = this.Context.Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.User.IsBanned == false && s.Title.ToLower().Contains(title.ToLower()) && (int)s.Category == category);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByTitleForUser(string userId, string search)
        {
            IEnumerable<Sale> sales = this.Context.Users.Find(userId).Sales.OrderByDescending(sale => sale.PostDate).Where(s => s.Title.ToLower().Contains(search.ToLower()));
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByCategoryForUser(string userId, int category)
        {
            IEnumerable<Sale> sales = this.Context.Users.Find(userId).Sales.OrderByDescending(sale => sale.PostDate).Where(s => (int)(s.Category) == category);
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }

        public IEnumerable<ShortSaleVm> GetAllSalesByCategoryAndTitleForUser(string userId, int category, string search)
        {
            IEnumerable<Sale> sales = this.Context.Users.Find(userId).Sales.OrderByDescending(sale => sale.PostDate).Where(s => (int)(s.Category) == category && s.Title.Contains(search));
            IEnumerable<ShortSaleVm> salesVms = Mapper.Map<IEnumerable<Sale>, IEnumerable<ShortSaleVm>>(sales);

            return salesVms;
        }
    }
}