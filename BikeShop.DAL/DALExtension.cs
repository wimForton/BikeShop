using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.Account;
using BikeShop.DAL.Data.Repositories.Product;
using BikeShop.DAL.Data.Repositories.ShoppingBag;
using BikeShop.DAL.Data.Repositories.SiteConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bikeshop.DAL
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


            //.AddSignInManager<SignInManager<IdentityUser>>();//testje

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;

            });

            return services;
        }
    }
}
