using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShop.Data;
using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.ShoppingBag;
using BikeShop.BLL.Services.ShoppingBags;

namespace BikeShop.Controllers
{
    public class ShoppingBagsController : Controller
    {
        private readonly BikeShopContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IShoppingBagsService _ShoppingBagService;
        public ShoppingBagsController(BikeShopContext context, UserManager<IdentityUser> userManager, IShoppingBagsService ShoppingBagService)
        {
            _userManager = userManager;
            _context = context;
            _ShoppingBagService = ShoppingBagService;
        }

        public async Task<IActionResult> Create(string userName)
        {
            await _ShoppingBagService.CreateShoppingBagAsync(userName);
            var user = await _userManager.FindByNameAsync(userName);//Dit moeten we doen omdat User.Identity.Name op zich niet veilig is en geen id weergeeft

            return RedirectToAction("Details", "Account", new { id = user.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //var shoppingBag = await _context.ShoppingBags.FindAsync(id);
            var shoppingBag = await _ShoppingBagService.GetShoppingBagByIdAsync(id);
            if (shoppingBag == null)
            {
                return NotFound();
            }
            return View(shoppingBag);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var shoppingBag = await _ShoppingBagService.GetShoppingBagWithDataByIdAsync(id);

            if (shoppingBag == null)
            {
                return NotFound();
            }
            return View(shoppingBag);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var shoppingBag = await _ShoppingBagService.GetShoppingBagByIdAsync(id);
            if (shoppingBag == null)
            {
                return NotFound();
            }

            return View(shoppingBag);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)///", int myCustomerId" niet meer TODO: naar customer
        {
            string shoppingBagUserId = await _ShoppingBagService.GetShoppingBagUserByIdAsync(id);//doe dit nu de shoppingbag er nog is
            await _ShoppingBagService.DeleteConfirmedAsync(id);
            return RedirectToAction("Details", "Account", new { id = shoppingBagUserId });
        }
        public IActionResult Back(int myCustomerId)//terug knop van de delete shoppingbag pagina
        {
            return RedirectToAction("Details", "Customers", new { Id = myCustomerId });///Bestaat niet meer TODO: naar customer
        }
        public async Task<IActionResult> AddItem(int myProductId, int myQuantity)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);///Ophalen user op basis van ingelogde persoon
            int myShoppingBagId = _ShoppingBagService.AddItem(user, myProductId, myQuantity);
            return RedirectToAction("Details", "ShoppingBags", new { Id = myShoppingBagId });
        }
        public async Task<IActionResult> RemoveItem(int myItemId, int myShoppingBagId)
        {
            await _ShoppingBagService.RemoveItem(myItemId);
            return RedirectToAction("Details", "ShoppingBags", new { Id = myShoppingBagId });
        }
    }
}

