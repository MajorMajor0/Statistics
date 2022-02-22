using Microsoft.EntityFrameworkCore;

namespace Statistics.CDC;

public class Context : DbContext
{
	public DbSet<Corona> Coronas { get; set; }

	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(
			$"Data Source={FileLocations.CDC}");

		optionsBuilder.UseLazyLoadingProxies();
		base.OnConfiguring(optionsBuilder);
	}
}
