using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShop.Data;
using BikeShop.Models;

namespace BikeShop.Controllers
{
    public class ShoppingBagsController : Controller
    {
        private readonly BikeShopContext _context;

        public ShoppingBagsController(BikeShopContext context)
        {
            _context = context;
        }

        public IActionResult Create(int myCustomerId)
        {
            ShoppingBag MyShoppingBag = new ShoppingBag() { myCustomerId = myCustomerId, Date = DateTime.Now };

            _context.ShoppingBags.Add(MyShoppingBag);
            _context.SaveChanges();
            return RedirectToAction("Details", "Customers", new { Id = myCustomerId });//new { Id = myCustomerId }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingBag = await _context.ShoppingBags.FindAsync(id);
            if (shoppingBag == null)
            {
                return NotFound();
            }
            return View(shoppingBag);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingBag =  _context.ShoppingBags
                .Include(x => x.myCustomer)
                .Include(x => x.myShoppingItems)
                .ThenInclude(x => x.myProduct)//theninclude gaat een niveau dieper, kijkt naar myShoppingItems
                .FirstOrDefault(x => x.Id == id);

            if (shoppingBag == null)
            {
                return NotFound();
            }
            return View(shoppingBag);
        }
        public async Task<IActionResult> Delete(int? id, int myCustomerId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingBag = await _context.ShoppingBags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingBag == null)
            {
                return NotFound();
            }

            return View(shoppingBag);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int myCustomerId)
        {
            var shoppingBag = await _context.ShoppingBags.FindAsync(id);
            ///eerst de items uit de bag verwijderen
            shoppingBag.myShoppingItems = _context.ShoppingItems.Where(x => x.myShoppingBagId == id).ToList();
            foreach (var item in shoppingBag.myShoppingItems)
            {
                _context.ShoppingItems.Remove(item);
            }

            
            _context.ShoppingBags.Remove(shoppingBag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Customers", new { Id = myCustomerId });
        }
        public IActionResult Back(int myCustomerId)//terug knop van de delete shoppingbag pagina
        {
            return RedirectToAction("Details", "Customers", new { Id = myCustomerId });
        }
        public IActionResult AddItem(int myProductId, int myCustomerId, int myQuantity)
        {
            ShoppingBag myShoppingBag = _context.ShoppingBags.OrderByDescending(x => x.Date).FirstOrDefault(x => x.myCustomerId == myCustomerId);
            //ShoppingBag myShoppingBag = _context.ShoppingBags.LastOrDefault(x => x.myCustomerId == myCustomerId);//.LastOrDefault();

            ShoppingItem myShoppingitem = new ShoppingItem();
            myShoppingitem.Productid = myProductId;
            myShoppingitem.myShoppingBagId = myShoppingBag.Id;
            myShoppingitem.Quantity = myQuantity;//TODO

            _context.ShoppingItems.Add(myShoppingitem);


            _context.SaveChanges();

            //return View(myShoppingBag);
            return RedirectToAction("Details", "ShoppingBags", new { Id = myShoppingBag.Id });
        }
    }
}
