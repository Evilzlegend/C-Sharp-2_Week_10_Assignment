#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDDAApi.Data;

namespace CRUDDAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimCustomersController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public DimCustomersController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/DimCustomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimCustomer>>> GetDimCustomers()
        {
            return await _context.DimCustomers.ToListAsync();
        }

        // GET: api/DimCustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimCustomer>> GetDimCustomer(int id)
        {
            var dimCustomer = await _context.DimCustomers.FindAsync(id);

            if (dimCustomer == null)
            {
                return NotFound();
            }

            return dimCustomer;
        }

        // PUT: api/DimCustomers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimCustomer(int id, DimCustomer dimCustomer)
        {
            if (id != dimCustomer.CustomerKey)
            {
                return BadRequest();
            }

            _context.Entry(dimCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimCustomerExists(id))
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

        // POST: api/DimCustomers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimCustomer>> PostDimCustomer(DimCustomer dimCustomer)
        {
            _context.DimCustomers.Add(dimCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimCustomer", new { id = dimCustomer.CustomerKey }, dimCustomer);
        }

        // DELETE: api/DimCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimCustomer(int id)
        {
            var dimCustomer = await _context.DimCustomers.FindAsync(id);
            if (dimCustomer == null)
            {
                return NotFound();
            }

            _context.DimCustomers.Remove(dimCustomer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimCustomerExists(int id)
        {
            return _context.DimCustomers.Any(e => e.CustomerKey == id);
        }
    }
}
