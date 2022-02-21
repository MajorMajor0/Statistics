using Microsoft.EntityFrameworkCore;

namespace Statistics.Census
{
	public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<cbg_b01> cbg_b01 { get; set; }
        public virtual DbSet<cbg_b02> cbg_b02 { get; set; }
        public virtual DbSet<cbg_b03> cbg_b03 { get; set; }
        public virtual DbSet<cbg_b07> cbg_b07 { get; set; }
        public virtual DbSet<cbg_b08> cbg_b08 { get; set; }
        public virtual DbSet<cbg_b09> cbg_b09 { get; set; }
        public virtual DbSet<cbg_b11> cbg_b11 { get; set; }
        public virtual DbSet<cbg_b12> cbg_b12 { get; set; }
        public virtual DbSet<cbg_b14> cbg_b14 { get; set; }
        public virtual DbSet<cbg_b15> cbg_b15 { get; set; }
        public virtual DbSet<cbg_b16> cbg_b16 { get; set; }
        public virtual DbSet<cbg_b17> cbg_b17 { get; set; }
        public virtual DbSet<cbg_b19> cbg_b19 { get; set; }
        public virtual DbSet<cbg_b20> cbg_b20 { get; set; }
        public virtual DbSet<cbg_b21> cbg_b21 { get; set; }
        public virtual DbSet<cbg_b22> cbg_b22 { get; set; }
        public virtual DbSet<cbg_b23> cbg_b23 { get; set; }
        public virtual DbSet<cbg_b24> cbg_b24 { get; set; }
        public virtual DbSet<cbg_b25> cbg_b25 { get; set; }
        public virtual DbSet<cbg_b27> cbg_b27 { get; set; }
        public virtual DbSet<cbg_b28> cbg_b28 { get; set; }
        public virtual DbSet<cbg_b29> cbg_b29 { get; set; }
        public virtual DbSet<cbg_b99> cbg_b99 { get; set; }
        public virtual DbSet<cbg_c02> cbg_c02 { get; set; }
        public virtual DbSet<cbg_c15> cbg_c15 { get; set; }
        public virtual DbSet<cbg_c16> cbg_c16 { get; set; }
        public virtual DbSet<cbg_c17> cbg_c17 { get; set; }
        public virtual DbSet<cbg_c21> cbg_c21 { get; set; }
        public virtual DbSet<cbg_c24> cbg_c24 { get; set; }
        public virtual DbSet<cbg_field_descriptions> cbg_field_descriptions { get; set; }
        public virtual DbSet<cbg_fips_codes> cbg_fips_codes { get; set; }
        public virtual DbSet<cbg_geographic_data> cbg_geographic_data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\census\\census.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cbg_b01>(entity =>
            {
                entity.HasKey(e => e.census_block_group);

                entity.Property(e => e.B01002Ae1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ae2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ae3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Am1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Am2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Am3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Be1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Be2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Be3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Bm1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Bm2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Bm3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ce1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ce2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ce3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Cm1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Cm2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Cm3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002De1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002De2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002De3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Dm1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Dm2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Dm3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ee1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ee2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ee3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Em1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Em2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Em3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Fe1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Fe2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Fe3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Fm1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Fm2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Fm3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ge1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ge2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ge3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Gm1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Gm2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Gm3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002He1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002He2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002He3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Hm1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Hm2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Hm3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ie1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ie2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Ie3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Im1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Im2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002Im3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002e1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002e2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002e3).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002m1).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002m2).HasColumnType("DOUBLE");

                entity.Property(e => e.B01002m3).HasColumnType("DOUBLE");
            });

            modelBuilder.Entity<cbg_b02>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b03>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b07>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b08>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b09>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b11>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b12>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b14>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b15>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b16>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b17>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b19>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b20>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b21>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b22>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b23>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b24>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b25>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b27>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b28>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b29>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_b99>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_c02>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_c15>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_c16>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_c17>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_c21>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_c24>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_field_descriptions>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_fips_codes>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_geographic_data>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
