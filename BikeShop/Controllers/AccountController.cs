using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.Account;
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
        private readonly IAccountRepository _AccountService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BikeShopContext context, IAccountRepository AccountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _AccountService = AccountService;
        }
        public IActionResult Index()
        {
            return View(_AccountService.GetAccounts());
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            string myAccountId = id;
            var user = await _AccountService.GetAccountByIdAsync(myAccountId);
            if (myAccountId == "-1")
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            //var user = await _AccountService.GetAccountByIdAsync(myAccountId);
            List<ShoppingBagModel> myShoppingBags = _AccountService.GetShoppingBagsByAccount(user);

            ViewData["shoppingBags"] = myShoppingBags;

            return View(user);
        }
        public async Task<IActionResult> RegisterUser(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == true)
            {
                bool userRegistered = await _AccountService.RegisterUserAsync(loginViewModel);

                if (userRegistered)
                {
                    return View("Login");
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
                //var user = await _userManager.FindByNameAsync(login.UserName);
                var user = await _AccountService.GetAccountByNameAsync(login.UserName);
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

            //var account = await _context.Users.FindAsync(id);
            var account = await _AccountService.GetAccountByIdAsync(id);
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
            if (!await _AccountService.DeleteUserAsync(id))
            {
                //that went wrong
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
