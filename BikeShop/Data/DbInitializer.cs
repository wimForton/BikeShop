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
            var shoppingbags = new ShoppingBag[]
            {
                new ShoppingBag{ Date = DateTime.Now, myCustomerId = 1 }
            };
            foreach (ShoppingBag s in shoppingbags)
            {
                context.ShoppingBags.Add(s);
            }

            var customers = new Customer[]
            {
                new Customer{ Firstname = "Wim", myShoppingBags = new List<ShoppingBag>(), Name = "Forton"},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            var products = new Product[]
            {
                new Product{ Name = "Grasmasjiene", Imagepath = "images/Grasmachine.jpg", Price = 150},
                new Product{ Name = "Boormasjiene", Imagepath = "images/Boormachine.jpg", Price = 80},
                new Product{ Name = "Zaagmasjiene", Imagepath = "images/Zaagmachine.jpg", Price = 75},
                new Product{ Name = "Snoeimasjiene", Imagepath = "images/Snoeimachine.jpg", Price = 90.5},
                new Product{ Name = "Hamerke", Imagepath = "images/Hamer.jpg", Price = 50},
                new Product{ Name = "Zaagske", Imagepath = "images/zaag.jpg", Price = 20},
                new Product{ Name = "Beiteltje", Imagepath = "images/beitel.jpg", Price = 70},
                new Product{ Name = "Productje", Imagepath = "images/product.jpg", Price = 60}
            };
            Random myRandom = new Random();
            for (int i = 0; i < 100; i++)
            {
                int index = myRandom.Next(0,7);
                Product p = new Product();
                p.Name = products[index].Name;
                p.Imagepath = products[index].Imagepath;
                p.Price = myRandom.Next(20, 200);
                context.Products.Add(p);

            }
            //foreach (Product p in products)
            //{
            //    context.Products.Add(p);
            //}

            context.SaveChanges();

        }
    }
}
