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
    public class FactProductInventoriesController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public FactProductInventoriesController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/FactProductInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FactProductInventory>>> GetFactProductInventories()
        {
            return await _context.FactProductInventories.ToListAsync();
        }

        // GET: api/FactProductInventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FactProductInventory>> GetFactProductInventory(int id)
        {
            var factProductInventory = await _context.FactProductInventories.FindAsync(id);

            if (factProductInventory == null)
            {
                return NotFound();
            }

            return factProductInventory;
        }

        // PUT: api/FactProductInventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactProductInventory(int id, FactProductInventory factProductInventory)
        {
            if (id != factProductInventory.ProductKey)
            {
                return BadRequest();
            }

            _context.Entry(factProductInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactProductInventoryExists(id))
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

        // POST: api/FactProductInventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FactProductInventory>> PostFactProductInventory(FactProductInventory factProductInventory)
        {
            _context.FactProductInventories.Add(factProductInventory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FactProductInventoryExists(factProductInventory.ProductKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFactProductInventory", new { id = factProductInventory.ProductKey }, factProductInventory);
        }

        // DELETE: api/FactProductInventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactProductInventory(int id)
        {
            var factProductInventory = await _context.FactProductInventories.FindAsync(id);
            if (factProductInventory == null)
            {
                return NotFound();
            }

            _context.FactProductInventories.Remove(factProductInventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactProductInventoryExists(int id)
        {
            return _context.FactProductInventories.Any(e => e.ProductKey == id);
        }
    }
}
