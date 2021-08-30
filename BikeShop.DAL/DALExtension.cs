using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.Product;
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

            return services;
        }
    }
}
