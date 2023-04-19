using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrderWebApi.Models;

namespace PizzaOrderWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaOrdersController : ControllerBase
    {
        private readonly PizzaOrderDbContext _dbContext;

        public PizzaOrdersController(PizzaOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaOrder>>> GetAll()
        {
            var pizzaOrders = await _dbContext.PizzaOrders.ToListAsync();
            return Ok(pizzaOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaOrder>> GetById(int id)
        {
            var pizzaOrder = await _dbContext.PizzaOrders.FindAsync(id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return Ok(pizzaOrder);
        }

        [HttpPost]
        public async Task<ActionResult<PizzaOrder>> CreateOrder(PizzaOrder pizzaOrder)
        {
            _dbContext.PizzaOrders.Add(pizzaOrder);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pizzaOrder.Id }, pizzaOrder);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, PizzaOrder pizzaOrder)
        {
            if (id != pizzaOrder.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(pizzaOrder).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.PizzaOrders.Any(p => p.Id == id))
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var pizzaOrder = await _dbContext.PizzaOrders.FindAsync(id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            _dbContext.PizzaOrders.Remove(pizzaOrder);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
