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
    public class DimProductsController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public DimProductsController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/DimProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimProduct>>> GetDimProducts()
        {
            return await _context.DimProducts.ToListAsync();
        }

        // GET: api/DimProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimProduct>> GetDimProduct(int id)
        {
            var dimProduct = await _context.DimProducts.FindAsync(id);

            if (dimProduct == null)
            {
                return NotFound();
            }

            return dimProduct;
        }

        // PUT: api/DimProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimProduct(int id, DimProduct dimProduct)
        {
            if (id != dimProduct.ProductKey)
            {
                return BadRequest();
            }

            _context.Entry(dimProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimProductExists(id))
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

        // POST: api/DimProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimProduct>> PostDimProduct(DimProduct dimProduct)
        {
            _context.DimProducts.Add(dimProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimProduct", new { id = dimProduct.ProductKey }, dimProduct);
        }

        // DELETE: api/DimProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimProduct(int id)
        {
            var dimProduct = await _context.DimProducts.FindAsync(id);
            if (dimProduct == null)
            {
                return NotFound();
            }

            _context.DimProducts.Remove(dimProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimProductExists(int id)
        {
            return _context.DimProducts.Any(e => e.ProductKey == id);
        }
    }
}
