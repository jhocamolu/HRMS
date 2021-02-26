using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reportes.Infraestructura.Enumerador;
using Reportes.Models;

namespace Reportes.Infraestructura.DbContexto
{
    public class ReportesDbContext : DbContext
    {
        public ReportesDbContext (DbContextOptions<ReportesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<ReporteParametro> ReporteParametros { get; set; }


        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted
                );

            foreach (var entityEntry in entries)
            {
                if (entityEntry.Metadata.FindProperty("FechaModificacion") != null)
                {
                    entityEntry.Property("FechaModificacion").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Activo;
                        entityEntry.Property("FechaCreacion").CurrentValue = DateTime.Now;
                    }
                    if (entityEntry.State == EntityState.Deleted)
                    {
                        entityEntry.State = EntityState.Modified;
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Eliminado;
                        entityEntry.Property("FechaEliminacion").CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e =>
              e.State == EntityState.Added
              || e.State == EntityState.Modified
              || e.State == EntityState.Deleted
              );

            foreach (var entityEntry in entries)
            {
                if (entityEntry.Metadata.FindProperty("FechaModificacion") != null)
                {
                    entityEntry.Property("FechaModificacion").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Activo;
                        entityEntry.Property("FechaCreacion").CurrentValue = DateTime.Now;
                    }
                    if (entityEntry.State == EntityState.Deleted)
                    {
                        entityEntry.State = EntityState.Modified;
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Eliminado;
                        entityEntry.Property("FechaEliminacion").CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Subcategoria>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Reporte>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Parametro>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ReporteParametro>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);

            //modelBuilder.Entity<Plantilla>()
            //       .HasOne(c => c.ComplementoEncabezado)
            //       .WithMany(b => b.Encabezados)
            //       .HasForeignKey(s => s.EncabezadoId)
            //       .OnDelete(DeleteBehavior.Restrict);

            #region Restrict

            modelBuilder.Entity<ReporteParametro>()
                .HasOne(t => t.Reporte)
                .WithMany()
                .HasForeignKey(s => s.ReporteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReporteParametro>()
                .HasOne(t => t.Parametro)
                .WithMany()
                .HasForeignKey(s => s.ParametroId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            modelBuilder.Entity<Reporte>()
                .HasIndex(p => p.Alias).IsUnique();

            modelBuilder.Entity<Reporte>()
              .Property(t => t.Formato)
              .HasConversion(new EnumToStringConverter<Formato>());
            modelBuilder.Entity<Reporte>()
              .Property(t => t.Extension)
              .HasConversion(new EnumToStringConverter<Extension>());

            modelBuilder.Entity<Parametro>()
              .Property(t => t.TipoDato)
              .HasConversion(new EnumToStringConverter<TipoDato>());

            modelBuilder.Entity<Categoria>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>()); 

            modelBuilder.Entity<Subcategoria>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Reporte>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Parametro>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ReporteParametro>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
