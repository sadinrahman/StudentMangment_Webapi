using day4.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public EmployeeController(ApplicationDbContext context)
		{
			_context = context;
		}
		[HttpGet("api/Employee")]
		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			return await _context.Employees.ToListAsync();
		}
		[HttpGet("api/Employee/{id}")]
		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var employee=await _context.Employees.FindAsync();
			if (employee == null)
			{
				return NotFound();
			}
			return Ok(employee);
		}
		[HttpPost]
		public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
		{
			_context.Employees.Add(employee);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetEmployee),new {id=employee.Id},employee);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
		{
			if (id != employee.Id)
			{
				return BadRequest();
			}
			_context.Entry(employee).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();	
		}
		[HttpDelete]
			public async Task<IActionResult> DeleteEmployee(int id)
		{
			var employee=await _context.Employees.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}
			_context.Employees.Remove(employee);
			await _context.SaveChangesAsync();
			return NoContent();
		}
		
	}
}
