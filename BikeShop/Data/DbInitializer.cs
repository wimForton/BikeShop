using BikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Data
{
    public class DbInitializer
    {
        public static void Initialize(BikeShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer{ Firstname = "Wim", myShoppingBags = new List<ShoppingBag>, Name = "Forton"}
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            var products = new Product[]
            {
                new Product{ Name = "Grasmasjiene", Imagepath = "images/Grasmachine.jpg"}
                //new Customer{ Firstname = "Wim", myShoppingBags = new List<ShoppingBag>, Name = "Forton"}
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }

            context.SaveChanges();

        }
    }
}
