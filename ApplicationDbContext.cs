using day4.Model;
using Microsoft.EntityFrameworkCore;

namespace day4
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Employee> Employees { get; set; }
	}
}
