using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WEB.Models;

#nullable disable

namespace Negocios
{
    public partial class _Context : DbContext
    {
        public _Context()
        {
        }

        public _Context(DbContextOptions<_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AutomovilViewModel> AutomovilContext { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=tcp:universidadamericana-sql.database.windows.net,1433;Initial Catalog=ExamenII;Persist Security Info=False;User ID=sa-universidadamericana-sql;Password=UAM2021.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AutomovilViewModel>(entity =>
            {
                entity.HasKey(e => e.Placa);

                entity.ToTable("Automovil");

                entity.Property(e => e.Placa)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMarcha)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
