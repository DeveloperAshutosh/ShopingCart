using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AshZoneModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AshZoneModels.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreDbContext _context;

        public CustomerController(StoreDbContext context )
            
        {
            
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Customer()
        {
           
            return View(await _context.Products.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Customer(string productsearch)
        {
            ViewData["GetProducts"] = productsearch;
            var query = from x in _context.Products select x;

            if (!String.IsNullOrEmpty(productsearch))
            {
                query = query.Where(x => x.ProductName.Contains(productsearch));
            }
            return View(await query.AsNoTracking().ToListAsync());
        }
        // GET: Products/Details/5

        public async Task<IActionResult> Details(int id)
        {
            

            var menuitemfromdb = await _context.Products.Where(s => s.ID == id).FirstOrDefaultAsync();

            ShopingCart cartobj = new ShopingCart()
            {
                Productitem = menuitemfromdb,
                ProductId = menuitemfromdb.ID
            };

            return View(cartobj);
        }


       // To Add the product item into the cart object and handle the count of cart 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShopingCart cartobject )
        {
            
            if (ModelState.IsValid)
            {
                cartobject.Id = 0;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                cartobject.AppUserId = userId;

                ShopingCart cartfromdb = await _context.ShoppingCart
                    .Where(c => c.AppUserId == cartobject.AppUserId && c.ProductId == cartobject.ProductId)
                    .FirstOrDefaultAsync();

                if (cartfromdb == null)
                {
                   await _context.ShoppingCart.AddAsync(cartobject);
                }
                else
                {
                    cartfromdb.Count = cartfromdb.Count + cartobject.Count;
                }

                await _context.SaveChangesAsync();

                 var Count = _context.ShoppingCart.Where(c => c.AppUserId == cartobject.AppUserId)
                    .ToList().Count();

                HttpContext.Session.SetInt32("ssCartCount", Count);

                return RedirectToAction(nameof(Customer));
            }
            else
            {
                var productFromDB = await _context.Products.Where(m => m.ID == cartobject.ProductId).FirstOrDefaultAsync();

                ShopingCart CartObj = new ShopingCart()
                {
                    Productitem = productFromDB,
                    ProductId = productFromDB.ID
                };

                return View(CartObj);
            }
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
