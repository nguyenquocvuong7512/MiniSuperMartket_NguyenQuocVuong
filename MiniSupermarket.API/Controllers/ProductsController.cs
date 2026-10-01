using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Category) // Nạp thông tin danh mục kèm theo
                .OrderByDescending(p => p.ProductId)
                .ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] int? categoryId)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(p =>
                    p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.Barcode.Contains(keyword));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            var list = await query.Include(p => p.Category).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Product newProduct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Kiểm tra mã vạch trùng
            var existBarcode = await _context.Products.AnyAsync(p => p.Barcode == newProduct.Barcode);
            if (existBarcode) return BadRequest(new { message = "Mã vạch đã tồn tại!" });

            // Kiểm tra Danh mục tồn tại
            var cat = await _context.Categories.FindAsync(newProduct.CategoryId);
            if (cat == null) return BadRequest(new { message = "Danh mục không tồn tại!" });

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updateProduct)
        {
            if (id != updateProduct.ProductId)
                return BadRequest(new { message = "Mã sản phẩm không khớp!" });

            var prod = await _context.Products.FindAsync(id);
            if (prod == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });

            // Kiểm tra mã vạch trùng (trừ chính sản phẩm đang sửa)
            var existBarcode = await _context.Products
                .AnyAsync(p => p.Barcode == updateProduct.Barcode && p.ProductId != id);
            if (existBarcode) return BadRequest(new { message = "Mã vạch đã tồn tại!" });

            prod.Barcode = updateProduct.Barcode;
            prod.ProductName = updateProduct.ProductName;
            prod.Price = updateProduct.Price;
            prod.StockQuantity = updateProduct.StockQuantity;
            prod.CategoryId = updateProduct.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _context.Products.FindAsync(id);
            if (prod == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });

            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}