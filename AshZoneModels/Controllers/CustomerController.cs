using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AshZoneModels.Models;

namespace AshZoneModels.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreDbContext _context;

        public CustomerController(StoreDbContext context)
        {
          
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Customer()
        {

            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }

    }
}
