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
    public class DimDatesController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public DimDatesController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/DimDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimDate>>> GetDimDates()
        {
            return await _context.DimDates.ToListAsync();
        }

        // GET: api/DimDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimDate>> GetDimDate(int id)
        {
            var dimDate = await _context.DimDates.FindAsync(id);

            if (dimDate == null)
            {
                return NotFound();
            }

            return dimDate;
        }

        // PUT: api/DimDates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimDate(int id, DimDate dimDate)
        {
            if (id != dimDate.DateKey)
            {
                return BadRequest();
            }

            _context.Entry(dimDate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimDateExists(id))
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

        // POST: api/DimDates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimDate>> PostDimDate(DimDate dimDate)
        {
            _context.DimDates.Add(dimDate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DimDateExists(dimDate.DateKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDimDate", new { id = dimDate.DateKey }, dimDate);
        }

        // DELETE: api/DimDates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimDate(int id)
        {
            var dimDate = await _context.DimDates.FindAsync(id);
            if (dimDate == null)
            {
                return NotFound();
            }

            _context.DimDates.Remove(dimDate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimDateExists(int id)
        {
            return _context.DimDates.Any(e => e.DateKey == id);
        }
    }
}
