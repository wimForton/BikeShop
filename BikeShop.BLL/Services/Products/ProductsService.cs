using BikeShop.DAL.Data.Repositories.Product;
using BikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.BLL.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _ProductRepository;
        public ProductsService(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            return _ProductRepository.GetProducts();
        }
        public List<ProductModel> GetProductsPage(int advancepage)
        {
            return _ProductRepository.GetProductsPage(advancepage);
        }
        public async Task<ProductModel> GetProductByIdAsync(int? id)
        {
            return await _ProductRepository.GetProductByIdAsync(id);
        }
        public int GetPageNumber()
        {
            return _ProductRepository.GetPageNumber();
        }
        public async Task AddProductAsync(ProductModel product)
        {
            await _ProductRepository.AddProductAsync(product);
        }
        public async Task UpdateProductAsync(ProductModel product)
        {
            await _ProductRepository.UpdateProductAsync(product);
        }
        public async Task RemoveProductAsync(int? id)
        {
            await _ProductRepository.RemoveProductAsync(id);
        }
        public bool ProductExists(int id)
        {
            return _ProductRepository.ProductExists(id);
        }
    }
}
