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
    public class DimSalesTerritoriesController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public DimSalesTerritoriesController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/DimSalesTerritories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimSalesTerritory>>> GetDimSalesTerritories()
        {
            return await _context.DimSalesTerritories.ToListAsync();
        }

        // GET: api/DimSalesTerritories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimSalesTerritory>> GetDimSalesTerritory(int id)
        {
            var dimSalesTerritory = await _context.DimSalesTerritories.FindAsync(id);

            if (dimSalesTerritory == null)
            {
                return NotFound();
            }

            return dimSalesTerritory;
        }

        // PUT: api/DimSalesTerritories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimSalesTerritory(int id, DimSalesTerritory dimSalesTerritory)
        {
            if (id != dimSalesTerritory.SalesTerritoryKey)
            {
                return BadRequest();
            }

            _context.Entry(dimSalesTerritory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimSalesTerritoryExists(id))
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

        // POST: api/DimSalesTerritories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimSalesTerritory>> PostDimSalesTerritory(DimSalesTerritory dimSalesTerritory)
        {
            _context.DimSalesTerritories.Add(dimSalesTerritory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimSalesTerritory", new { id = dimSalesTerritory.SalesTerritoryKey }, dimSalesTerritory);
        }

        // DELETE: api/DimSalesTerritories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimSalesTerritory(int id)
        {
            var dimSalesTerritory = await _context.DimSalesTerritories.FindAsync(id);
            if (dimSalesTerritory == null)
            {
                return NotFound();
            }

            _context.DimSalesTerritories.Remove(dimSalesTerritory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimSalesTerritoryExists(int id)
        {
            return _context.DimSalesTerritories.Any(e => e.SalesTerritoryKey == id);
        }
    }
}
