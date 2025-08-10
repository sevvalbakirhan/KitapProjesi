using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly BookStoreContext _ctx;
        public OrdersController(BookStoreContext ctx) => _ctx = ctx;

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ctx.Orders
                .Include(o => o.User)
                .Include(o => o.Book)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _ctx.Orders
                .Include(o => o.User)
                .Include(o => o.Book)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order == null ? NotFound() : Ok(order);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var list = await _ctx.Orders
                .Include(o => o.Book)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (order == null || order.UserId <= 0 || order.BookId <= 0 || order.Quantity <= 0)
                return BadRequest();

            if (order.OrderDate == default) order.OrderDate = DateTime.UtcNow;

            _ctx.Orders.Add(order);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

  
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _ctx.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _ctx.Orders.Remove(order);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}