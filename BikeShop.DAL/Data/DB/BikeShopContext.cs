using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.DAL.Data
{
    public class BikeShopContext:IdentityDbContext<IdentityUser>
    {
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ShoppingBagModel> ShoppingBags { get; set; }
        public DbSet<ShoppingItemModel> ShoppingItems { get; set; }
        public BikeShopContext(DbContextOptions<BikeShopContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Customer>().ToTable("Customer");
        //    modelBuilder.Entity<Product>().ToTable("Product");
        //    modelBuilder.Entity<ShoppingBag>().ToTable("ShoppingBag");
        //    modelBuilder.Entity<ShoppingItem>().ToTable("ShoppingItem");
        //}

    }
}
