using BikeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DAL.Data.Repositories.SiteConfig
{
    public class SiteConfigRepository : ISiteConfigRepository
    {
        private readonly BikeShopContext _context;
        public SiteConfigRepository(BikeShopContext context)
        {
            _context = context;
        }

    }
}
