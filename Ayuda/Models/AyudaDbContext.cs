using Ayuda.Dominio.Enumerador;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Models
{
    public class AyudaDbContext : DbContext
    {
        public AyudaDbContext(DbContextOptions<AyudaDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<ArticuloClave> ArticuloClaves { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Clave> Claves { get; set; }

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
            modelBuilder.Entity<Articulo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);

            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.Padre)
                .WithMany(b => b.Categorias)
                .HasForeignKey(s => s.CategoriaId);

            modelBuilder.Entity<ArticuloClave>()
                .HasKey(ac => new { ac.ClaveId, ac.ArticuloId });

            modelBuilder.Entity<Categoria>()
            .Property(t => t.EstadoRegistro)
            .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Articulo>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());
            
            base.OnModelCreating(modelBuilder);
        }

    }

}
