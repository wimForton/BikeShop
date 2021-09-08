using BikeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DAL.Data.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        public static int PageNumber = 1;
        public static int ProductsPerPage = 9;
        private readonly BikeShopContext _context;
        public ProductRepository(BikeShopContext context)
        {
            _context = context;
        }
        public BikeShopContext GetContext()
        {
            return _context;
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            var x = _context.Products.Include(x => x.Discounts);
            return x;
        }
        public async Task<ProductModel> GetProductByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var product = await _context.Products
                .Include(x => x.Discounts)
                .FirstOrDefaultAsync(m => m.Id == id);
            return product;
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        public async Task AddProductAsync(ProductModel product)
        {
            _context.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var x = e.Message;
                //Message.box(x);
                throw;
            }
        }
        public async Task RemoveProductAsync(int? id)
        {
            ProductModel product = await GetProductByIdAsync(id);
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var x = e.Message;
                throw;
            }
        }
        public async Task UpdateProductAsync(ProductModel product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var x = e.Message;
                //Message.box(x);
                throw;
            }
        }
        public List<ProductModel> GetProductsPage(int advancepage)
        {
            ProductRepository.PageNumber += advancepage;
            if (ProductRepository.PageNumber < 1)
            {
                ProductRepository.PageNumber = 1;
            }
            if (ProductRepository.PageNumber > _context.Products.Count() / ProductRepository.ProductsPerPage + 1)
            {
                ProductRepository.PageNumber = _context.Products.Count() / ProductRepository.ProductsPerPage + 1;
            }
            var x = _context.Products.Include(x => x.Discounts);
            return x.Skip((ProductRepository.PageNumber - 1) * ProductRepository.ProductsPerPage).Take(ProductRepository.ProductsPerPage).ToList();
        }
        public int GetPageNumber()
        {
            return ProductRepository.PageNumber;
        }
    }
}
