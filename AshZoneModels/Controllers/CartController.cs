﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AshZoneModels.Models;
using AshZoneModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshZoneModels.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreDbContext _context;

        [BindProperty]
        public OrderDetailsCart DetailCart { get; set; }
        public CartController(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            DetailCart = new OrderDetailsCart()
            {
                OrderHeader = new Models.OrderHeader()
            };

            DetailCart.OrderHeader.OrderTotal = 0;
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _context.ShoppingCart.Where(c => c.AppUserId == userId);


            if (cart != null)
            {
                DetailCart.ListCart = cart.ToList();
            }
            

            foreach (var list in DetailCart.ListCart)
            {
                list.Productitem = await _context.Products.FirstOrDefaultAsync(x => x.ID == list.ProductId);
                DetailCart.OrderHeader.OrderTotal = DetailCart.OrderHeader.OrderTotal + (list.Productitem.Price * list.Count);

                list.Productitem.ProductDescription = list.Productitem.ProductDescription;

                if (list.Productitem.ProductDescription.Length > 100)
                {
                    list.Productitem.ProductDescription = list.Productitem.ProductDescription.Substring(0, 99) + "...";
                }
            }
            DetailCart.OrderHeader.OrderTotalOriginal = DetailCart.OrderHeader.OrderTotal;
            return View(DetailCart);
        }
        public async Task<IActionResult> IncrementItem(int cartid)
        {
            var cart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartid);
            cart.Count += 1;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ReduceItem(int cartid)
        {
            var cart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartid);
            if (cart.Count == 1)
            {
                _context.ShoppingCart.Remove(cart);
                await _context.SaveChangesAsync();

                var cnt = _context.ShoppingCart.Where(u => u.AppUserId == cart.AppUserId).ToList().Count();
                HttpContext.Session.SetInt32("ssCartCount", cnt);
            }
            else
            {
                cart.Count -= 1;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveItem(int cartid)
        {
            var cart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartid);
            _context.ShoppingCart.Remove(cart);
            await _context.SaveChangesAsync();

            var cnt = _context.ShoppingCart.Where(u => u.AppUserId == cart.AppUserId).ToList().Count();
            HttpContext.Session.SetInt32("ssCartCount", cnt);
            return RedirectToAction(nameof(Index));
        }
      
      
    }
}