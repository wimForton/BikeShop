using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShop.Data;
using BikeShop.Models;
using Microsoft.AspNetCore.Authorization;
using BikeShop.DAL.Data;
using BikeShop.DAL.Data.Repositories.Product;

namespace BikeShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BikeShopContext _context;
        public static int PageNumber = 1;
        public static int ProductsPerPage = 9;
        private readonly IProductRepository _ProductService;
        public ProductsController(IProductRepository ProductService)
        {

            _ProductService = ProductService;
        }

        // GET: Products
        public IActionResult Index(int advancepage = 0)
        {
            List<ProductModel> onePageList = _ProductService.GetProductsPage(advancepage);
            ViewData["pagenumber"] = _ProductService.GetPageNumber();
            return View(onePageList);
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            ViewData["quantity"] = 1;

            ProductModel product = await _ProductService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Imagepath,Price")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await _ProductService.AddProduct(product);
                return RedirectToAction(nameof(Index));//fancy way om niet harcoded "index" te moeten schrijven
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var product = await _ProductService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Imagepath,Price")] ProductModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ProductService.UpdateProduct(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_ProductService.ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _ProductService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ProductService.RemoveProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
