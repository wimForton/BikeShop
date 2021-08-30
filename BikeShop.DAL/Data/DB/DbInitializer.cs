using BikeShop.DAL.Data;
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

            var products = new ProductModel[]
            {
                new ProductModel{ Name = "Grasmasjiene", Imagepath = "images/Grasmachine.jpg", Price = 150},
                new ProductModel{ Name = "Boormasjiene", Imagepath = "images/Boormachine.jpg", Price = 80},
                new ProductModel{ Name = "Zaagmasjiene", Imagepath = "images/Zaagmachine.jpg", Price = 75},
                new ProductModel{ Name = "Snoeimasjiene", Imagepath = "images/Snoeimachine.jpg", Price = 90.5},
                new ProductModel{ Name = "Hamerke", Imagepath = "images/Hamer.jpg", Price = 50},
                new ProductModel{ Name = "Zaagske", Imagepath = "images/zaag.jpg", Price = 20},
                new ProductModel{ Name = "Beiteltje", Imagepath = "images/beitel.jpg", Price = 70},
                new ProductModel{ Name = "Productje", Imagepath = "images/product.jpg", Price = 60}
            };
            Random myRandom = new Random();
            for (int i = 0; i < 100; i++)
            {
                int index = myRandom.Next(0,7);
                ProductModel p = new ProductModel();
                p.Name = products[index].Name;
                p.Imagepath = products[index].Imagepath;
                p.Price = myRandom.Next(20, 200);
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
    }
}
