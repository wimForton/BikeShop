using BikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DAL.Data.Repositories.Product
{
    public interface IProductRepository
    {
        public IEnumerable<ProductModel> GetProducts();
        public List<ProductModel> GetProductsPage(int advancepage);
        public BikeShopContext GetContext();
        public Task<ProductModel> GetProductByIdAsync(int? id);
        public int GetPageNumber();
        public Task AddProductAsync(ProductModel product);
        public Task UpdateProductAsync(ProductModel product);
        public Task RemoveProductAsync(int? id);
        public bool ProductExists(int id);
    }
}
