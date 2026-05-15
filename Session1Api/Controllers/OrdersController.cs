using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session1Api.Models;

namespace Session1Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public OrdersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public  IActionResult GetOrders()
        {
            var orders =  _context.Orders.Select(i => new
            {
              id =   i.OrderId,
                name = i.Account.FirstName + " " + i.Account.LastName,
                date = i.OrderDateTime,
                tAmount = i.TotalAmount,
                status = i.Status
            }).Take(100).ToList();

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public  IActionResult GetOrder(Guid id)
        {
            var order =  _context.Orders.Include(i => i.OrderItems).Where(i=> i.OrderId== id).Select(i => new
            {
             id =   i.OrderId,
                name = i.Account.FirstName + " " + i.Account.LastName,
                date = i.OrderDateTime,
                tAmount = i.TotalAmount,
                status = i.Status,
               OrderItem =  i.OrderItems.Select(o=>new { o.Product.ProductName, o.Quantity , o.Price} )
            }).FirstOrDefault();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}/complete")]
        public IActionResult CompleteOrder(Guid id)
        {
            var product = _context.Orders.Find(id);
            if (product == null) return NotFound();
            if (product.Status != 2) {
                return BadRequest("Order's status is not processing to complete it.");
            }
            product.Status = 3;
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpPut("{id}/cancel")]
        public IActionResult CancelOrder(Guid id)
        {
            var product = _context.Orders.Find(id);
            if (product == null) return NotFound();
            if (product.Status == 4)
            {
                return BadRequest("Order is Already Cancelled.");
            }
            product.Status = 4;
            _context.SaveChanges();
            return Ok(product);
        }
        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
      

    }
}
