using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DatabaseContext;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEntity>>> Getemployees()
        {
          if (_context.employees == null)
          {
              return NotFound();
          }
            return await _context.employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEntity>> GetEmployeeEntity(int id)
        {
          if (_context.employees == null)
          {
              return NotFound();
          }
            var employeeEntity = await _context.employees.FindAsync(id);

            if (employeeEntity == null)
            {
                return NotFound();
            }

            return employeeEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeEntity(int id, EmployeeEntity employeeEntity)
        {
            if (id != employeeEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEntityExists(id))
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

        [HttpPost]
        public async Task<ActionResult<EmployeeEntity>> PostEmployeeEntity(EmployeeEntity employeeEntity)
        {
          if (_context.employees == null)
          {
              return Problem("Entity set 'EmployeeDbContext.employees'  is null.");
          }
            _context.employees.Add(employeeEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeEntity", new { id = employeeEntity.Id }, employeeEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeEntity(int id)
        {
            if (_context.employees == null)
            {
                return NotFound();
            }
            var employeeEntity = await _context.employees.FindAsync(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employeeEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeEntityExists(int id)
        {
            return (_context.employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
