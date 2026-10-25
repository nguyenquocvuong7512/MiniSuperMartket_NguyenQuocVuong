using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Dto;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/customers/paged?keyword=Nguyen&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedCustomers(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5;

            var query = _context.Customers
                .AsNoTracking()
                .OrderByDescending(c => c.RewardPoints)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(search) 
                                      || c.PhoneNumber.Contains(search));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            var response = new PagedResult<CustomerDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .OrderByDescending(c => c.CustomerId)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng!" });
            }

            return Ok(customer);
        }

        // GET: api/customers/by-phone/0901122334
        [HttpGet("by-phone/{phoneNumber}")]
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetByPhone(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest(new { message = "Số điện thoại không được để trống!" });
            }

            string phone = phoneNumber.Trim();
            var customer = await _context.Customers
                .AsNoTracking()
                .Where(c => c.PhoneNumber == phone)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng với số điện thoại này!" });
            }

            return Ok(customer);
        }

        // GET: api/customers/search?keyword=Nguyen
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            var query = _context.Customers.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(c =>
                    c.CustomerName.ToLower().Contains(kw) ||
                    c.PhoneNumber.Contains(kw));
            }

            var customers = await query
                .OrderByDescending(c => c.CustomerId)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            return Ok(customers);
        }

        // POST: api/customers (Thêm mới nhận CreateCustomerDto)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCustomer = new Customer
            {
                CustomerName = dto.CustomerName.Trim(),
                PhoneNumber = dto.PhoneNumber.Trim(),
                Address = dto.Address,
                RewardPoints = dto.RewardPoints,
                MembershipRank = string.IsNullOrWhiteSpace(dto.MembershipRank) ? "Mới" : dto.MembershipRank
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            var resultDto = new CustomerDto
            {
                CustomerId = newCustomer.CustomerId,
                CustomerName = newCustomer.CustomerName,
                PhoneNumber = newCustomer.PhoneNumber,
                Address = newCustomer.Address,
                RewardPoints = newCustomer.RewardPoints,
                MembershipRank = newCustomer.MembershipRank
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = resultDto.CustomerId },
                resultDto
            );
        }

        // PUT: api/customers/1 (Cập nhật nhận UpdateCustomerDto)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            if (id != dto.CustomerId && dto.CustomerId != 0)
            {
                return BadRequest(new { message = "Mã khách hàng không khớp!" });
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng!" });
            }

            customer.CustomerName = dto.CustomerName.Trim();
            customer.PhoneNumber = dto.PhoneNumber.Trim();
            customer.Address = dto.Address;
            customer.MembershipRank = dto.MembershipRank;
            customer.RewardPoints = dto.RewardPoints;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng!" });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}