﻿using BikeShop.Models;
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
            return _context.Products;
        }
        public async Task<ProductModel> GetProductById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            return product;
        }
        public async Task AddProduct(ProductModel product)
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
        public async Task RemoveProduct(int? id)
        {
            ProductModel product = await GetProductById(id);

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
        public async Task UpdateProduct(ProductModel product)
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
            return _context.Products.Skip((ProductRepository.PageNumber - 1) * ProductRepository.ProductsPerPage).Take(ProductRepository.ProductsPerPage).ToList();
        }
        public int GetPageNumber()
        {
            return ProductRepository.PageNumber;
        }
    }
}
