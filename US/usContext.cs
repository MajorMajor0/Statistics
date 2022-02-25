using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Statistics.US
{
	public partial class usContext : DbContext
	{
		public usContext()
		{
		}

		public usContext(DbContextOptions<usContext> options)
			: base(options)
		{
		}

		public virtual DbSet<ocid> ocids { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite($"DataSource={FileLocations.US}");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ocid>(entity =>
			{
				entity.HasNoKey();

				entity.ToTable("ocid");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
