using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShop.Data;
using BikeShop.Models;
using BikeShop.DAL.Data;

namespace BikeShop.Controllers
{
    public class ShoppingItemsController : Controller
    {
        private readonly BikeShopContext _context;

        public ShoppingItemsController(BikeShopContext context)
        {
            _context = context;
        }
    }
}
