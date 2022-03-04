using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Statistics.US
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

        public virtual DbSet<CongressionalDistrict> CongressionalDistricts { get; set; }
        public virtual DbSet<Congressman> Congressmen { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<ocid> ocids { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\data\\us.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongressionalDistrict>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CongressionalDistrict1).HasColumnName("CongressionalDistrict");

                entity.Property(e => e.EndDate).HasColumnType("DATETIME");
            });

            modelBuilder.Entity<Congressman>(entity =>
            {
                entity.ToTable("Congressman");

                entity.HasIndex(e => e.Id, "IX_Congressman_Id")
                    .IsUnique();

                entity.Property(e => e.Birthdate).HasColumnType("DATETIME");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Abbreviation);
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Terms_Id")
                    .IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("DATETIME");

                entity.Property(e => e.StartDate).HasColumnType("DATETIME");

                entity.HasOne(d => d.Congressman)
                    .WithMany(p => p.Terms)
                    .HasForeignKey(d => d.Congressman_ID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ocid>(entity =>
            {
                entity.ToTable("ocid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
