using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
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

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .OrderByDescending(c => c.CustomerId)
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });

            return Ok(customer);
        }

        // GET: api/customers/search?keyword=Nguyen
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword));
            }

            var customers = await query
                .OrderByDescending(c => c.CustomerId)
                .ToListAsync();

            return Ok(customers);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customer
            );
        }

        // PUT: api/customers/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Customer updateCustomer)
        {
            if (id != updateCustomer.CustomerId)
            {
                return BadRequest(new
                {
                    message = "Mã khách hàng không khớp!"
                });
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });
            }

            customer.CustomerName = updateCustomer.CustomerName;
            customer.PhoneNumber = updateCustomer.PhoneNumber;
            customer.Address = updateCustomer.Address;
            customer.MembershipRank = updateCustomer.MembershipRank;
            customer.RewardPoints = updateCustomer.RewardPoints;

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
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}