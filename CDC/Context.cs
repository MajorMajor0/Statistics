using Microsoft.EntityFrameworkCore;

namespace Statistics.CDC;

public class Context : DbContext
{
	public DbSet<Corona> Coronas { get; set; }

	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(
			"Data Source=cdc.db");

		optionsBuilder.UseLazyLoadingProxies();
		base.OnConfiguring(optionsBuilder);
	}
}
