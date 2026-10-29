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
    [Authorize] // Yêu cầu đăng nhập toàn bộ Controller
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext qua Constructor Injection
        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/categories/paged?keyword=...&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedCategories(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5;

            var query = _context.Categories
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(c => c.CategoryName.ToLower().Contains(kw) ||
                                        (c.Description != null && c.Description.ToLower().Contains(kw)));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query
                .OrderBy(c => c.CategoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            var response = new PagedResult<CategoryDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = items
            };

            return Ok(response);
        }

        // 1. READ: Lấy toàn bộ danh mục từ SQL Server dưới dạng DTO
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.CategoryId)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(list);
        }

        // 2. READ: Lấy chi tiết 1 danh mục theo ID dưới dạng DTO
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryId == id)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng trong CSDL!" });
            }
            return Ok(category);
        }

        // 3. SEARCH: Tìm kiếm trên SQL Server trả về DTO
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }
            var result = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryName.Contains(keyword))
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Chỉ Admin được thêm, nhận CreateCategoryDto
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCat = new Category
            {
                CategoryName = dto.CategoryName.Trim(),
                Description = dto.Description
            };

            _context.Categories.Add(newCat);
            await _context.SaveChangesAsync();

            var resultDto = new CategoryDto
            {
                CategoryId = newCat.CategoryId,
                CategoryName = newCat.CategoryName,
                Description = newCat.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.CategoryId }, resultDto);
        }

        // 5. UPDATE: Chỉ Admin được sửa, nhận UpdateCategoryDto
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (id != dto.CategoryId && dto.CategoryId != 0)
            {
                return BadRequest(new { message = "Mã nhóm hàng không khớp!" });
            }

            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });

            cat.CategoryName = dto.CategoryName.Trim();
            cat.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Chỉ Admin được xóa
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Các endpoint phân quyền bổ sung (áp dụng async/await)
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            return await Task.FromResult(Ok(new { message = "Chào mừng Admin!" }));
        }

        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetStaffPos()
        {
            return await Task.FromResult(Ok(new { message = "Màn hình POS sẵn sàng!" }));
        }
    }
}
