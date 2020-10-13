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
           
            
            var cnt = _context.ShoppingCart.ToList().Count();

            return View(await _context.Products.ToListAsync());
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


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShopingCart cartobject)
        {
            cartobject.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartobject.AppUserId = claim.Value;

                ShopingCart cartfromdb = await _context.ShoppingCart.Where(c => c.AppUserId == cartobject.AppUserId && c.ProductId == cartobject.ProductId).FirstOrDefaultAsync();

                if (cartfromdb == null)
                {
                   await _context.ShoppingCart.AddAsync(cartobject);
                }
                else
                {
                    cartfromdb.Count++;
                }

                await _context.SaveChangesAsync();

                var Count = _context.ShoppingCart.Where(c => c.AppUserId == cartobject.AppUserId).ToList().Count();

                

                return RedirectToAction(nameof(Customer));
            }
            else
            {
                var productfromdb = await _context.Products.Where(m => m.ID == cartobject.ProductId).FirstOrDefaultAsync();

                ShopingCart CartObj = new ShopingCart()
                {
                    Productitem = productfromdb,
                    ProductId = productfromdb.ID
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
