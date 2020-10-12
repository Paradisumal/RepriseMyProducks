using System;
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
using Producks.UnderCuttersFacade;

namespace Producks.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreDb _context;
        private readonly ICategory _category;
        private readonly IBrand _brand;
        private readonly IProduct _product;

        public StoreController(StoreDb context, ICategory category, IBrand brand, IProduct product)
        {
            _context = context;
            _category = category;
            _brand = brand;
            _product = product;
        }

        // GET: ProductSearch
        public async Task<IActionResult> Search()
        {
            /*var client = new HttpClient();
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

            IEnumerable<UCBrandDto> ucBrands = await brandResponse.Content.ReadAsAsync<IEnumerable<UCBrandDto>>();
            var ucBrandList = ucBrands.Select(c => new Brand
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();*/

            var ucCategories = await _category.GetCategories();
            var ucCategoryList = ucCategories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            var ucBrands = await _brand.GetBrands();
            var ucBrandList = ucBrands.Select(c => new Brand
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            var localCategories = await _context.Categories.Where(c => c.Active == true).ToListAsync();
            var localBrands = await _context.Brands.Where(c => c.Active == true).ToListAsync();

            /*var categories = localCategories.Concat(ucCategoryList);
            var brands = localBrands.Concat(ucBrandList);*/

            var categories = localCategories.Concat(ucCategoryList);
            var brands = localBrands.Concat(ucBrandList);

            var viewModel = new ProductDrillDown { };

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name");

            return View(viewModel);
        }

        // GET: Products
        public async Task<IActionResult> Products([FromQuery, Required] int categoryId,
                                                [FromQuery, Required] string categoryName,
                                                     [FromQuery, Required] int brandId)
        {
            /*var client = new HttpClient();
            client.BaseAddress = new Uri("http://undercutters.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.Timeout = TimeSpan.FromSeconds(5);*/

            /*HttpResponseMessage productResponse = await client.GetAsync("api/Product?category_id=" + categoryId 
                                                                                + "&category_name=" + categoryName 
                                                                                     + "&brand_id=" + brandId 
                                                                                    + "&min_price=" + 0 
                                                                                    + "&max_price=" + 9999);
            productResponse.EnsureSuccessStatusCode();

            IEnumerable<UCProductDto> ucProducts = await productResponse.Content.ReadAsAsync<IEnumerable<UCProductDto>>();
            var ucProductList = ucProducts.Select(c => new Product
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                BrandId = c.BrandId,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                StockLevel = 999
            }).ToList();*/

            var ucProducts = await _product.GetProducts(categoryId, categoryName, brandId);
            var ucProductList = ucProducts.Select(c => new Product
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                BrandId = c.BrandId,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                StockLevel = 999
            }).ToList();

            var localProducts = await _context.Products.Where(q => q.Active == true)
                                            .Where(p => p.CategoryId == categoryId)
                                            .Where(p => p.BrandId == brandId).Include(p => p.Brand).ToListAsync();

            var products = localProducts.Concat(ucProductList);

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