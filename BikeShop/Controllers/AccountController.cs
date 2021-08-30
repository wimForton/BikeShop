using BikeShop.DAL.Data;
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
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly BikeShopContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BikeShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            var user = await _context.Users.FindAsync(id);

            List<ShoppingBagModel> myShoppingBags = _context.ShoppingBags.Where(x => x.IdentityUserId == user.Id).ToList();

            ViewData["shoppingBags"] = myShoppingBags;

            return View(user);
        }
        public async Task<IActionResult> RegisterUser(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == true)
            {

                var user = new IdentityUser { UserName = loginViewModel.UserName, PasswordHash = loginViewModel.Password, Email = loginViewModel.Email };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if (result.Succeeded)
                {
                    //Hier maken we de shoppingbag en voegen ze toe aan de db
                    _context.ShoppingBags.Add(new ShoppingBagModel { IdentityUserId = user.Id, Date = System.DateTime.Now });
                    _context.SaveChanges();
                    return View("Login");
                }
                else
                {
                    //what if the user couldn't be added?
                    //logging
                    //exception handling
                    //...
                }
            }
            return View("register", loginViewModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Authenticate(LoginViewModel login)
        {
            if (ModelState.IsValid == true)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
            }
            return View("Login", login);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        ///////////////////////////////////////////////////////////////////////////////delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Users.FindAsync(id);
            //.FindByIdAsync(userId);
            //.FirstOrDefault(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Users.FindAsync(id);
            ShoppingBagModel myShoppingBag = _context.ShoppingBags.FirstOrDefault(x => x.IdentityUserId == account.Id);
            while (myShoppingBag != null)
            {
                _context.ShoppingBags.Remove(myShoppingBag);
                await _context.SaveChangesAsync();
                myShoppingBag = _context.ShoppingBags.FirstOrDefault(x => x.IdentityUserId == account.Id);///shoppingbag ophalen op basis van persoon
            }
            _context.Users.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
