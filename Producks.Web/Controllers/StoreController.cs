﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://undercutters.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);

            HttpResponseMessage categoryResponse = await client.GetAsync("api/Category");
            categoryResponse.EnsureSuccessStatusCode();

            IEnumerable<UCCategoryDto> ucCategories = await categoryResponse.Content.ReadAsAsync<IEnumerable<UCCategoryDto>>();
            var ucCategoryList = ucCategories.Select(c => new Category 
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            HttpResponseMessage brandResponse = await client.GetAsync("api/Brand");
            brandResponse.EnsureSuccessStatusCode();
            var ucBrandList = ucCategories.Select(c => new Brand
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            IEnumerable<UCBrandDto> ucBrands = await brandResponse.Content.ReadAsAsync<IEnumerable<UCBrandDto>>();

            var localCategories = await _context.Categories.Where(c => c.Active == true).ToListAsync();
            var localBrands = await _context.Brands.Where(c => c.Active == true).ToListAsync();

            var categories = localCategories.Concat(ucCategoryList);
            var brands = localBrands.Concat(ucBrandList);

            var viewModel = new ProductDrillDown { };

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name");

            return View(viewModel);
        }

        // GET: Products
        public async Task<IActionResult> Products([FromQuery, Required] int categoryId,
                                                     [FromQuery, Required] int brandId)
        {
            var products = await _context.Products.Where(q => q.Active == true)
                                            .Where(p => p.CategoryId == categoryId)
                                            .Where(p => p.BrandId == brandId).Include(p => p.Brand).ToListAsync();

            var viewModel = await _context.Products.Select(p => new ProductStoreViewModel
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockLevel = p.StockLevel,
                Active = p.Active,
                Category = p.Category.Name,
                Brand = p.Brand.Name
            }).ToListAsync();

            return View(viewModel);
        }
    }
}