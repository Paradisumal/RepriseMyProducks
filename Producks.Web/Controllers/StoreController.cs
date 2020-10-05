using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Producks.Data;
using Producks.Web.Models;

namespace Producks.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreDb _context;

        public StoreController(StoreDb context)
        {
            _context = context;
        }

        // GET: ProductSearch
        public async Task<IActionResult> Search()
        {
            var categories = await _context.Categories.Where(c => c.Active == true).ToListAsync();
            var brands = await _context.Brands.Where(c => c.Active == true).ToListAsync();

            var viewModel = new ProductDrillDown { };

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name");

            return View(viewModel);
        }

        // GET: Products
        public async Task<IActionResult> Products(ProductDrillDown productDrillDown)
        {
            var products = _context.Products.Where(q => q.Active == true)
                                            .Where(p => p.CategoryId == productDrillDown.CategoryId)
                                            .Where(p => p.BrandId == productDrillDown.BrandId).Include(p => p.Brand);
            return View(await products.ToListAsync());
        }
    }
}