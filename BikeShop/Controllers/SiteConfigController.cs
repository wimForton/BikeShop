using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.Account;
using BikeShop.DAL.Data.Repositories.SiteConfig;
using BikeShop.Data;
using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Controllers
{
    public class SiteConfigController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly BikeShopContext _context;
        private readonly ISiteConfigRepository _SiteConfigService;

        public SiteConfigController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ISiteConfigRepository SiteConfigService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _SiteConfigService = SiteConfigService;
        }
        public IActionResult Index()
        {
            //return View(_SiteConfigService.GetAccounts());
            return View();
        }
    }
}
