using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<cbg_field_description> cbg_field_descriptions { get; set; }
        public virtual DbSet<cbg_fips_code> cbg_fips_codes { get; set; }
        public virtual DbSet<cbg_geographic_datum> cbg_geographic_data { get; set; }
        public virtual DbSet<census_bloc_cd116> census_bloc_cd116s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\data\\census.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cbg_b01>(entity =>
            {
                entity.HasKey(e => e.census_block_group);

                entity.ToTable("cbg_b01");

                entity.HasIndex(e => e.State, "IX_cbg_b01_State");

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

            modelBuilder.Entity<cbg_field_description>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_fips_code>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<cbg_geographic_datum>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<census_bloc_cd116>(entity =>
            {
                entity.HasKey(e => e.BLOCKID);

                entity.ToTable("census_bloc_cd116");

                entity.HasIndex(e => e.BLOCK_GROUP, "thisIndex");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
