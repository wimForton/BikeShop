using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.Account;
using BikeShop.DAL.Data.Repositories.Product;
using BikeShop.DAL.Data.Repositories.ShoppingBag;
using BikeShop.DAL.Data.Repositories.SiteConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoMVC.DAL
{
    public static class DALExtension
    {
        public static IServiceCollection RegisterDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BikeShopContext>(options => options.UseSqlServer(configuration.GetConnectionString("BikeShopConnection")));


            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IShoppingBagRepository, ShoppingBagRepository>();
            services.AddTransient<ISiteConfigRepository, SiteConfigRepository>();

            return services;
        }
    }
}
