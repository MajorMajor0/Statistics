using Microsoft.EntityFrameworkCore;

namespace Statistics.US;

public class Context : DbContext
{
	public DbSet<State> States { get; set; }
	public DbSet<City> Cities { get; set; }

	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(
			@"Data Source=C:\data\us.db");

		optionsBuilder.UseLazyLoadingProxies();
		base.OnConfiguring(optionsBuilder);
	}
}
