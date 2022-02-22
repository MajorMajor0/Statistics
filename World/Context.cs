using Microsoft.EntityFrameworkCore;

namespace Statistics.World;

public class Context : DbContext
{
	public DbSet<Country> Countries { get; set; }
	public DbSet<Population> Populations { get; set; }
	public DbSet<GdpPerCap> GdpPerCaps { get; set; }
	public DbSet<GdpPpp> GdpPpps { get; set; }
	public DbSet<Gdp> Gdps { get; set; }

	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(
			$"Data Source={FileLocations.World}");

		optionsBuilder.UseLazyLoadingProxies();
		base.OnConfiguring(optionsBuilder);
	}
}
