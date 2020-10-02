using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Producks.Data;
using Producks.Web.Models;

namespace Producks.Web.Controllers
{
    [ApiController]
    public class ExportsController : ControllerBase
    {
        private readonly StoreDb _context;

        public ExportsController(StoreDb context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet("api/Brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _context.Brands
                                       .Select(b => new BrandDto
                                       {
                                           Id = b.Id,
                                           Name = b.Name,
                                           Active = b.Active
                                       })
                                       .ToListAsync();
            return Ok(brands);
        }

        // GET: api/Categories
        [HttpGet("api/Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                                       .Select(c => new CategoryDto
                                       {
                                           Id = c.Id,
                                           Name = c.Name,
                                           Description = c.Description,
                                           Active = c.Active
                                       })
                                       .ToListAsync();
            return Ok(categories);
        }

        // GET: api/Products?brandId=X?categoryId=X?minPrice=X&maxPrice=X
        [HttpGet("api/Products")]
        public async Task<IActionResult> GetProducts([FromQuery, Required] int brandId,
                                                     [FromQuery, Required] int categoryId,
                                                     [FromQuery, Required] double minPrice,
                                                     [FromQuery, Required] double maxPrice)
        {
            var products = await _context.Products.Where(s => s.BrandId == brandId)
                                       .Where(r => r.CategoryId == categoryId)
                                       .Where(q => q.Price > minPrice && q.Price < maxPrice)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }

        // GET: api/Products?brandId=X&categoryId=X
        [HttpGet("api/Products/byBrandAndCategory")]
        public async Task<IActionResult> GetProductsByBrandAndCategory([FromQuery, Required] int brandId,
                                                                       [FromQuery, Required] int categoryId)
        {
            var products = await _context.Products.Where(r => r.BrandId == brandId)
                                       .Where(q => q.CategoryId == categoryId)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }

        // GET: api/Products?brandId=X?minPrice=X&maxPrice=X
        [HttpGet("api/Products/byBrandAndPriceRange")]
        public async Task<IActionResult> GetProductsByBrandAndPriceRange([FromQuery, Required] int brandId,
                                                                         [FromQuery, Required] double minPrice,
                                                                         [FromQuery, Required] double maxPrice)
        {
            var products = await _context.Products.Where(r => r.BrandId == brandId)
                                       .Where(q => q.Price > minPrice && q.Price < maxPrice)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }

        // GET: api/Products?categoryId=X?minPrice=X&maxPrice=X
        [HttpGet("api/Products/byCategoryAndPriceRange")]
        public async Task<IActionResult> GetProductsByCategoryAndPriceRange([FromQuery, Required] int categoryId,
                                                                            [FromQuery, Required] double minPrice,
                                                                            [FromQuery, Required] double maxPrice)
        {
            var products = await _context.Products.Where(r => r.CategoryId == categoryId)
                                       .Where(q => q.Price > minPrice && q.Price < maxPrice)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }

        // GET: api/Products?brandId=X
        [HttpGet("api/Products/byBrand")]
        public async Task<IActionResult> GetProductsByBrand([FromQuery, Required] int brandId)
        {
            var products = await _context.Products.Where(q => q.BrandId == brandId)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }

        // GET: api/Products?categoryId=X
        [HttpGet("api/Products/byCategory")]
        public async Task<IActionResult> GetProductsByCategory([FromQuery, Required] int categoryId)
        {
            var products = await _context.Products.Where(q => q.CategoryId == categoryId)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }

        // GET: api/Products?minPrice=X&maxPrice=X
        [HttpGet("api/Products/byPriceRange")]
        public async Task<IActionResult> GetProductsByPriceRange([FromQuery, Required] double minPrice,
                                                                 [FromQuery, Required] double maxPrice)
        {
            var products = await _context.Products.Where(q => q.Price > minPrice && q.Price < maxPrice)
                                       .Select(p => new ProductDto
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
                                       })
                                       .ToListAsync();
            return Ok(products);
        }
    }
}
