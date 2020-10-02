using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Producks.Data;

namespace Producks.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreDb _context;

        public StoreController(StoreDb context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Where(c => c.Active == true).ToListAsync();

            return View(categories);
        }

        // GET: Products
        public async Task<IActionResult> Products([FromQuery, Required] int categoryId)
        {
            var products = _context.Products.Where(q => q.Active == true)
                                            .Where(p => p.CategoryId == categoryId).Include(p => p.Brand);
            return View(await products.ToListAsync());
        }
    }
}