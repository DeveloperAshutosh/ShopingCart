using System;
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
        //To add one mre item to the card which is allready been added to the cart 
        public async Task<IActionResult> IncrementItem(int cartid)
        {
            var cart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartid);
            cart.Count += 1;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //To reduce the item by 1 which is allready been added to the cart 
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
        //To remove the item from the cart Completely 
        public async Task<IActionResult> RemoveItem(int cartid)
        {
            var cart = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartid);
            _context.ShoppingCart.Remove(cart);
            await _context.SaveChangesAsync();

            var cnt = _context.ShoppingCart.Where(u => u.AppUserId == cart.AppUserId).ToList().Count();
            HttpContext.Session.SetInt32("ssCartCount", cnt);
            return RedirectToAction(nameof(Index));
        }
        //Check Out Method to proceed to the payment section
        public async Task<IActionResult> Checkout()
        {
            DetailCart = new OrderDetailsCart()
            {
                OrderHeader = new Models.OrderHeader()
            };

            DetailCart.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            AppUser Appuser = await _context.AppUsers.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();

            var cart = _context.ShoppingCart.Where(c => c.AppUserId == claim.Value);

            if (cart != null)
            {
                DetailCart.ListCart = cart.ToList();
            }
            // To get the Total Amount Due 
            foreach (var list in DetailCart.ListCart)
            {
                list.Productitem = await _context.Products.FirstOrDefaultAsync(m => m.ID == list.ProductId);
                DetailCart.OrderHeader.OrderTotal = DetailCart.OrderHeader.OrderTotal + (list.Productitem.Price * list.Count);
            }
            DetailCart.OrderHeader.OrderTotalOriginal = DetailCart.OrderHeader.OrderTotal;
            DetailCart.OrderHeader.PickupName = Appuser.UserName;
           
            DetailCart.OrderHeader.PickUpDate = DateTime.Now;

            return View(DetailCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Checkout")]
        public async Task<IActionResult> CheckoutPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            DetailCart.ListCart = await _context.ShoppingCart.Where(c => c.AppUserId == claim.Value).ToListAsync();


            DetailCart.OrderHeader.OrderDate = DateTime.Now;
            DetailCart.OrderHeader.UserId = claim.Value;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            _context.OrderHeaders.Add(DetailCart.OrderHeader);
            await _context.SaveChangesAsync();

            DetailCart.OrderHeader.OrderTotalOriginal = 0;

            foreach (var item in DetailCart.ListCart)
            {
                item.Productitem = await _context.Products.FirstOrDefaultAsync(m => m.ID == item.ProductId);
                OrderDetail orderdetails = new OrderDetail
                {
                    
                    ProductItemId = item.ProductId,
                    OrderId = DetailCart.OrderHeader.Id,
                    Description = item.Productitem.ProductDescription,
                    Name = item.Productitem.ProductName,
                    Price = item.Productitem.Price,
                    Count = item.Count
                };
               
                DetailCart.OrderHeader.OrderTotalOriginal += orderDetails.Count * orderdetails.Price;
                _context.OrderDetails.Add(orderdetails);
            }
            _context.ShoppingCart.RemoveRange(DetailCart.ListCart);
            HttpContext.Session.SetInt32("ssCartCount", 0);

            await _context.SaveChangesAsync();
            return RedirectToAction("Confirm", "Order");
        }

    }
}
