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
    public class DimCurrenciesController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public DimCurrenciesController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/DimCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimCurrency>>> GetDimCurrencies()
        {
            return await _context.DimCurrencies.ToListAsync();
        }

        // GET: api/DimCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimCurrency>> GetDimCurrency(int id)
        {
            var dimCurrency = await _context.DimCurrencies.FindAsync(id);

            if (dimCurrency == null)
            {
                return NotFound();
            }

            return dimCurrency;
        }

        // PUT: api/DimCurrencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimCurrency(int id, DimCurrency dimCurrency)
        {
            if (id != dimCurrency.CurrencyKey)
            {
                return BadRequest();
            }

            _context.Entry(dimCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimCurrencyExists(id))
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

        // POST: api/DimCurrencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimCurrency>> PostDimCurrency(DimCurrency dimCurrency)
        {
            _context.DimCurrencies.Add(dimCurrency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimCurrency", new { id = dimCurrency.CurrencyKey }, dimCurrency);
        }

        // DELETE: api/DimCurrencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimCurrency(int id)
        {
            var dimCurrency = await _context.DimCurrencies.FindAsync(id);
            if (dimCurrency == null)
            {
                return NotFound();
            }

            _context.DimCurrencies.Remove(dimCurrency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimCurrencyExists(int id)
        {
            return _context.DimCurrencies.Any(e => e.CurrencyKey == id);
        }
    }
}
