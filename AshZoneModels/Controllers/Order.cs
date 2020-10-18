using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AshZoneModels.Models;
using AshZoneModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshZoneModels.Controllers
{
    public class Order : Controller
    {
        private readonly StoreDbContext _context;

        public Order(StoreDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Confirm(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel()
            {
                OrderHeader = await _context.OrderHeaders.Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId),
                OrderDetails = await _context.OrderDetails.Where(o => o.OrderId == id).ToListAsync()
            };

            return View(orderDetailsViewModel);
        }
        


    }
}
