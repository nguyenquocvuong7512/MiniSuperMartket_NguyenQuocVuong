using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Dto;
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

        // GET: api/products/paged?keyword=sua&categoryId=1&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            // 1. Kiểm tra tham số trang đầu vào hợp lệ
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5; // Mặc định 5 dòng, tối đa 50 dòng

            // 2. Khởi tạo truy vấn LINQ IQueryable kết hợp Eager Loading (.Include)
            // AsNoTracking giúp tối ưu hóa bộ nhớ RAM cho các truy vấn chỉ đọc dữ liệu
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 3. Lọc theo nhóm hàng (nếu có chọn) - Bài tập 6.2
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 4. Lọc theo từ khóa tìm kiếm (nếu người dùng có nhập)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(search)
                                      || p.Barcode.Contains(search));
            }

            // 5. Đếm tổng số lượng bản ghi thỏa mãn điều kiện
            int totalItems = await query.CountAsync();

            // 6. Tính toán tổng số trang
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 7. Áp dụng toán tử Skip và Take để phân trang trực tiếp dưới SQL Server
            var items = await query
                .OrderBy(p => p.ProductId) // Bắt buộc phải OrderBy trước khi Skip/Take
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Unit = p.Unit,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            // 8. Đóng gói kết quả trả về cho Client
            var response = new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .OrderByDescending(p => p.ProductId)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Unit = p.Unit,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Unit = p.Unit,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .FirstOrDefaultAsync();

            if (product == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });
            return Ok(product);
        }

        // GET: api/products/search
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId)
        {
            var query = _context.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();

                query = query.Where(p =>
                    EF.Functions.Like(p.ProductName, $"%{keyword}%") ||
                    EF.Functions.Like(p.Barcode, $"%{keyword}%"));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var list = await query
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Unit = p.Unit,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            return Ok(list);
        }

        // POST: api/products (Thêm mới nhận CreateProductDto)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Kiểm tra mã vạch trùng
            var existBarcode = await _context.Products.AnyAsync(p => p.Barcode == dto.Barcode);
            if (existBarcode) return BadRequest(new { message = "Mã vạch đã tồn tại!" });

            // Kiểm tra Danh mục tồn tại
            var cat = await _context.Categories.FindAsync(dto.CategoryId);
            if (cat == null) return BadRequest(new { message = "Danh mục không tồn tại!" });

            var newProduct = new Product
            {
                Barcode = dto.Barcode.Trim(),
                ProductName = dto.ProductName.Trim(),
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Unit = dto.Unit,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            var resultDto = new ProductDto
            {
                ProductId = newProduct.ProductId,
                Barcode = newProduct.Barcode,
                ProductName = newProduct.ProductName,
                Price = newProduct.Price,
                StockQuantity = newProduct.StockQuantity,
                Unit = newProduct.Unit,
                CategoryId = newProduct.CategoryId,
                CategoryName = cat.CategoryName
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.ProductId }, resultDto);
        }

        // PUT: api/products/{id} (Cập nhật nhận UpdateProductDto)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (id != dto.ProductId && dto.ProductId != 0)
                return BadRequest(new { message = "Mã sản phẩm không khớp!" });

            var prod = await _context.Products.FindAsync(id);
            if (prod == null) return NotFound(new { message = "Không tìm thấy sản phẩm!" });

            // Kiểm tra mã vạch trùng (trừ chính sản phẩm đang sửa)
            var existBarcode = await _context.Products
                .AnyAsync(p => p.Barcode == dto.Barcode && p.ProductId != id);
            if (existBarcode) return BadRequest(new { message = "Mã vạch đã tồn tại!" });

            // Kiểm tra danh mục
            var cat = await _context.Categories.FindAsync(dto.CategoryId);
            if (cat == null) return BadRequest(new { message = "Danh mục không tồn tại!" });

            prod.Barcode = dto.Barcode.Trim();
            prod.ProductName = dto.ProductName.Trim();
            prod.Price = dto.Price;
            prod.StockQuantity = dto.StockQuantity;
            prod.Unit = dto.Unit;
            prod.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/products/{id}
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