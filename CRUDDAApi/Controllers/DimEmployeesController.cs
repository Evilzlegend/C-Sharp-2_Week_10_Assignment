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
    public class DimEmployeesController : ControllerBase
    {
        private readonly Adventureworksdw2019Context _context;

        public DimEmployeesController(Adventureworksdw2019Context context)
        {
            _context = context;
        }

        // GET: api/DimEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimEmployee>>> GetDimEmployees()
        {
            return await _context.DimEmployees.ToListAsync();
        }

        // GET: api/DimEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimEmployee>> GetDimEmployee(int id)
        {
            var dimEmployee = await _context.DimEmployees.FindAsync(id);

            if (dimEmployee == null)
            {
                return NotFound();
            }

            return dimEmployee;
        }

        // PUT: api/DimEmployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimEmployee(int id, DimEmployee dimEmployee)
        {
            if (id != dimEmployee.EmployeeKey)
            {
                return BadRequest();
            }

            _context.Entry(dimEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimEmployeeExists(id))
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

        // POST: api/DimEmployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimEmployee>> PostDimEmployee(DimEmployee dimEmployee)
        {
            _context.DimEmployees.Add(dimEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimEmployee", new { id = dimEmployee.EmployeeKey }, dimEmployee);
        }

        // DELETE: api/DimEmployees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimEmployee(int id)
        {
            var dimEmployee = await _context.DimEmployees.FindAsync(id);
            if (dimEmployee == null)
            {
                return NotFound();
            }

            _context.DimEmployees.Remove(dimEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimEmployeeExists(int id)
        {
            return _context.DimEmployees.Any(e => e.EmployeeKey == id);
        }
    }
}
